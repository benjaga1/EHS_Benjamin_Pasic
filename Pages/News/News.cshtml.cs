using EHS_Benjamin_Pasic.Models;
using EHS_Benjamin_Pasic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EHS_Benjamin_Pasic.Pages.News
{
    public class NewsModel : PageModel
    {
        private readonly NewsService _newsService;

        public NewsModel(NewsService newsService)
        {
            _newsService = newsService;
        }

        public List<NewsDto> News { get; set; } = new List<NewsDto>();

        public async Task OnGetAsync(string category = "health")
        {
            CurrentCategory = category;
            News = await _newsService.GetNewsByCategoryAsync(category);
        }

        public async Task<IActionResult> OnGetNewsPartialAsync(string category)
        {
            var news = await _newsService.GetNewsByCategoryAsync(category);
            return Partial("_NewsListPartial", news);
        }
        public string CurrentCategory { get; set; }
    }
}
