using Newtonsoft.Json;
using Pundit.KnowledgeBase.WebCore.Commands;
using Pundit.KnowledgeBase.WebCore.Debug;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Data
{
    public class Category
    {
        private ICollection<Category> _categories = new List<Category>();
        private ICollection<Article> _articles = new List<Article>();

        private Category() { }

        public Category(string name, string icon)
            : this(name, icon, null)
        {

        }

        public Category(string name, string icon, long? parentId)
        {
            Name = name;
            SefName = CreateSearchEngineFriendlyName(name);
            Icon = icon;
            ParentId = parentId;
        }

        [Key]
        [JsonProperty]
        public long Id { get;  set; }

        [Required]
        [JsonProperty]
        public string Name { get; private set; }

        [Required]
        [JsonProperty]
        public string SefName { get; private set; }

        [JsonProperty]
        public string Icon { get; private set; }

        [JsonProperty]
        public long? ParentId { get; private set; }

        [ForeignKey("ParentId")]
        [JsonProperty]
        public IEnumerable<Category> Children { get { return _categories; } private set { _categories = value.ToList(); } }
        
        [JsonProperty]
        public IEnumerable<Article> Articles { get { return _articles; } private set { _articles = value.ToList(); } }

        public Category CreateSubCategory(Category category)
        {
            _categories.Add(category);

            return category;
        }

        public Category UpdateSubCategory(long categoryId, string name, string icon)
        {
            var category = Children.FirstOrDefault(c => c.Id == categoryId);

            Guard.Assert(() => category == null, new ArgumentException("CategoryId", $"Category_UpdateSubCategory - Category [{categoryId}] was not found."));

            category.Update(name, icon);

            return category;
        }

        public async Task DeleteSubCategoriesAsync(DeleteCategoryCommandHandler handler)
        {
            foreach (var child in _categories)
            {
                var childDeleteCategoryCommand = new DeleteCategoryCommand(child.Id, new Version("1"));

                await handler.ExecuteAsync(childDeleteCategoryCommand);
            }
        }

        public Category Update(string name, string icon)
        {
            Name = name;
            SefName = CreateSearchEngineFriendlyName(name);
            Icon = icon;

            return this;
        }

        public Article CreateArticle(string title, string content, DateTimeOffset publishDate, DateTimeOffset publishExpiration)
        {
            var article = new Article(title, content, publishDate, publishExpiration);

            _articles.Add(article);

            return article;
        }

        private string CreateSearchEngineFriendlyName(string name)
        {
            return name.Replace(" ", "-").ToLower();
        }
    }
}
