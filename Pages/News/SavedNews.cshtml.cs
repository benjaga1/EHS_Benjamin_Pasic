using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Pages.News
{
    public class SavedNewsModel : PageModel
    {
        private readonly AppDbContext _db;

        public SavedNewsModel(AppDbContext db)
        {
            _db = db;
        }

        public List<NewsItem> SavedNews { get; set; } = new List<NewsItem>();

        public async Task OnGetAsync()
        {
            SavedNews = await _db.NewsItems
                .OrderByDescending(n => n.PublishedAt)
                .ToListAsync();
        }
    }
}