namespace BlogApi.Models
{
    public enum ArticleItemType
    {
        Text = 1,
        Code = 2
    }

    public class ArticleItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ArticleItemType Type { get; set; }
    }
}