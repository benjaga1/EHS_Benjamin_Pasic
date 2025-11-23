using EHS_Benjamin_Pasic.Models;
using EHS_Benjamin_Pasic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EHS_Benjamin_Pasic.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly NewsService _newsService;

        public DetailsModel(NewsService newsService)
        {
            _newsService = newsService;
        }

        public NewsDto News { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ArticleId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var allNews = await _newsService.GetNewsByCategoryAsync("health"); // ili prema zadanoj kategoriji
            News = allNews.FirstOrDefault(n => n.ArticleId == ArticleId);

            if (News == null)
                return NotFound();

            return Page();
        }
    }
}
