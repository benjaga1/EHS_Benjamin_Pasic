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
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            SavedNews = await _db.NewsItems
                .OrderByDescending(n => n.PublishedAt)
                .ToListAsync();
        }
        public static string FormatCategory(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return "";

            return string.Join(", ",
                raw.Split(',', StringSplitOptions.RemoveEmptyEntries)
                   .Select(c => char.ToUpper(c.Trim()[0]) + c.Trim().Substring(1))
            );
        }

    }
}