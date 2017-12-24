using System;
using System.Collections.Generic;
using BlogApi.Models;

namespace BlogApi.Repositories
{
    public static class ArticleRepository
    {
        public static Article GetArticle(int articleId)
        {
            var article = new Article
            {
                Id = 1,
                Subject = "Constraints and indexes",
                Author = "Rasmus Dybkjær",
                Category = "SQL Server",
                SubCategory = "Developers",
                Level = 3,
                Created = DateTime.Now,
                ArticleItems = new List<ArticleItem>()
            };

            foreach(var item in GetArticleItems(articleId))
            {
                article.ArticleItems.Add(item);
            }

            return article;
        }

        public static IList<ArticleMetaInfo> GetArticleMetaInfo()
        {
            var articles = new List<ArticleMetaInfo>();

            articles.Add(new ArticleMetaInfo
            {
                Id = 1,
                Subject = "Constraints and indexes",
                Author = "Rasmus Dybkjær",
                Category = "SQL Server",
                SubCategory = "Developers",
                Level = 3,
                Created = DateTime.Now,
            });
            articles.Add(new ArticleMetaInfo
            {
                Id = 2,
                Subject = "Finding blocking queries",
                Author = "Rasmus Dybkjær",
                Category = "SQL Server",
                SubCategory = "DBA",
                Level = 3,
                Created = DateTime.Now,
            });
            articles.Add(new ArticleMetaInfo
            {
                Id = 3,
                Subject = "Execution plans bla bla",
                Author = "Rasmus Dybkjær",
                Category = "SQL Server",
                SubCategory = "DBA",
                Level = 3,
                Created = DateTime.Now,
            });
            articles.Add(new ArticleMetaInfo
            {
                Id = 4,
                Subject = "Mapping in js",
                Author = "Rasmus Dybkjær",
                Category = "JavaScript",
                SubCategory = "Map",
                Level = 4,
                Created = DateTime.Now,
            }); articles.Add(new ArticleMetaInfo
            {
                Id = 5,
                Subject = "Writing React with TypeScript",
                Author = "Rasmus Dybkjær",
                Category = "TypeScript",
                SubCategory = "React",
                Level = 2,
                Created = DateTime.Now,
            });

            return articles;
        }

        public static IList<ArticleItem> GetArticleItems(int articleId)
        {
            var articleItems = new List<ArticleItem>();
            var articles = GetArticleMetaInfo();
            articleItems.Add(new ArticleItem
            {
                Id = 1,
                Text = "Here is the article called " + articles[articleId - 1].Subject,
                Type = ArticleItemType.Text
            });
            articleItems.Add(new ArticleItem
            {
                Id = 2,
                Text = "SELECT * \nFROM dbo.Table \nWHERE x = 5;",
                Type = ArticleItemType.Code,
            });
            articleItems.Add(new ArticleItem
            {
                Id = 3,
                Text = "Perhaps we should try something else? How about some line breaks 'n' stuff?",
                Type = ArticleItemType.Text,
            });
            articleItems.Add(new ArticleItem
            {
                Id = 4,
                Text = "UPDATE dbo.Table\nSET Code = 'Nix'\nWHERE POWER(y, 3) < 10;",
                Type = ArticleItemType.Code,
            });
            articleItems.Add(new ArticleItem
            {
                Id = 5,
                Text = "This is the end of my first article!",
                Type = ArticleItemType.Text,
            });

            return articleItems;
        }
    }
}