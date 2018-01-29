using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Data
{
    public class Article
    {
        public Article(string title, string content, DateTimeOffset publishDate, DateTimeOffset publishExpiration)
        {
            ArticleTitle = title;
            ArticleContent = content;
            ArticlePublishDate = publishDate;
            ArticlePublishExpirationDate = publishExpiration;

            ArticleSefTitle = CreateSearchEngineFriendlyName(title);
        }

        [Key]
        [JsonProperty]
        public long ArticleId { get; private set; }

        [Required]
        [JsonProperty]
        public string ArticleTitle { get; private set; }

        [JsonProperty]
        public string ArticleContent { get; private set; }

        [JsonProperty]
        public DateTimeOffset ArticleCreationDate { get; private set; }

        [JsonProperty]
        public DateTimeOffset ArticleLastModifiedDate { get; private set; }

        [JsonProperty]
        public DateTimeOffset ArticlePublishDate { get; private set; }

        [JsonProperty]
        public DateTimeOffset ArticlePublishExpirationDate { get; private set; }

        [Required]
        [JsonProperty]
        public string ArticleSefTitle { get; private set; }

        // TODO: Handle this with an audit trail for views.
        [JsonProperty]
        public long ArticleViews { get; private set; }

        // TODO: Handle this with an audit trail for likes.
        [JsonProperty]
        public long ArticleLikes { get; private set; }

        [JsonProperty]
        public long CategoryId { get; private set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; private set; }


        private string CreateSearchEngineFriendlyName(string name)
        {
            return name.Replace(" ", "-").ToLower();
        }
    }
}
