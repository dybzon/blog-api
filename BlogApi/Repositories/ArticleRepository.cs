namespace BlogApi.Repositories
{
    using System.Collections.Generic;
    using BlogApi.Models;
    using System.Data.SqlClient;

    public static class ArticleRepository
    {
        public static Article GetArticle(int articleId)
        {
            var article = new Article(GetArticleMetaInfo(articleId));
            article.ArticleItems = GetArticleItems(articleId);
            return article;
        }

        public static IEnumerable<ArticleMetaInfo> GetArticleMetaInfo()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                connection.Open();
                var sql = @"SELECT a.Id, a.[Subject], a.Author, c.[Name] AS Category, sc.[Name] AS SubCategory, a.[Level], a.Created 
                            FROM Content.Articles a
                                INNER JOIN Content.Categories c ON c.Id = a.CategoryId
                                INNER JOIN Content.SubCategories sc ON sc.Id = a.SubCategoryId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return ArticleMetaInfo.LoadFromReader(reader);
                        }
                    }
                }
            }
        }

        public static ArticleMetaInfo GetArticleMetaInfo(int articleId)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                connection.Open();
                var sql = $@"SELECT a.Id, a.[Subject], a.Author, c.[Name] AS Category, sc.[Name] AS SubCategory, a.[Level], a.Created 
                            FROM Content.Articles a
                                INNER JOIN Content.Categories c ON c.Id = a.CategoryId
                                INNER JOIN Content.SubCategories sc ON sc.Id = a.SubCategoryId
                            WHERE a.Id = {articleId}";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        return ArticleMetaInfo.LoadFromReader(reader);
                    }
                }
            }
        }

        public static IEnumerable<ArticleItem> GetArticleItems(int articleId)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                connection.Open();
                var sql = $"SELECT Id, Type, Content FROM Content.ArticleItems WHERE ArticleId = {articleId}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return ArticleItem.LoadFromReader(reader);
                        }
                    }
                }
            }
        }
    }
}