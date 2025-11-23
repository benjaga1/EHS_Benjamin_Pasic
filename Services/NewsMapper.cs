using EHS_Benjamin_Pasic.Models;

namespace EHS_Benjamin_Pasic.Services
{
    public static class NewsMapper
    {
        public static NewsItem MapToNewsItem(NewsDto dto)
        {
            return new NewsItem
            {
                ArticleId = dto.ArticleId,
                Title = dto.Title ?? "",
                Description = dto.Description ?? "",
                Url = dto.Link ?? "",
                ImageUrl = dto.ImageUrl ?? "",
                Category = dto.Category != null ? string.Join(",", dto.Category) : "",
                PublishedAt = DateTime.TryParse(dto.PubDate, out var date) ? date : DateTime.Now,
                SourceName = "NewsData.io",
                SourceUrl = dto.Link ?? "",
            };
        }
    }
}
