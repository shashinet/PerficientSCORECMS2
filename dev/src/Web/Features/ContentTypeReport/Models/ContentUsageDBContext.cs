using Microsoft.EntityFrameworkCore;

namespace Perficient.Web.Features.ContentTypeReport.Models
{
    public class ContentTypeDBContext : DbContext
    {
        public ContentTypeDBContext(DbContextOptions<ContentTypeDBContext> options) : base(options) { }
        public DbSet<ContentUsageDetail> ContentUsageDetails { get; set; }
        public DbSet<ContentUsageImage> ContentUsageImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentUsageDetail>().ToTable("ContentUsageDetail");
            modelBuilder.Entity<ContentUsageImage>().ToTable("ContentUsageImage");
            base.OnModelCreating(modelBuilder);
        }
    }
}
