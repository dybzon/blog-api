using System.Collections.Generic;

namespace BlogApi.Models
{
    public class Article : ArticleMetaInfo
    {
        public IList<ArticleItem> ArticleItems { get; set; }
    }
}