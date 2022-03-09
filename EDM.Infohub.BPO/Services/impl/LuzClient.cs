using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Services.impl
{
    public class LuzClient : ILuzClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration _config;

        public LuzClient(IConfiguration config)
        {
            _config = config;
            client = new HttpClient();
            client.BaseAddress = new Uri(_config["LuzSettings:URL"]);
            client.DefaultRequestHeaders.Add("Authorization", _config["LuzSettings:Token"]);
        }

        public Uri GetBaseAddress()
        {
            return client.BaseAddress;
        }

        public Task<HttpResponseMessage> GetAsync(string path)
        {
            return client.GetAsync(path);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return client.PostAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return client.DeleteAsync(requestUri);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return client.SendAsync(request);
        }
    }
}
