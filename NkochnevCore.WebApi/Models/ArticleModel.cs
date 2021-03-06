﻿using JetBrains.Annotations;
using NkochnevCore.Infrastructure.Domain;

namespace NkochnevCore.WebApi.Models
{
    public class ArticleModel
    {
        [UsedImplicitly]
        public ArticleModel()
        {
        }

        public ArticleModel(ArticleDomain articleDomain)
        {
            Title = articleDomain.Title;
            Key = articleDomain.Key;
            CreatedIso = articleDomain.Created.ToString("o");
            Created = articleDomain.Created.ToString("dd MMMM, yyyy");
            ModifiedIso = articleDomain.Modified.ToString("o");
            Modified = articleDomain.Modified.ToString("dd MMMM, yyyy");
            PreviewContent = articleDomain.Preview;
            Content = articleDomain.Content;
            IsDraft = articleDomain.IsDraft;
        }

        public string Key { get; set; }
        public string Title { get; set; }

        public string CreatedIso { get; set; }
        public string Created { get; set; }

        public string ModifiedIso { get; set; }
        public string Modified { get; set; }

        public string PreviewContent { get; set; }

        public string Content { get; set; }
        public bool IsDraft { get; set; }

        public ArticleModel ToMarkdownStyle()
        {
            PreviewContent = MarkdownHelper.AddDefaultLangToCodeWrappers(PreviewContent);
            Content = MarkdownHelper.AddDefaultLangToCodeWrappers(Content);
            return this;
        }
    }
}