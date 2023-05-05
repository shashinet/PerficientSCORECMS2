using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Articles.Repositories;
using Perficient.Web.Features.Articles.ViewModels;

namespace Perficient.Web.Features.Articles.Apis
{
    [ApiController]    
    [Route("api/articles")]
    public class ArticleSearchController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleSearchController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search([FromBody] ArticleSearchFilterViewModel SearchFilter)
        {
            var results = _articleRepository.Search(SearchFilter);
            return Ok(results);
        }

        [HttpGet]
        [Route("track")]
        public string Track(string query, string hitId, string trackId)
        {
            _articleRepository.TrackClick(query, hitId, trackId);
            return "Ok";
        }
    }
}
