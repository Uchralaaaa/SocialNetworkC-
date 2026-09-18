using Microsoft.EntityFrameworkCore;
using SocialPlatform.Domain;

namespace SocialPlatform.Repository
{
    public class SocialDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;

        // Уламжилсан классуудыг нэмнэ
        public DbSet<TextPost> TextPosts { get; set; } = null!;
        public DbSet<ImagePost> ImagePosts { get; set; } = null!;

        public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Entity Map
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserName).IsRequired();
                entity.Property(u => u.PassWord).IsRequired();
            });

            // Post Entity Hierarchy Map (TPH Pattern)
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.Id);

                // Derived классуудыг нэг хүснэгтэд phân biệt хийх тохиргоо
                entity.HasDiscriminator<string>("PostType")
                      .HasValue<TextPost>("Text")
                      .HasValue<ImagePost>("Image");
            });
        }
    }
}