using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Data
{
    public class DatabaseRepository : DbContext
    {
        public DatabaseRepository(DbContextOptions<DatabaseRepository> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
