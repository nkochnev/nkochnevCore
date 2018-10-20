using System;
using System.Collections.Generic;
using System.Linq;
using NkochnevCore.Infrastructure.Data;
using NkochnevCore.Infrastructure.Domain;
using NkochnevCore.Infrastructure.Services.Interfaces;

namespace NkochnevCore.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<ArticleDomain> _articleRepository;

        public ArticleService(IRepository<ArticleDomain> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public ArticleDomain GetArticleDomain(string key)
        {
            var article = FindArticleDomain(key);
            if (article == null) throw new Exception(string.Format("Не найдена статья с ключом {0}", key));
            return article;
        }

        public ArticleDomain FindArticleDomain(string key)
        {
            if (!string.IsNullOrEmpty(key)) key = key.ToLower();
            var article = GetNotDeleted().FirstOrDefault(x => x.Key.ToLower() == key);
            return article;
        }

        public ArticleDomain CreateArticle(string key, string title, string content, string preview, string seoKeyWords,
            string seoDescription)
        {
            var article = FindArticleDomain(key);
            if (article != null) throw new Exception($"Статья с ключом {article.Key} уже создана");

            article = FillArticle(new ArticleDomain(), key, title, content, preview, seoKeyWords, seoDescription);
            article.Created = DateTime.Now;
            _articleRepository.Insert(article);
            return article;
        }

        public ArticleDomain UpdateArticle(string key, string title, string content, string preview, string seoKeyWords,
            string seoDescription)
        {
            var article = FindArticleDomain(key);
            article = FillArticle(article, key, title, content, preview, seoKeyWords, seoDescription);
            _articleRepository.Update(article);
            return article;
        }

        public List<ArticleDomain> GetArticles()
        {
            return GetNotDeleted().OrderByDescending(x => x.Created).ToList();
        }

        public void Delete(string key)
        {
            var article = GetArticleDomain(key);
            article.IsDeleted = true;
            _articleRepository.Update(article);
        }

        private ArticleDomain FillArticle(ArticleDomain article, string key, string title, string content,
            string preview, string seoKeyWords,
            string seoDescription)
        {
            article.Title = title;
            article.Content = content;
            article.Modified = DateTime.Now;
            article.Key = key;
            article.SeoKeyWords = seoKeyWords;
            article.SeoDescription = seoDescription;
            article.Preview = preview;
            return article;
        }

        private IQueryable<ArticleDomain> GetNotDeleted()
        {
            return _articleRepository.Table.Where(x => !x.IsDeleted);
        }
    }
}