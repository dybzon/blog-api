using System;

namespace BlogApi.Models
{
    public class ArticleMetaInfo
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int Level { get; set; }
    }
}