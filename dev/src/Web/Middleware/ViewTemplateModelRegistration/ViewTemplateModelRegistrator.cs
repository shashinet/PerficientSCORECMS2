using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using Perficient.Infrastructure.Definitions;
using Perficient.Infrastructure.Models.Base;
using Perficient.Web.Features.Articles.Models;
using Perficient.Web.Features.Blocks.Components.RichText;
using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Pages.BaseContentPage;

namespace Perficient.Web.Middleware.ViewTemplateModelRegistration
{
    [ServiceConfiguration(typeof(IViewTemplateModelRegistrator))]
    public class ViewTemplateModelRegistrator :  IViewTemplateModelRegistrator
    {
        public const string NavigationViewFolder = "~/Features/Navigation/Views";
        public const string ComponentsViewFolder = "~/Features/Blocks/Components";
        public const string ArticlesViewFolder = "~/Features/Articles/Views";

        public void Register(TemplateModelCollection viewTemplateModelRegistrator)
        {
            #region Navigation Templates

            //Registering templates for Base Page
            viewTemplateModelRegistrator.Add(typeof(BasePage), new TemplateModel[]
            {
                new TemplateModel() {
                    Name = $"{NavigationConstants.LinkTag}_BasePage",
                    Inherit = true,
                    AvailableWithoutTag = false,
                    Tags = new[] { NavigationConstants.LinkTag },
                    Path = $"{NavigationViewFolder}/DisplayTemplates/{NavigationConstants.LinkTag}_BasePage.cshtml"
                },
                new TemplateModel() {
                    Name = $"{NavigationConstants.TeaserTag}_BasePage",
                    Inherit = true,
                    AvailableWithoutTag = false,
                    Tags = new[] { NavigationConstants.TeaserTag },
                    Path = $"{NavigationViewFolder}/DisplayTemplates/{NavigationConstants.TeaserTag}_BasePage.cshtml"
                },
                 new TemplateModel
                {
                    Name = $"{NavigationConstants.NavigationLinkTag}_BasePage",
                    Inherit = true,
                    AvailableWithoutTag = false,
                    Tags = new[] { NavigationConstants.NavigationLinkTag },
                    Path = $"{NavigationViewFolder}/DisplayTemplates/{NavigationConstants.NavigationLinkTag}_BasePage.cshtml"
                }
            });

            //Registering templates for NavigationLinkBlock
            viewTemplateModelRegistrator.Add(typeof(NavigationLinkBlock), new TemplateModel[]
            {
                new TemplateModel() {
                    Name = $"{NavigationConstants.LinkTag}_NavigationLinkBlock",
                    Inherit = true,
                    AvailableWithoutTag = false,
                    Tags = new[] { NavigationConstants.LinkTag },
                    Path = $"{NavigationViewFolder}/DisplayTemplates/{NavigationConstants.LinkTag}_NavigationLinkBlock.cshtml"
                }
            });
            
            viewTemplateModelRegistrator.Add(typeof(RichTextBlock), new TemplateModel
            {
                Name = $"{NavigationConstants.LinkTag}_RichTextBlock",
                Inherit = true,
                AvailableWithoutTag = false,
                Tags = new[] { NavigationConstants.LinkTag },
                Path = $"{NavigationViewFolder}/DisplayTemplates/{NavigationConstants.LinkTag}_RichTextBlock.cshtml"
            });
            #endregion

            #region Base Content
            //Registering for Base Content
            viewTemplateModelRegistrator.Add(typeof(BaseContentPage), new TemplateModel[]
            {
                new TemplateModel() {
                    Name = $"{TagNames.PageContent}_SectionHero",
                    Inherit = true,
                    AvailableWithoutTag = false,
                    Tags = new[] { TagNames.PageContent },
                    Path = $"{ComponentsViewFolder}/SectionHero/{TagNames.PageContent}_SectionHero.cshtml"
                },
                new TemplateModel() {
                    Name = $"{TagNames.NestedContent}_Card",
                    Inherit = true,
                    AvailableWithoutTag = false,
                    Tags = new[] { TagNames.NestedContent },
                    Path = $"{ComponentsViewFolder}/Card/{TagNames.NestedContent}_Card.cshtml"
                }
            });

            #endregion

            #region Article Content

            viewTemplateModelRegistrator.Add(typeof(BaseArticlePage), new TemplateModel[]
            {
                new TemplateModel() {
                    Name = $"{TagNames.ArticleTopContent}_Card",
                    Inherit = true,
                    AvailableWithoutTag = false,
                    Tags = new[] { TagNames.ArticleTopContent },
                    Path = $"{ArticlesViewFolder}/{TagNames.ArticleTopContent}_Tile.cshtml"
                },
                new TemplateModel() {
                    Name = $"{TagNames.ArticleFeaturedContent}_Tile",
                    Inherit = true,
                    AvailableWithoutTag = false,
                    Tags = new[] { TagNames.ArticleFeaturedContent },
                    Path = $"{ArticlesViewFolder}/{TagNames.ArticleFeaturedContent}_Card.cshtml"
                }
            });

            #endregion
        }
    }
}
