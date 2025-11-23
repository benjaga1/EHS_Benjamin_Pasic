using EHS_Benjamin_Pasic.Models;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<NewsItem> NewsItems { get; set; }
    }
}
