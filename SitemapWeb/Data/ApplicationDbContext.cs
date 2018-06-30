using Microsoft.EntityFrameworkCore;
using SitemapWeb.Entities;

namespace SitemapWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SitemapSubmission> SitemapSubmissions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Sitemaps;Trusted_Connection=True;");
        }
    }
}
