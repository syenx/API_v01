using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Services.impl
{
    public interface ILuzClient
    {
        Uri GetBaseAddress();
        Task<HttpResponseMessage> GetAsync(string path);

        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);

        Task<HttpResponseMessage> DeleteAsync(string requestUri);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
