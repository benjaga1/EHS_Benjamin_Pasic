using EHS_Benjamin_Pasic.Models;
using EHS_Benjamin_Pasic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EHS_Benjamin_Pasic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly NewsService _newsService;
        public IndexModel(NewsService newsService)
        {
            _newsService = newsService;
        }
        public List<NewsDto> News { get; set; } = new List<NewsDto>();
        public async Task OnGetAsync()
        {
            News = await _newsService.GetHealthNewsAsync();
        }
    }
}
