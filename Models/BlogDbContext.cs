using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TestWebApplication.Models
{
    public class BlogDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.LikedPosts)
                .WithMany(p => p.LikedUsers)
                .Map(m =>
                {
                    m.ToTable("UsersLikedPosts");
                    m.MapLeftKey("UserID");
                    m.MapRightKey("PostID");
                });
        }
    }
}