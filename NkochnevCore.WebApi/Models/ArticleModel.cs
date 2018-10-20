using NkochnevCore.Infrastructure.Domain;

namespace NkochnevCore.WebApi.Models
{
    public class ArticleModel
    {
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
	        SeoDescription = articleDomain.SeoDescription;
	        SeoKeyWords = articleDomain.SeoKeyWords;
        }

		public string Key { get; set; }
	    public string Title { get; set; }

	    public string CreatedIso { get; set; }
	    public string Created { get; set; }

	    public string ModifiedIso { get; set; }
	    public string Modified { get; set; }

	    public string PreviewContent { get; set; }

        public string Content { get; set; }
        public string SeoKeyWords { get; set; }
        public string SeoDescription { get; set; }
    }
}
