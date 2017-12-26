namespace BlogApi.Models
{
    using System.Data.SqlClient;

    public enum ArticleItemType
    {
        Text = 1,
        Code = 2
    }

    public class ArticleItem
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public ArticleItemType Type { get; set; }

        public static ArticleItem LoadFromReader(SqlDataReader reader)
        {
            return new ArticleItem
            {
                Id = (int)reader["Id"],
                Content = (string)reader["Content"],
                Type = (ArticleItemType)reader["Type"]
            };
        }
    }
}