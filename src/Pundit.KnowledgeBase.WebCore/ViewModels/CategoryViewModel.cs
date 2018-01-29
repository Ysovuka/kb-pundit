using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.ViewModels
{
    public class CategoryViewModel
    {
        private List<CategoryViewModel> _categories = new List<CategoryViewModel>();

        private CategoryViewModel()
        {

        }
        
        public CategoryViewModel(string name, string icon)
            : this(name, icon, null)
        {
            
        }
            
        public CategoryViewModel(string name, string icon, long? parentId)
        {
            Name = name;
            Icon = icon;
            ParentId = parentId;
        }

        [JsonProperty]
        public long Id { get; set; }

        [JsonProperty]
        [Required]
        public string Name { get; set; }

        [JsonProperty]
        [Required]
        public string SefName { get; private set; }

        [JsonProperty]
        public string Icon { get; set; }

        [JsonProperty]
        public long? ParentId { get; set; }

        public IEnumerable<CategoryViewModel> Children { get { return _categories; } private set { _categories = value.ToList(); } }
    }
}
