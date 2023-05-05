//using EPiServer;
//using EPiServer.Core;
//using EPiServer.ServiceLocation;
//using EPiServer.Web.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using Perficient.Web.Features.Articles.ViewModels;

//namespace Perficient.Web.Features.Articles
//{
//    public class ArticleSearchPageController : PageController<ArticleSearchPage>
//    {
//        private Injected<IContentLoader> _contentLoader { get; set; }
//        public ActionResult Index(ArticleSearchPage currentContent)
//        {

//            var model = new SearchViewModel<ArticleSearchPage>(currentContent);
//            model.SearchPageID = currentContent.ContentLink.ID;            
//            return View("~/Features/Blog/SearchPage.cshtml", model);
//        }

//        [HttpPost]        
//        public ActionResult Index(SearchViewModel<ArticleSearchPage> model, int SearchPageID)
//        {   
//            model.CurrentContent = _contentLoader.Service.Get<ArticleSearchPage>(new ContentReference(SearchPageID));
//            model.SearchResults = null; // List API Call
//            return View("~/Features/Blog/SearchPage.cshtml", model);
//        }       
//    }
//}

