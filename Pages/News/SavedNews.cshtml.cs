using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Pages.News
{
    [Authorize]
    public class SavedNewsModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public SavedNewsModel(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public List<NewsItem> SavedNews { get; set; } = new List<NewsItem>();

        public async Task OnGetAsync()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                SavedNews = await _db.UserSavedNews
                    .Where(usn => usn.UserId == user.Id)
                    .Include(usn => usn.NewsItem)
                    .OrderByDescending(usn => usn.NewsItem.PublishedAt)
                    .Select(usn => usn.NewsItem)
                    .ToListAsync();
            }
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