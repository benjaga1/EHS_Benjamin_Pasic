namespace EHS_Benjamin_Pasic.Models
{
    public class NewsDto
    {
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Category { get; set; }
        public string PubDate { get; set; }
        public string SourceName { get; set; }
        public string SourceUrl { get; set; }
    }
}
