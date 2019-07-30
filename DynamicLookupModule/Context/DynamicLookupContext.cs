using DynamicLookupModule.Models.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace DynamicLookupModule.Context
{
    public class DynamicLookupContext : DbContext
    {
        public DynamicLookupContext(DbContextOptions<DynamicLookupContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<LookupEmaple1> LookupEmaple1 { get; set; }
        public DbSet<LookupEmaple2> LookupEmaple2 { get; set; }
    }
}
