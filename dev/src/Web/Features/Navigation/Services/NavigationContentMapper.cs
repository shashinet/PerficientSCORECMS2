using AutoMapper;
using Perficient.Web.Features.Navigation.Models;
using Perficient.Web.Features.Navigation.ViewModels;

namespace Perficient.Web.Features.Navigation.Services
{
    public class NavigationContentMapper : INavigationContentMapper
    {
        private readonly IMapper _mapper;
        public NavigationContentMapper(IMapper mapper)
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
                default:
                    return null;
            }

        }
    }
}
