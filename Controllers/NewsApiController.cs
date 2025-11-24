using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using EHS_Benjamin_Pasic.Services;
using Microsoft.AspNetCore.Mvc;

namespace EHS_Benjamin_Pasic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public NewsApiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveNews([FromBody] NewsDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.ArticleId))
                return BadRequest(new { ok = false, msg = "Invalid article" });

            if (_db.NewsItems.Any(x => x.ArticleId == dto.ArticleId))
                return BadRequest(new { ok = false, msg = "Already saved" });

            var item = NewsMapper.MapToNewsItem(dto);

            _db.NewsItems.Add(item);
            await _db.SaveChangesAsync();

            return Ok(new { ok = true, msg = "Saved" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNews(string id)
        {
            var item = _db.NewsItems.FirstOrDefault(x => x.ArticleId == id);

            if (item == null)
                return NotFound(new { ok = false, msg = "Not found" });

            _db.NewsItems.Remove(item);
            await _db.SaveChangesAsync();

            return Ok(new { ok = true, msg = "Deleted" });
        }

    }
}
