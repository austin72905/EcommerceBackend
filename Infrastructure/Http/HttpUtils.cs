using Infrastructure.Interfaces;
using System.Text.Json;

namespace Infrastructure.Http
{
    public class HttpUtils : IHttpUtils
    {
        private readonly HttpClient _httpClient;

        public HttpUtils(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formData, Dictionary<string, string>? headers = null) where T : class
        {

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!_httpClient.DefaultRequestHeaders.Contains(header.Key))
                    {
                        _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }

            // 確保釋放資源
            using var content = new FormUrlEncodedContent(formData);

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Error from PostFormAsync: {response.StatusCode}, Content: {errorContent}");
            }

            var jsonResp = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(jsonResp, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result == null)
            {
                throw new InvalidOperationException("Failed to deserialize  response.");
            }

            return result;
        }


    }
}
