using BlogApi.Models;
using System.Web.Http.Cors;
using System.Web.Http;
using BlogApi.Repositories;
using System;

namespace BlogApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        [Route("save")]
        [HttpPost]
        public IHttpActionResult SaveArticle([FromBody]Article article)
        {
            try
            {
                article = ArticleRepository.SaveArticle(article);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(article);
        }
    }
}
