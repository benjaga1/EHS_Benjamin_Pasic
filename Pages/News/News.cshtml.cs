using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using EHS_Benjamin_Pasic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Pages.News
{
    public class NewsModel : PageModel
    {
        private readonly NewsService _newsService;
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        [BindProperty]
        public NewsDto NewsToSave { get; set; }

        public NewsModel(NewsService newsService, AppDbContext db, UserManager<AppUser> userManager)
        {
            _newsService = newsService;
            _db = db;
            _userManager = userManager;
        }

        public List<NewsDto> News { get; set; } = new List<NewsDto>();
        public string CurrentCategory { get; set; }

        public async Task OnGetAsync(string category = "health")
        {
            CurrentCategory = category;
            await LoadNewsWithSavedStatus(category);
        }

        public async Task<IActionResult> OnGetNewsPartialAsync(string category)
        {
            await LoadNewsWithSavedStatus(category);
            return Partial("_NewsListPartial", News);
        }

        private async Task LoadNewsWithSavedStatus(string category)
        {
            News = await _newsService.GetNewsAsync(category);

            var user = await _userManager.GetUserAsync(User);

            HashSet<string> savedIds = new();

            if (user != null)
            {
                savedIds = await _db.UserSavedNews
                    .Where(x => x.UserId == user.Id)
                    .Select(x => x.NewsItem.ArticleId)
                    .ToHashSetAsync();
            }

            foreach (var n in News)
                n.IsSaved = savedIds.Contains(n.ArticleId);
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (NewsToSave == null || string.IsNullOrEmpty(NewsToSave.ArticleId))
                return new JsonResult(new { ok = false, msg = "Invalid article" });

            if (_db.NewsItems.Any(x => x.ArticleId == NewsToSave.ArticleId))
                return new JsonResult(new { ok = false, msg = "Already saved" });

            var item = NewsMapper.MapToNewsItem(NewsToSave);

            _db.NewsItems.Add(item);
            await _db.SaveChangesAsync();

            return new JsonResult(new { ok = true, msg = "Saved" });
        }
    }
}
