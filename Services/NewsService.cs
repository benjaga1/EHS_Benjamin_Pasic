using EHS_Benjamin_Pasic.Data;
using EHS_Benjamin_Pasic.Models;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;

        public NewsService(HttpClient httpClient, IConfiguration configuration, AppDbContext db, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["NewsApiKey"];
            _db = db;
            _cache = cache;
        }

        public async Task<List<NewsDto>> GetNewsAsync(string category = null)
        {
            string cacheKey = $"cached_news_{(category ?? "all").ToLower()}";

            if (_cache.TryGetValue(cacheKey, out List<NewsDto> cachedNews))
            {
                return cachedNews;
            }

            string url = $"https://newsdata.io/api/1/news?apikey={_apiKey}&language=en";

            if (!string.IsNullOrWhiteSpace(category) && category.ToLower() != "all")
            {
                url += $"&category={category}";
            }

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<NewsApiResponse>(json);

            var list = result?.Results ?? new List<NewsDto>();

            _cache.Set(cacheKey, list, TimeSpan.FromMinutes(5));

            return list;

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

        public async Task<List<NewsDto>> GetMockNewsAsync()
        {
            var json = await File.ReadAllTextAsync("wwwroot/data/mock-news.json");
            return JsonSerializer.Deserialize<List<NewsDto>>(json);
        }

    }
}
