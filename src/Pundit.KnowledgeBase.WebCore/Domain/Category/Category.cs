using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Pundit.KnowledgeBase.WebCore.Domain.Category
{
    public class Category : Entity
    {
        private Category() { }
        public Category(Guid requestId, long id, string name, string icon)
        {
            Id = id;
            Name = name;
            Icon = icon;
            RequestId = requestId;
        }

        [NotMapped]
        public Guid RequestId { get; private set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Required]
        public string Name { get; private set; }

        public string Icon { get; private set; }

        public long? ParentId { get; private set; }

        private IList<Category> _children = new List<Category>();
        [ForeignKey("ParentId")]
        public virtual IEnumerable<Category> Children { get { return _children; } private set { _children = value.ToList(); } }

        public void Update(Guid requestId, string name, string icon)
        {
            RequestId = requestId;

            Name = name;
            Icon = icon;
        }
    }
}
