using Microsoft.EntityFrameworkCore;
using Pundit.Harbinger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public DbSet<Category.Category> Categories { get; set; }
    }
}
