namespace BlogApi.Repositories
{
    using System.Collections.Generic;
    using BlogApi.Models;
    using System.Data.SqlClient;
    using System.Data;

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

        public static Article SaveArticle(Article article)
        {
            article.Id = SaveArticleMetaInfo(new ArticleMetaInfo(article));
            foreach(var articleItem in article.ArticleItems)
            {
                SaveArticleItem(articleItem, article.Id);
            }

            return article;
        }

        private static int SaveArticleMetaInfo(ArticleMetaInfo info)
        {
            var articleId = info.Id;
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                connection.Open();
                var sql = $"Content.SaveArticle";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = info.Id;
                    command.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = info.Subject;
                    command.Parameters.Add("@Author", SqlDbType.NVarChar).Value = info.Author;
                    command.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = info.Category;
                    command.Parameters.Add("@SubCategoryName", SqlDbType.NVarChar).Value = info.SubCategory;
                    command.Parameters.Add("@Level", SqlDbType.Int).Value = info.Level;
                    command.Parameters.Add("@NewArticleId", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    command.ExecuteNonQuery();
                    articleId = (int)command.Parameters["@NewArticleId"].Value;
                }
            }
            
            return articleId;
        }

        private static bool SaveArticleItem(ArticleItem item, int articleId)
        {
            // Think about how to delete article items that are no longer present in the article.
            // We should set them as inactive?
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                connection.Open();
                var sql = $"Content.SaveArticleItem";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = item.Id;
                    command.Parameters.Add("@Content", SqlDbType.NVarChar).Value = item.Content;
                    command.Parameters.Add("@Type", SqlDbType.Int).Value = item.Type;
                    command.Parameters.Add("@ArticleId", SqlDbType.Int).Value = articleId;
                    command.Parameters.Add("@NewItemId", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    command.ExecuteNonQuery();
                    item.Id = (int)command.Parameters["@NewItemId"].Value;
                }
            }

            return true;
        }
    }
}