using NkochnevCore.Infrastructure.Domain;

namespace NkochnevCore.WebApi.Models
{
    public class ArticleFullModel : ArticleModelBase
    {
	    public ArticleFullModel(ArticleDomain articleDomain) : base(articleDomain)
	    {
		    Content = articleDomain.Content;
		    SeoDescription = articleDomain.SeoDescription;
		    SeoKeyWords = articleDomain.SeoKeyWords;
	    }

	    public ArticleFullModel()
	    {
		    
	    }

	    public string Content { get; set; }
	    public string SeoKeyWords { get; set; }
	    public string SeoDescription { get; set; }
    }
}
