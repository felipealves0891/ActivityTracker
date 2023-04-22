using ActivityTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivityTracker.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>(x => 
            {
                x.ToTable("Issues");
                x.HasKey(x => x.Id);
                x.Property(x => x.Name).HasMaxLength(120);
                x.Property(x => x.Description).HasMaxLength(550);

            });

            modelBuilder.Entity<Activity>(x =>
            {
                x.ToTable("Activities");
                x.HasKey(x => x.Id);
                x.Property(x => x.Name).HasMaxLength(550);

            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<Activity> Activities { get; set; }
    }
}
