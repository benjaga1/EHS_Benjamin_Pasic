using EHS_Benjamin_Pasic.Models;
using EHS_Benjamin_Pasic.Services;
using EHS_Benjamin_Pasic.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly NewsService _newsService;
        private readonly AppDbContext _db;

        public IndexModel(NewsService newsService, AppDbContext db)
        {
            _newsService = newsService;
            _db = db;
        }

        public List<NewsDto> News { get; set; } = new List<NewsDto>();

        public List<string> SavedArticleIds { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            News = (await _newsService.GetNewsAsync()).Take(10).ToList();

            SavedArticleIds = await _db.NewsItems
                                       .Select(n => n.ArticleId)
                                       .ToListAsync();
        }
    }
}
