using System.Collections.Generic;
using System.Linq;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Templating;

using Microsoft.AspNetCore.Mvc.Rendering;

using Perficient.Infrastructure.Interfaces.Content;

namespace Perficient.Infrastructure.Display
{
    public class CustomContentAreaRenderer : ContentAreaRenderer
    {
        private readonly IContentAreaLoader _contentAreaLoader;
        private readonly IContentRenderer _contentRenderer;
        private readonly IContentAreaItemAttributeAssembler _attributeAssembler;
        private readonly IContentTypeRepository _contentTypeRepository;

        public CustomContentAreaRenderer(
            IContentAreaLoader contentAreaLoader,
            IContentRenderer contentRenderer,
            IContentAreaItemAttributeAssembler attributeAssembler,
            IContentTypeRepository contentTypeRepository)
        {
            _contentAreaLoader = contentAreaLoader;
            _contentRenderer = contentRenderer;
            _attributeAssembler = attributeAssembler;
            _contentTypeRepository = contentTypeRepository;
        }

        protected override void RenderContentAreaItem(
            IHtmlHelper htmlHelper,
            ContentAreaItem contentAreaItem,
            string templateTag,
            string htmlTag,
            string cssClass)
        {
            var renderSettings = new Dictionary<string, object>
            {
                ["childrencustomtagname"] = htmlTag,
                ["childrencssclass"] = cssClass,
                ["tag"] = templateTag
            };

            renderSettings = contentAreaItem.RenderSettings.Concat(
                from r in renderSettings
                where !contentAreaItem.RenderSettings.ContainsKey(r.Key)
                select r).ToDictionary(r => r.Key, r => r.Value);

            htmlHelper.ViewBag.RenderSettings = renderSettings;
            var content = _contentAreaLoader.LoadContent(contentAreaItem);
            if (content == null)
            {
                return;
            }

            var isInEditMode = IsInEditMode();

            using (new ContentRenderingScope(htmlHelper.ViewContext.HttpContext, content))
            {
                var templateModel = ResolveContentTemplate(htmlHelper, content, new List<string>() { templateTag });
                if (templateModel != null || isInEditMode)
                {
                    var renderWrappingElement = ShouldRenderWrappingElementForContentAreaItem(htmlHelper);

                    // The code for this method has been c/p from EPiServer.Web.Mvc.Html.ContentAreaRenderer.
                    // The only difference is this if/else block.
                    if (isInEditMode || renderWrappingElement)
                    {
                        var tagBuilder = new TagBuilder(htmlTag);
                        AddNonEmptyCssClass(tagBuilder, cssClass);
                        tagBuilder.MergeAttributes(_attributeAssembler.GetAttributes(
                            contentAreaItem,
                            isInEditMode,
                            templateModel != null));
                        BeforeRenderContentAreaItemStartTag(tagBuilder, contentAreaItem);
                        htmlHelper.ViewContext.Writer.Write(tagBuilder.RenderStartTag());

                        if (isInEditMode)
                        {
                            htmlHelper.ViewContext.Writer.Write(PageEditorHelperPanelStart((IContent)content));
                        }

                        htmlHelper.RenderContentData(content, true, templateModel, _contentRenderer);

                        if (isInEditMode)
                        {
                            htmlHelper.ViewContext.Writer.Write(PageEditorHelperPanelEnd((IContent)content));
                        }

                        htmlHelper.ViewContext.Writer.Write(tagBuilder.RenderEndTag());
                    }
                    else
                    {
                        // This is the extra code: don't render wrapping elements in view mode
                        htmlHelper.RenderContentData(content, true, templateModel, _contentRenderer);
                    }
                }
            }
        }

        private static string PageEditorHelperPanelEnd(IContent content) => content is IOnPageEditHelperPanel ? $"</div></div>" : string.Empty;

        private string PageEditorHelperPanelStart(IContent content)
        {
            if (content is IOnPageEditHelperPanel)
            {
                var contentType = _contentTypeRepository.Load(content.ContentTypeID);
                var blockType = contentType.DisplayName;
                var blockName = content.Name;

                var title = $"<span class=\"panel-title\"><span class=\"score-pe-component-type\">{blockType}:&nbsp;</span>{blockName}</span>";

                return $"<div class=\"score-pe panel panel-default\"><div class=\"panel-heading\">{title}</div><div class=\"panel-body\">";
            }

            return string.Empty;
        }

        private static bool ShouldRenderWrappingElementForContentAreaItem(IHtmlHelper htmlHelper)
        {
            // set 'haschildcontainers' to false by default
            var item = (bool?)htmlHelper.ViewContext.ViewData["haschildcontainers"];

            return item.HasValue && item.Value;
        }

        protected override bool ShouldRenderWrappingElement(IHtmlHelper htmlHelper)
        {
            // set 'hascontainer' to false by default
            var item = (bool?)htmlHelper.ViewContext.ViewData["hascontainer"];

            return item.HasValue && item.Value;
        }
        protected override string GetContentAreaItemCssClass(IHtmlHelper htmlHelper, ContentAreaItem contentAreaItem)
        {
            var baseItemClass = base.GetContentAreaItemCssClass(htmlHelper, contentAreaItem);
            var tag = GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem);

            return $"{baseItemClass} {tag}";
        }
    }
}
