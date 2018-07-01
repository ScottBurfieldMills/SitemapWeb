using Microsoft.EntityFrameworkCore;
using SitemapWeb.Entities;

namespace SitemapWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SitemapSubmission> SitemapSubmissions { get; set; }

        public DbSet<SitemapSubmissionStatus> SitemapSubmissionStatuses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=192.168.0.50\sqlexpress;Database=Sitemap;ConnectRetryCount=0;user=pagespeed;password=asd123";

            optionsBuilder.UseSqlServer(connection);
        }
    }
}
