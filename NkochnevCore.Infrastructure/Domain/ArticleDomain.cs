using System;
using NkochnevCore.Infrastructure.Data;

namespace NkochnevCore.Infrastructure.Domain
{
    public class ArticleDomain : BaseDomain
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Preview { get; set; }

        public string SeoKeyWords { get; set; }
        public string SeoDescription { get; set; }

        public bool IsDeleted { get; set; }
    }
}