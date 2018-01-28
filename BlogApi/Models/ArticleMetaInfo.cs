namespace BlogApi.Models
{
    using System;
    using System.Data.SqlClient;

    public class ArticleMetaInfo
    {
        public ArticleMetaInfo()
        {
        }

        public ArticleMetaInfo(Article article)
        {
            this.Id = article.Id;
            this.Subject = article.Subject;
            this.Author = article.Author;
            this.Created = article.Created;
            this.Category = article.Category;
            this.SubCategory = article.SubCategory;
            this.Level = article.Level;
        }

        protected ArticleMetaInfo(ArticleMetaInfo other)
        {
            this.Id = other.Id;
            this.Subject = other.Subject;
            this.Author = other.Author;
            this.Created = other.Created;
            this.Category = other.Category;
            this.SubCategory = other.SubCategory;
            this.Level = other.Level;
        }

        public int Id { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int Level { get; set; }

        public static ArticleMetaInfo LoadFromReader(SqlDataReader reader)
        {
            return new ArticleMetaInfo
            {
                Id = (int)reader["Id"],
                Subject = (string)reader["Subject"],
                Author = (string)reader["Author"],
                Created = (DateTime)reader["Created"],
                Category = (string)reader["Category"],
                SubCategory = (string)reader["SubCategory"],
                Level = (int)reader["Level"],
            };
        }
    }
}