using Microsoft.EntityFrameworkCore;
using Gallery.Models.Entities;

namespace Gallery.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Image>()
                .HasMany(i => i.Tags)
                .WithMany(t => t.Images);
        }
    }
}
