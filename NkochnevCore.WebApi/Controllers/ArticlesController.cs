using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.Extensions.Logging;
using NkochnevCore.Infrastructure.Services.Interfaces;
using NkochnevCore.WebApi.Models;

namespace NkochnevCore.WebApi.Controllers
{
	[Produces("application/json")]
	[Route("api/articles")]
	public class ArticlesController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly ILogger<ArticlesController> _logger;

		public ArticlesController(IArticleService articleService, ILogger<ArticlesController> logger)
		{
			_articleService = articleService;
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<ArticleModelBase> GetArticles()
		{
			_logger.LogInformation("GetArticles says hello");
			var articles = _articleService.GetArticles();
			return articles.Select(x => new ArticleModelBase(x));
		}

		[HttpGet("{key}")]
		public ArticleFullModel GetArticleByKey(string key)
		{
			var articleDomain = _articleService.GetArticleDomain(key);
			return new ArticleFullModel(articleDomain);
		}
		
		[HttpPut("{key}")]
		[Authorize()]
		public ActionResult UpdateArticle([FromRoute] string key, [FromBody]ArticleFullModel article)
		{
			_articleService.UpdateArticle(key, article.Title, article.Content, article.PreviewContent,
				article.SeoKeyWords, article.SeoDescription);
			return new OkResult();
		}
		
		[HttpPost]
		[Authorize()]
		public ActionResult CreateArticle([FromBody] ArticleFullModel article)
		{
			_articleService.CreateArticle(article.Key, article.Title, article.Content, article.PreviewContent,
				article.SeoKeyWords, article.SeoDescription);
			return new OkResult();
		}
	}
}