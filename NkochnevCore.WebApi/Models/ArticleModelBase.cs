using NkochnevCore.Infrastructure.Domain;

namespace NkochnevCore.WebApi.Models
{
    public class ArticleModelBase
    {
	    public ArticleModelBase(ArticleDomain articleDomain)
	    {
		    Title = articleDomain.Title;
		    Key = articleDomain.Key;
		    CreatedIso = articleDomain.Created.ToString("o");
		    Created = articleDomain.Created.ToString("dd MMMM, yyyy");
		    ModifiedIso = articleDomain.Modified.ToString("o");
		    Modified = articleDomain.Modified.ToString("dd MMMM, yyyy");
		    PreviewContent = articleDomain.Preview;
	    }

	    public ArticleModelBase()
	    {
		    
	    }

		public string Key { get; set; }
	    public string Title { get; set; }

	    public string CreatedIso { get; set; }
	    public string Created { get; set; }

	    public string ModifiedIso { get; set; }
	    public string Modified { get; set; }

	    public string PreviewContent { get; set; }
	}
}
