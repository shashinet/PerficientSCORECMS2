using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Perficient.Infrastructure.Models.Content;
using Perficient.Web.Features.Articles;
using Perficient.Web.Features.Articles.Models;
using Perficient.Web.Features.Articles.Pages.ArticleCategoryLanding;
using Perficient.Web.Features.Articles.Pages.BlogDetails;
using Perficient.Web.Features.Articles.Pages.NewsDetails;
using Perficient.Web.Features.Categories;
using Perficient.Web.Features.Pages.GenericFullWidth;
using Perficient.Web.Features.Pages.GenericInterior;
using Perficient.Web.Features.Pages.GenericLanding;
using Perficient.Web.Features.Pages.Home;
using Perficient.Web.Features.Pages.HttpStatus;
using System;
using System.Collections.Generic;

namespace Perficient.Web.Middleware.Initialization
{
    [ModuleDependency(typeof(InitializationModule))]
    public class AvailableContentRestrictionInitialization : IInitializableModule
    {
        private IContentTypeRepository _contentTypeRepository;
        private IAvailableSettingsRepository _availableSettingsRepository;

        public void Initialize(InitializationEngine context)
        {
            _contentTypeRepository = context.Locate.Advanced.GetInstance<IContentTypeRepository>();
            _availableSettingsRepository = context.Locate.Advanced.GetInstance<IAvailableSettingsRepository>();

            
            var typesOnHome = new List<Type>
            {
                typeof(FolderPage),
                typeof(ArticleHomePage),
                typeof(GenericLandingPage),
                typeof(GenericFullWidthPage),
                typeof(ArticleSearchPage),
                typeof(HttpStatusPage),                
            };          

            var typesOnFolder = new List<Type>
            {
                typeof(HomePage),
                typeof(ArticleHomePage),
                typeof(FolderPage),
                typeof(GenericLandingPage),
                typeof(GenericFullWidthPage),
                typeof(ArticleSearchPage),
                typeof(HttpStatusPage)
            };


            // Home Page
            SetPageRestriction<HomePage>(typesOnHome);

            // Folder Page Details 
            SetPageRestriction<FolderPage>(typesOnFolder);

            // Article Home Page
            SetPageRestriction<ArticleHomePage>(new List<Type>
            {                
                typeof(ArticleCategoryLandingPage),
                typeof(BlogDetailsPage),
                typeof(NewsDetailsPage)
            });

            // Article Home Page
            SetPageRestriction<ArticleCategoryLandingPage>(new List<Type>
            {
                typeof(BlogDetailsPage),
                typeof(NewsDetailsPage)
            });

            // GenericLanding Page
            SetPageRestriction<GenericLandingPage>(new List<Type>
            {
                typeof(GenericLandingPage),
                typeof(GenericInteriorPage),
                typeof(ArticleHomePage)
            });
            // Generic Full Width Page
            SetPageRestriction<GenericFullWidthPage>(new List<Type>
            {
                typeof(GenericLandingPage),
                typeof(GenericFullWidthPage),
                typeof(GenericInteriorPage),
                typeof(ArticleHomePage)
            });

            SetPageRestriction<GenericInteriorPage>(new List<Type>
            {
                typeof(GenericInteriorPage)
            });

            SetPageRestriction<ArticleCategoryLandingPage>(new List<Type>
            {
                typeof(BlogDetailsPage),
                typeof(ArticleHomePage)
            });
            SetPageRestriction<BlogDetailsPage>(new List<Type>
            {
                typeof(BlogDetailsPage),
                typeof(ArticleHomePage)
            });          
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        private void DisallowAll<T>()
        {
            var page = _contentTypeRepository.Load(typeof(T));

            var setting = new AvailableSetting
            {
                Availability = Availability.None
            };

            _availableSettingsRepository.RegisterSetting(page, setting);
        }

        private void SetPageRestriction<T>(IEnumerable<Type> pageTypes)
        {
            var page = _contentTypeRepository.Load(typeof(T));

            var setting = new AvailableSetting
            {
                Availability = Availability.Specific
            };

            foreach (var pageType in pageTypes)
            {
                var contentType = _contentTypeRepository.Load(pageType);
                setting.AllowedContentTypeNames.Add(contentType.Name);
            }

            _availableSettingsRepository.RegisterSetting(page, setting);

        }
    }
}
