using System.Text.Json.Serialization;

namespace EHS_Benjamin_Pasic.Models
{
    public class NewsDto
    {
        [JsonPropertyName("article_id")]
        public string ArticleId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("category")]
        public List<string> Category { get; set; }

        [JsonPropertyName("pubDate")]
        public string PubDate { get; set; }
        [JsonIgnore]
        public DateTime PublishedAt => DateTime.TryParse(PubDate, out var dt) ? dt : DateTime.MinValue;

        [JsonPropertyName("source_name")]
        public string SourceName { get; set; }

        [JsonPropertyName("source_url")]
        public string SourceUrl { get; set; }

        public bool IsSaved { get; set; } = false;
    }
}
