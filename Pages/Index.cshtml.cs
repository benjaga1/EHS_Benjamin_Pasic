using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using EHS_Benjamin_Pasic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly NewsService _newsService;
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(NewsService newsService, AppDbContext db, UserManager<AppUser> userManager)
        {
            _newsService = newsService;
            _db = db;
            _userManager = userManager;
        }

        public List<NewsDto> News { get; set; } = new List<NewsDto>();

        public List<string> SavedArticleIds { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            News = (await _newsService.GetNewsAsync()).Take(10).ToList();

            var userId = User.Identity.IsAuthenticated
            ? _userManager.GetUserId(User)
            : null;

            if (userId != null)
            {
                SavedArticleIds = await _db.UserSavedNews
                                           .Where(x => x.UserId == userId)
                                           .Select(x => x.NewsItem.ArticleId)
                                           .ToListAsync();
            }
        }
    }
}
