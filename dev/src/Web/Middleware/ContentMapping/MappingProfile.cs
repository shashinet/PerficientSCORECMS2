using AutoMapper;
using EPiServer;
using EPiServer.Core;
using Perficient.Infrastructure.Models.Base;
using Perficient.Infrastructure.Settings.Models;
using Perficient.Infrastructure.Settings.Models.Content;
using Perficient.Web.Features.Blocks.Collections.Slider;
using Perficient.Web.Features.Blocks.Collections.Tabs;
using Perficient.Web.Features.Blocks.Components.Button;
using Perficient.Web.Features.Blocks.Components.Card;
using Perficient.Web.Features.Blocks.Components.RichText;
using Perficient.Web.Features.Blocks.Components.Tile;
using Perficient.Web.Features.Blocks.Fields.SideNavigation.Models;
using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Navigation.ViewModels;
using System.Linq;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Navigation

            CreateMap<HeaderBlock, HeaderViewModel>()
               .ForMember(dest => dest.HeaderStyle,
                   opt => opt.MapFrom(src => src.HeaderStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
               .ForMember(dest => dest.GlobalStyle,
                   opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Image,
                   opt => opt.MapFrom<ImageMediaResolver, ContentReference>(src => src.Logo))               
               .ForMember(dest => dest.TagLine,
                   opt => opt.MapFrom<XhtmlStringMemberResolver, XhtmlString>(src => src.TagLine))
               .ForMember(dest => dest.NavigationItems,
                   opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.NavigationItems))
               .ForMember(dest => dest.UtilityNavigation,
                   opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.UtilityNavigation));

            CreateMap<FooterBlock, FooterViewModel>()
                .ForMember(dest => dest.FooterStyle,
                    opt => opt.MapFrom(src => src.FooterStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom<ImageMediaResolver, ContentReference>(src => src.Image))
                .ForMember(dest => dest.LogoUrl,
                    opt => opt.MapFrom<LinkUrlMemberResolver, Url>(src => src.ImageLink))
                .ForMember(dest => dest.UpperFooter,
                    opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.UpperFooterNavBlock))
                .ForMember(dest => dest.LowerFooter,
                    opt => opt.MapFrom<LowerFooterValueResolver>());

            CreateMap<NavigationPanelBlock, NavigationPanelViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.ColumnContent,
                    opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.MainContentArea));

            CreateMap<MenuListBlock, MenuListViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.SectionTitle,
                    opt => opt.MapFrom(src => src.SectionTitle))
                .ForMember(dest => dest.SectionUrl,
                    opt => opt.MapFrom<LinkUrlMemberResolver, Url>(src => src.SectionUrl))
                 .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.MenuListContent,
                    opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.MainContentArea));

            CreateMap<NavigationLinkBlock, NavigationLinkViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.OpenInNewWindow,
                    opt => opt.MapFrom(src => src.OpenInNewWindow))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom<LinkUrlMemberResolver, Url>(src => src.Link))
                 .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()));

            CreateMap<BasePage, NavigationLinkViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => $"basepage-{src.ContentGuid.ToString()}"))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.NavigationTitle) ? src.Name : src.NavigationTitle))
                .ForMember(dest => dest.OpenInNewWindow,
                    opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom<LinkUrlMemberResolver, Url>(src => src.LinkURL));

            CreateMap<SocialIconBlock, SocialIconViewModel>()
              .ForMember(dest => dest.Id,
                  opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.GlobalStyle,
                   opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
              .ForMember(dest => dest.Title,
                  opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Title) ? string.Empty : src.Title))
              .ForMember(dest => dest.OpenInNewWindow,
                  opt => opt.MapFrom(src => src.OpenInNewWindow))
              .ForMember(dest => dest.Url,
                   opt => opt.MapFrom<LinkUrlMemberResolver, Url>(src => src.Link))
               .ForMember(dest => dest.Image,
                   opt => opt.MapFrom<ImageMediaResolver, ContentReference>(src => src.Image));

            CreateMap<MegaMenuFlyoutBlock, MegaMenuFlyoutViewModel>()
               .ForMember(dest => dest.Id,
                   opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.MenuTitle))
                .ForMember(dest => dest.NavigationPanels,
                    opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.NavigationPanels))
                .ForMember(dest => dest.CallToActionButtons,
                    opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.CallToActionContentArea))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()));

            CreateMap<UtilitySearchBlock, UtilitySearchBoxViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
               .ForMember(dest => dest.PlaceholderText,
                    opt => opt.MapFrom(src => src.SearchPlaceholderText))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.SearchPage,
                    opt => opt.MapFrom<ContentUrlMemberResolver, ContentReference>(src => src.SearchPage));

           
            #endregion

            #region Collections                        

            CreateMap<VerticalTabsetBlock, VerticalTabsetViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title.Text ?? string.Empty))
                .ForMember(dest => dest.TitleTag,
                    opt => opt.MapFrom(src => src.Title.HeadingStyle))
                .ForMember(dest => dest.SubTitle,
                    opt => opt.MapFrom(src => src.Subtitle.Text ?? string.Empty))
                .ForMember(dest => dest.SubTitleTag,
                    opt => opt.MapFrom(src => src.Subtitle.HeadingStyle))
                .ForMember(dest => dest.Panels,
                    opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.TabPanelsContentArea))
                .ForMember(dest => dest.CallToActionButtons,
                        opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.CtaContentArea));

            CreateMap<SliderBlock, SliderViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title.Text ?? string.Empty))
                .ForMember(dest => dest.TitleTag,
                    opt => opt.MapFrom(src => src.Title.HeadingStyle))
                .ForMember(dest => dest.Cards,
                    opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.Cards))
                 .ForMember(dest => dest.CallToActionButtons,
                    opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.CtaContentArea))
                .ForMember(dest => dest.ShowPagination,
                    opt => opt.MapFrom(src => src.ShowPagination));


            CreateMap<TabPanelBlock, TabPanelViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.ButtonText,
                    opt => opt.MapFrom(src => src.PanelButtonText))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.PanelTitle))
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom<ContentUrlMemberResolver, ContentReference>(src => src.Image))
                .ForMember(dest => dest.ImageText,
                    opt => opt.MapFrom<ImageTitleResolver, ContentReference>(src => src.Image))
                .ForMember(dest => dest.Text,
                        opt => opt.MapFrom<XhtmlStringMemberResolver, XhtmlString>(src => src.PanelText))
                .ForMember(dest => dest.CallToActionButtons,
                        opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.CtaContentArea));

            CreateMap<CardBlock, CardViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.CardStyle,
                    opt => opt.MapFrom(src => src.CardStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.CaptionHeading.Text ?? string.Empty))
                .ForMember(dest => dest.TitleTag,
                    opt => opt.MapFrom(src => src.CaptionHeading.HeadingStyle))
                  .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom<ContentUrlMemberResolver, ContentReference>(src => src.Image))
                .ForMember(dest => dest.ImageText,
                    opt => opt.MapFrom<ImageTitleResolver, ContentReference>(src => src.Image))
                .ForMember(dest => dest.Body,
                    opt => opt.MapFrom<XhtmlStringMemberResolver, XhtmlString>(src => src.CaptionBody))
                .ForMember(dest => dest.BodyDescription,
                    opt => opt.MapFrom(src => src.BodyDescription))
                .ForMember(dest => dest.CallToAction,
                        opt => opt.MapFrom<ContentAreaMemberResolver, ContentArea>(src => src.CallToActionContentArea));

            CreateMap<SideNavigationLinkViewModel, SideNavigationItems>()
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.OpenInNewWindow,
                    opt => opt.MapFrom(src => src.OpenInNewWindow))
                .ForMember(dest => dest.ContentType,
                    opt => opt.MapFrom(src => (src.ChildLinks != null && src.ChildLinks.Count > 0 ? "navigationItem" : "navigationLink")))
                .ForMember(dest => dest.ChildPages,
                    opt => opt.MapFrom(src => src.ChildLinks))
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(src => src.IsActive))
                    ;
            #endregion

            #region Components
            CreateMap<ButtonBlock, ButtonViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GlobalStyle,
                    opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Style,
                    opt => opt.MapFrom(src => src.ButtonStyle))
                .ForMember(dest => dest.Text,
                    opt => opt.MapFrom(src => src.ButtonText))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom<LinkUrlMemberResolver, Url>(src => src.ButtonLink))
                .ForMember(dest => dest.OpenInNewWindow,
                    opt => opt.MapFrom(src => src.OpenInNewWindow));

            CreateMap<RichTextBlock, RichTextViewModel>()
               .ForMember(dest => dest.Value,
                   opt => opt.MapFrom<XhtmlStringMemberResolver, XhtmlString>(src => src.MainBody));

            CreateMap<TileBlock, TileViewModel>()
               .ForMember(dest => dest.Id,
               opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.GlobalStyle,
                   opt => opt.MapFrom(src => src.GlobalStyle.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
               .ForMember(dest => dest.Image,
                   opt => opt.MapFrom<ImageMediaResolver, ContentReference>(src => src.Image))
               .ForMember(dest => dest.Title,
                   opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.TileStyles,
                  opt => opt.MapFrom(src => src.TileStyles.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()))
               .ForMember(dest => dest.Link,
                   opt => opt.MapFrom<LinkUrlMemberResolver, Url>(src => src.Link))
               .ForMember(dest => dest.OpenInNewWindow,
                   opt => opt.MapFrom(src => src.OpenInNewWindow));
            #endregion

            CreateMap<JsonFileStyleSettingsModel, StyleSettings>();
        }

    }
}
