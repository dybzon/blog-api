namespace BlogApi.Models
{
    using System.Collections.Generic;

    public class Article : ArticleMetaInfo
    {
        public IEnumerable<ArticleItem> ArticleItems { get; set; }

        public Article(ArticleMetaInfo other): base(other)
        {
        }
    }
}