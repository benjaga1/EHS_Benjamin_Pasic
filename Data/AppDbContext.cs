using EHS_Benjamin_Pasic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<UserSavedNews> UserSavedNews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserSavedNews>()
                .HasOne(usn => usn.User)
                .WithMany(u => u.SavedNews)
                .HasForeignKey(usn => usn.UserId);

            builder.Entity<UserSavedNews>()
                .HasOne(usn => usn.NewsItem)
                .WithMany()
                .HasForeignKey(usn => usn.NewsItemId);
        }

    }

}
