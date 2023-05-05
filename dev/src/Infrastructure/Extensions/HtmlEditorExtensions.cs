using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Perficient.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Perficient.Infrastructure.Extensions
{
    public static class HtmlEditorExtensions
    {
        /// <summary>
        /// <para>Builds an option bar for On Page Editing for any properties that are marked with the <c>OptionBarItemAttribute</c>.??<br /></para>
        /// Additionally, any option bar items with TriggerFullRefresh enabled, will be added to a hidden element with
        /// a 'data-epi-full-refresh-property-names' attribute to require a full page refresh when edited.<br />
        /// </summary>
        /// See <see cref="OptionBarItemAttribute"/>
        /// <param name="helper">The HTML helper instance that this method extends</param>
        /// <returns>Returns the HTML markup for the option bar as well as the full refresh hidden values.
        /// </returns>
        public static HtmlString RenderOptionBar<TModel>(this IHtmlHelper<TModel> helper, bool showOnPage = false) where TModel : IContentData
        {
            if (helper.IsInEditMode())
            {
                var optionProps = getMarkedOptionItems(typeof(TModel));

                return getOptionBarMarkup(helper, optionProps, showOnPage);
            }

            return HtmlString.Empty;
        }

        public static HtmlString RenderOptionBar(this IHtmlHelper helper, IContentData model, bool showOnPage = false)
        {
            if (helper.IsInEditMode())
            {
                var optionProps = getMarkedOptionItems(model.GetType());

                return getOptionBarMarkup(helper, optionProps, showOnPage);
            }

            return HtmlString.Empty;
        }

        /// <summary>
        /// Builds an option bar for On Page Editing for any properties specified in the <paramref name="propertyNames"/> array.
        /// Optionally appends any properties of <typeparamref name="TModel"/> marked with <see cref="OptionBarItemAttribute"/> if <paramref name="includeMarkedProperties"/> is set to True.
        /// Note: If marked properties are included, specified parameters for <paramref name="showValues"/> and <paramref name="addFullRefreshMetadata"/> override values from attribute.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper">The HTML helper instance that this method extends</param>
        /// <param name="propertyNames">A string array of property names to include in the option bar.</param>
        /// <param name="showValues">Whether or not to show the values for each item in the option bar.</param>
        /// <param name="addFullRefreshMetadata">Whether the FullRefreshMetadata element should be added to the edit page for these items.</param>
        /// <param name="includeMarkedProperties">If set to true, the option bar will append any properties marked with
        /// <see cref="OptionBarItemAttribute"/> in order according to their OrderIndex.</param>
        /// <returns></returns>
        public static HtmlString RenderOptionBar<TModel>(
            this IHtmlHelper<TModel> helper,
            string[] propertyNames,
            bool includeMarkedProperties = false,
            bool showOnPage = false) where TModel : IContentData
        {
            if (!helper.IsInEditMode())
            {
                return HtmlString.Empty;
            }

            var modelProps = typeof(TModel).GetProperties();
            var optionProps = modelProps.Where(p => propertyNames.Contains(p.Name));

            if (includeMarkedProperties)
            {
                optionProps = optionProps.Union(getMarkedOptionItems(typeof(TModel)));
            }

            return getOptionBarMarkup(helper, optionProps, showOnPage);
        }


        /// <summary>
        /// <para>Builds a hidden element for On Page Editing for any properties that are marked with the <c>FullRefreshAttribute</c>.??<br /></para>
        /// The properties will be added to a hidden element with a 'data-epi-full-refresh-property-names' attribute to require a full page refresh when edited.<br />
        /// </summary>
        /// See <see cref="FullRefreshAttribute"/>
        /// <param name="helper">The HTML helper instance that this method extends</param>
        /// <returns>Returns the HTML markup for the full refresh hidden values.
        /// </returns>
        public static HtmlString FullRefreshPropertiesAttribute<TModel>(this IHtmlHelper<TModel> helper) where TModel : IContentData
        {
            if (helper.IsInEditMode())
            {
                return (HtmlString)helper.FullRefreshPropertiesMetaData(getFullRefreshProperties(typeof(TModel)).Select(p => p.Name).ToArray());
            }

            return HtmlString.Empty;
        }

        public static HtmlString FullRefreshPropertiesAttribute(this IHtmlHelper helper, IContentData model)
        {
            if (helper.IsInEditMode())
            {
                return (HtmlString)helper.FullRefreshPropertiesMetaData(getFullRefreshProperties(model.GetType()).Select(p => p.Name).ToArray());
            }

            return HtmlString.Empty;
        }

        private static IEnumerable<PropertyInfo> getFullRefreshProperties(Type modelType)
        {
            var modelProps = modelType.GetProperties();
            return modelProps
                .Where(p => Attribute.IsDefined(p, typeof(FullRefreshAttribute)))
                .Where(p => !Attribute.IsDefined(p, typeof(ScaffoldColumnAttribute)) || p.GetCustomAttribute<ScaffoldColumnAttribute>().Scaffold)
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreAttribute)));
        }

        private static IEnumerable<PropertyInfo> getMarkedOptionItems(Type modelType)
        {
            var markedProps = modelType.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(OptionBarItemAttribute)))
                .Where(p => !Attribute.IsDefined(p, typeof(ScaffoldColumnAttribute)) || p.GetCustomAttribute<ScaffoldColumnAttribute>().Scaffold)
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreAttribute)));

            return markedProps.OrderBy(p => p.GetCustomAttribute<OptionBarItemAttribute>().OrderIndex);
        }

        private static HtmlString getOptionBarMarkup(IHtmlHelper helper, IEnumerable<PropertyInfo> optionProps, bool showOnPage = false)
        {
            var optionsMarkup = optionProps.Select(p => getOptionBarItemMarkup(p, helper, p.GetCustomAttribute<OptionBarItemAttribute>().ShowFieldValue));
            var refreshString = helper.FullRefreshPropertiesMetaData(optionProps.Where(p => p.GetCustomAttribute<OptionBarItemAttribute>().TriggerFullRefresh).Select(p => p.Name).ToArray());
            var markupString = $"{refreshString}{Environment.NewLine}<div class=\"edit-options-bar {(!showOnPage ? "no-page-show" : "")}\">{string.Join(Environment.NewLine, optionsMarkup)}</div>";

            return new HtmlString(markupString);
        }

        private static string getOptionBarItemMarkup(PropertyInfo optionItem, IHtmlHelper helper, bool includeValue = false)
        {
            var description = optionItem.GetCustomAttribute<OptionBarItemAttribute>()?.DisplayText;
            if (string.IsNullOrWhiteSpace(description))
            {
                description = optionItem.GetCustomAttribute<DisplayAttribute>()?.Name;
                if (string.IsNullOrWhiteSpace(description))
                {
                    description = optionItem.Name;
                }
            }
            if (includeValue)
            {
                description += $"<br />(Current: {optionItem.GetValue(helper.ViewData.Model) ?? string.Empty})";
            }
            return $"<div class=\"option\" {helper.EditAttributes(optionItem.Name)}>{description}</div>";
        }
    }
}
