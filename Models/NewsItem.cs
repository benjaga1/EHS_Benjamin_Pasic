namespace EHS_Benjamin_Pasic.Models
{
    public class NewsItem
    {
        public int Id { get; set; } 
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public DateTime PublishedAt { get; set; }
        public string SourceName { get; set; }
        public string SourceUrl { get; set; }
        public bool IsFavorite { get; set; } = false;
    }
}
