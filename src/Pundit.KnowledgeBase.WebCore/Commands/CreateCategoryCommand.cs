using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pundit.Harbinger.Commands;
using Pundit.KnowledgeBase.WebCore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Commands
{
    public class CreateCategoryCommand : Command
    {
        public CreateCategoryCommand(long aggregateId, string name, string icon,long? parentId, Version version)
            : base(aggregateId, version)
        {
            Name = name;
            Icon = icon;
            ParentId = parentId;
        }

        [Required]
        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public string Icon { get; private set; }

        [JsonProperty]
        public long? ParentId { get; private set; }

        public bool HasParentAssociation() => ParentId.HasValue;

        private string CreateSearchEngineFriendlyName(string name)
        {
            return name.Replace(" ", "-").ToLower();
        }
    }
}
