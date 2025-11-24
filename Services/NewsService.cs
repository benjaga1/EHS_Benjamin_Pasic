using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EHS_Benjamin_Pasic.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly AppDbContext _db;

        public NewsService(HttpClient httpClient, IConfiguration configuration, AppDbContext db)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["NewsData:ApiKey"];
            _db = db;
        }

        public async Task<List<NewsDto>> GetNewsAsync(string category = null)
        {
            string url = $"https://newsdata.io/api/1/news?apikey={_apiKey}&language=en";

            if (!string.IsNullOrWhiteSpace(category) && category.ToLower() != "all")
            {
                url += $"&category={category}";
            }

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<NewsApiResponse>(json);

            return result?.Results ?? new List<NewsDto>();
        }


        public async Task<List<NewsDto>> GetNewsByCategoryAsync(string category)
        {
            string url = $"https://newsdata.io/api/1/news?apikey={_apiKey}&language=en&category={category}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<NewsApiResponse>(json);

            return result?.Results ?? new List<NewsDto>();
        }

        private class NewsApiResponse
        {
            [JsonPropertyName("results")]
            public List<NewsDto> Results { get; set; }
        }
    }
}
