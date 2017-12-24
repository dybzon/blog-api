using System.Collections.Generic;
using System.Web.Http;
using BlogApi.Repositories;
using BlogApi.Models;

namespace BlogApi.Controllers
{
    [RoutePrefix("api/articles")]
    public class ArticleController : ApiController
    {
        [Route("")]
        public IEnumerable<ArticleMetaInfo> GetArticles()
        {
            return ArticleRepository.GetArticleMetaInfo();
        }

        [Route("{id}")]
        public Article GetArticle(int id)
        {
            return ArticleRepository.GetArticle(id);
        }
    }
}
