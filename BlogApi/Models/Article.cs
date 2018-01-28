namespace BlogApi.Models
{
    using System.Collections.Generic;

    public class Article : ArticleMetaInfo
    {
        public Article()
        {
        }

        public Article(ArticleMetaInfo other): base(other)
        {
        }

        public IEnumerable<ArticleItem> ArticleItems { get; set; }
    }
}