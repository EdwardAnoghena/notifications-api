using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotificationsApi.V1.Infrastructure.Services
{
    public abstract class HttpResourceService : IHttpResourceService
    {
        private readonly IHttpClientFactory _httpClient;

        protected abstract Uri BaseUrl { get; }

        public virtual Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        public HttpResourceService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string endpoint, object itemId = null)
        {
            if (itemId != null)
            {
                endpoint += $"/{itemId}";
            }

            return await GetAsync<T>(endpoint).ConfigureAwait(false);
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await SendRequestAsync(endpoint, HttpMethod.Get).ConfigureAwait(false);

            return await SendResponse<T>(response).ConfigureAwait(false);
        }

        public async Task<string> GetAsync(string endpoint)
        {
            var response = await SendRequestAsync(endpoint, HttpMethod.Get).ConfigureAwait(false);

            return await ReadResponse(response).ConfigureAwait(false);
        }

        public async Task<T> PostAsync<T>(string endpoint, object model)
        {
            var response = await SendRequestAsync(endpoint, HttpMethod.Post, model).ConfigureAwait(false);

            return await SendResponse<T>(response).ConfigureAwait(false);
        }

        public async Task<T> PutAsync<T>(string endpoint, object model)
        {
            var response = await SendRequestAsync(endpoint, HttpMethod.Put, model).ConfigureAwait(false);

            return await SendResponse<T>(response).ConfigureAwait(false);
        }

        public async Task<T> DeleteAsync<T>(string endpoint, object item)
        {
            if (item != null)
            {
                endpoint += $"/{item}";
            }

            var response = await SendRequestAsync(endpoint, HttpMethod.Delete).ConfigureAwait(false);

            return await SendResponse<T>(response).ConfigureAwait(false);
        }

        private static async Task<string> ReadResponse(HttpResponseMessage httpResponse)
        {
            var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
        }

        private static async Task<T> SendResponse<T>(HttpResponseMessage httpResponse)
        {
            var content = await ReadResponse(httpResponse).ConfigureAwait(false);
            if (content != null && content.Contains("<"))
            {
                throw new HttpRequestException();
            }
            return JsonSerializer.Deserialize<T>(content);
        }

        public async Task<HttpResponseMessage> SendRequestAsync(string endpoint, HttpMethod method, object body = null, bool isJson = true)
        {
            var apiUrl = new Uri(BaseUrl, endpoint);
#pragma warning disable CA2000 // Dispose objects before losing scope
            var request = new HttpRequestMessage(method, apiUrl);
#pragma warning restore CA2000 // Dispose objects before losing scope

            foreach (var item in Headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            if (body != null)
            {
                var json = JsonSerializer.Serialize(body);

                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var client = _httpClient.CreateClient();
            if (isJson)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset-utf-8");
            }

            var response = await client.SendAsync(request).ConfigureAwait(false);
            client.Dispose();
            return response;
        }



        //public async Task<T> GetAltAsync<T>(string path) where T : class
        //{
        //    var client = new HttpClient();
        //    // client.BaseAddress = new Uri(baseURL);
        //    var response = await client.GetAsync(path).ConfigureAwait(false);
        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("The request was not successful...");
        //    string result = response.Content.ReadAsStringAsync().Result;
        //    T returnValue = JsonSerializer.Deserialize<T>(result);
        //    return returnValue;
        //}
    }
}
