namespace EHS_Benjamin_Pasic.Models
{
    public class NewsItem
    {
        public int Id { get; set; } 
        public string ArticleId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
        public string SourceName { get; set; } = string.Empty;
        public string SourceUrl { get; set; } = string.Empty;
    }
}
