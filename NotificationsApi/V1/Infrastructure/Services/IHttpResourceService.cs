using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationsApi.V1.Infrastructure.Services
{
    public interface IHttpResourceService
    {
        Task<T> GetAsync<T>(string endpoint);

        Task<T> GetAsync<T>(string endpoint, object id);

        Task<T> PostAsync<T>(string endpoint, object model);

        Task<T> PutAsync<T>(string endpoint, object model);

        Task<T> DeleteAsync<T>(string endpoint, object id);

        Task<HttpResponseMessage> SendRequestAsync(string endpoint, HttpMethod method, object body = null, bool isJson = true);
    }
}
