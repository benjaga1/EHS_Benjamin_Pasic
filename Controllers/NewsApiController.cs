using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using EHS_Benjamin_Pasic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EHS_Benjamin_Pasic.Controllers
{
    [Authorize]
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
            if (!User.Identity.IsAuthenticated)
                return Unauthorized(new { ok = false, msg = "Login required" });

            if (dto == null || string.IsNullOrEmpty(dto.ArticleId))
                return BadRequest(new { ok = false, msg = "Invalid article" });

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var existingItem = await _db.NewsItems
                .FirstOrDefaultAsync(x => x.ArticleId == dto.ArticleId);

            if (existingItem == null)
            {
                existingItem = NewsMapper.MapToNewsItem(dto);
                _db.NewsItems.Add(existingItem);
                await _db.SaveChangesAsync();
            }

            bool alreadySaved = await _db.UserSavedNews
                .AnyAsync(x => x.UserId == userId && x.NewsItemId == existingItem.Id);

            if (alreadySaved)
                return BadRequest(new { ok = false, msg = "Already saved" });

            var userSaved = new UserSavedNews
            {
                UserId = userId,
                NewsItemId = existingItem.Id
            };

            _db.UserSavedNews.Add(userSaved);
            await _db.SaveChangesAsync();

            return Ok(new { ok = true, msg = "Saved" });
        }


        [HttpDelete("delete/{articleId}")]
        public async Task<IActionResult> DeleteNews(string articleId)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized(new { ok = false, msg = "Login required" });

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var item = await _db.NewsItems.FirstOrDefaultAsync(x => x.ArticleId == articleId);
            if (item == null)
                return NotFound(new { ok = false, msg = "Not found" });

            var link = await _db.UserSavedNews
                .FirstOrDefaultAsync(x => x.UserId == userId && x.NewsItemId == item.Id);

            if (link == null)
                return NotFound(new { ok = false, msg = "Not saved by this user" });

            _db.UserSavedNews.Remove(link);
            await _db.SaveChangesAsync();

            return Ok(new { ok = true, msg = "Deleted" });
        }

    }
}
