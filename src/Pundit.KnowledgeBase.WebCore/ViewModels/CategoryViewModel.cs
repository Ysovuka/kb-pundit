using Pundit.KnowledgeBase.WebCore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel() { }
        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
