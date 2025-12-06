using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CreativeToolsAggregatorApp.Models;

namespace CreativeToolsAggregatorApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CreativeToolsAggregatorApp.Models.Tools> Tools { get; set; } = default!;
    }
}
