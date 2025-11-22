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

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["NewsData:ApiKey"];
        }

        public async Task<List<NewsDto>> GetHealthNewsAsync()
        {
            string url = $"https://newsdata.io/api/1/news?apikey={_apiKey}&language=en&category=health";

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
