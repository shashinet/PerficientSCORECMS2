using AutoMapper;
using Perficient.Web.Features.Blocks.Collections.Tabs;
using Perficient.Web.Features.Blocks.Components.Button;
using Perficient.Web.Features.Blocks.Components.Card;
using Perficient.Web.Features.Blocks.Components.RichText;
using Perficient.Web.Features.Blocks.Components.Tile;
using Perficient.Web.Features.Media;
using Perficient.Web.Features.Media.ViewModels;
using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Navigation.ViewModels;

namespace Perficient.Web.Middleware.ContentMapping
{
    public class ContentMapper : IContentMapper
    {
        private readonly IMapper _mapper;
        public ContentMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public object MapContentTypes(object item)
        {
            switch (item)
            {
                case FooterBlock:
                    return _mapper.Map<FooterViewModel>(item);
                case NavigationPanelBlock:
                    return _mapper.Map<NavigationPanelViewModel>(item);
                case MenuListBlock:
                    return _mapper.Map<MenuListViewModel>(item);
                case SocialIconBlock:
                    return _mapper.Map<SocialIconViewModel>(item);
                case NavigationLinkBlock:
                    return _mapper.Map<NavigationLinkViewModel>(item);
                case MegaMenuFlyoutBlock:
                    return _mapper.Map<MegaMenuFlyoutViewModel>(item);
                case ImageMediaData:
                    return _mapper.Map<ImageMediaViewModel>(item);
                case RichTextBlock:
                    return _mapper.Map<RichTextViewModel>(item);
                case TabPanelBlock:
                    return _mapper.Map<TabPanelViewModel>(item);
                case ButtonBlock:
                    return _mapper.Map<ButtonViewModel>(item);
                case CardBlock:
                    return _mapper.Map<CardViewModel>(item);
                case UtilitySearchBlock:
                    return _mapper.Map<UtilitySearchBoxViewModel>(item);
                case TileBlock:
                    return _mapper.Map<TileViewModel>(item);
                default:
                    return null;
            }

        }
    }
}
