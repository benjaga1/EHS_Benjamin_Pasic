using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Pages.News
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _db;

        public DetailsModel(AppDbContext db)
        {
            _db = db;
        }

        public NewsItem News { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ArticleId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            News = await _db.NewsItems.FirstOrDefaultAsync(x => x.ArticleId == ArticleId);

            if (News == null)
                return NotFound();

            return Page();
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
