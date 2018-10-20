using System.Collections.Generic;
using NkochnevCore.Infrastructure.Domain;

namespace NkochnevCore.Infrastructure.Services.Interfaces
{
    public interface IArticleService
    {
        ArticleDomain GetArticleDomain(string key);
        ArticleDomain FindArticleDomain(string key);

        ArticleDomain CreateArticle(string key, string title, string content, string preview, string seoKeyWords,
            string seoDescription);

        ArticleDomain UpdateArticle(string key, string title, string content, string preview, string seoKeyWords,
            string seoDescription);

        List<ArticleDomain> GetArticles();

        void Delete(string key);
    }
}