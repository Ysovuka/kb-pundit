using Pundit.Harbinger.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Commands
{
    public class DeleteCategoryCommand : Command
    {
        public DeleteCategoryCommand(long aggregateId, Version version)
            : base(aggregateId, version)
        {
 
        }
    }
}
