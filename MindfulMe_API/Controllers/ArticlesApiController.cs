using Entities.ViewModels.Article;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace MindfulMe_API.Controllers
{
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesApiController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("/articles_list")]
        public ActionResult<IEnumerable<ArticleViewModel>> GetAllArticles()
        {
            var articles = _articleService.GetAllArticles();
            var articleViewModels = articles.Select(article => new ArticleViewModel
            {
                ArticleId = article.ArticleId,
                Title = article.Title,
                Description = article.Description,
                ImageUrl = article.ImageUrl,
                Content = article.Content,
                AuthorUsername = article.AuthorUsername
            }).ToList();

            return Ok(articleViewModels);
        }

        [HttpGet]
        [Route("/get_article/{id}")]
        public ActionResult<ArticleViewModel> GetArticleById(int id)
        {
            var article = _articleService.GetArticleById(id);

            if (article == null)
            {
                return NotFound();
            }

            var articleViewModel = new ArticleViewModel
            {
                ArticleId = article.ArticleId,
                Title = article.Title,
                Description = article.Description,
                ImageUrl = article.ImageUrl,
                Content = article.Content,
                AuthorUsername = article.AuthorUsername
            };

            return Ok(articleViewModel);
        }

        [HttpPost]
        [Route("/create_article")]
        public ActionResult CreateArticle([FromBody] CreateArticleViewModel articleCreateViewModel)
        {
            if (articleCreateViewModel == null)
            {
                return BadRequest();
            }

            var newArticle = new Article
            {
                Title = articleCreateViewModel.Title,
                Description = articleCreateViewModel.Description,
                ImageUrl = articleCreateViewModel.ImageUrl,
                Content = articleCreateViewModel.Content,
                AuthorUsername = articleCreateViewModel.AuthorUsername
            };

            _articleService.CreateArticle(newArticle);

            return Ok();
        }

        [HttpPut]
        [Route("/update_article/{id}")]
        public ActionResult UpdateArticle(int id, [FromBody] UpdateArticleViewModel articleUpdateViewModel)
        {
            if (articleUpdateViewModel == null)
            {
                return BadRequest();
            }

            var existingArticle = _articleService.GetArticleById(id);

            if (existingArticle == null)
            {
                return NotFound();
            }

            existingArticle.Title = articleUpdateViewModel.Title;
            existingArticle.Description = articleUpdateViewModel.Description;
            existingArticle.ImageUrl = articleUpdateViewModel.ImageUrl;
            existingArticle.Content = articleUpdateViewModel.Content;
            existingArticle.AuthorUsername = articleUpdateViewModel.AuthorUsername;

            _articleService.UpdateArticle(existingArticle);

            return Ok();
        }

        [HttpDelete]
        [Route("/delete_article/{id}")]
        public ActionResult DeleteArticle(int id)
        {
            var existingArticle = _articleService.GetArticleById(id);

            if (existingArticle == null)
            {
                return NotFound();
            }

            _articleService.DeleteArticle(id);

            return Ok();
        }
    }
}
