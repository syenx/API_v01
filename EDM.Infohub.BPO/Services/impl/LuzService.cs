using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.Assinatura;
using EDM.Infohub.BPO.Models.DadosAtuais;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Services
{
    public class LuzService : ILuzService
    {
        private readonly ILuzClient _client;
        private readonly IConfiguration _config;
        private readonly ILogger<LuzService> _logger;


        public LuzService(IConfiguration config, ILogger<LuzService> logger, ILuzClient client)
        {
            // _client = client;
            _logger = logger;
            _config = config;
            _client = client;
            //_client = new HttpClient();
            //_client.BaseAddress = new Uri(_config["LuzSettings:URL"]);
            //_client.DefaultRequestHeaders.Add("Authorization", _config["LuzSettings:Token"]);
        }

        public async Task<string> AutenticationAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
            }
        }

        public async Task<EnviarArquivoResponse> EnviarMensagem(string xmlMessage)
        {
            try
            {
                var content = new StringContent(xmlMessage, Encoding.UTF8, "text/xml");
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");
                Console.WriteLine(content.Headers.ToString());
                HttpResponseMessage response = await _client.PostAsync("xml/", content);
                if (response.StatusCode.Equals(HttpStatusCode.UnsupportedMediaType))
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError(error, new HttpRequestException(error));
                    return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
                }
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                return JsonConvert.DeserializeObject<EnviarArquivoResponse>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<List<string>> PapeisAssinados()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("assinatura/");
                //response.EnsureSuccessStatusCode();
                if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    return new List<string>();
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<string> AssinarPapel(string papel)
        {
            try
            {
                var content = new StringContent("");
                //var content = new StringContent("", Encoding.UTF8, "");
                //content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");
                //Console.WriteLine(content.Headers.ToString());
                HttpResponseMessage response = await _client.PostAsync("assinatura/" + papel.ToUpper(), content);
                //response.EnsureSuccessStatusCode();
                if (response.StatusCode.Equals(HttpStatusCode.NotModified))
                {
                    return "{message: Papel já assinado}";
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                return responseBody;
                return JsonConvert.DeserializeObject<string>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }
        public async Task<List<AssinaturaLuzResponse>> AssinarPapelLote(List<AssinaturaLuzRequest> listaPapeis)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(listaPapeis));
                //var content = new StringContent("", Encoding.UTF8, "");
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                //Console.WriteLine(content.Headers.ToString());
                HttpResponseMessage response = await _client.PostAsync("assinatura_lista/", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                return JsonConvert.DeserializeObject<List<AssinaturaLuzResponse>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }


        public async Task<string> RemoverAssinatura(string papel)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync("assinatura/" + papel.ToUpper());
                //response.EnsureSuccessStatusCode();
                if (response.StatusCode.Equals(HttpStatusCode.NotModified))
                {
                    return "{message: Papel não existe na assinatura}";
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                return responseBody;
                return JsonConvert.DeserializeObject<string>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<(List<DadosPrecoLuz>, HttpResponseHeaders)> RelatorioPreco(DateTime date, string page)
        {
            string url = "relatorio/preco/data/";
            url += date.ToString("yyyy-MM-dd") + "/";
            //url += "/contrato/" + papel.ToUpper();
            var pathQuery = PathBuilder(url, page);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(pathQuery);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<DadosPrecoLuz> listaVazia = new List<DadosPrecoLuz>();
                    return (listaVazia, response.Headers);
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                //return responseBody;
                return (JsonConvert.DeserializeObject<List<DadosPrecoLuz>>(responseBody), response.Headers);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<(List<Fluxos>, HttpResponseHeaders)> RelatorioFluxo(DateTime date, string page)
        {
            string url = "relatorio/fluxo/data/";
            url += date.ToString("yyyy-MM-dd") + "/";

            var pathQuery = PathBuilder(url, page);
            //url += "/contrato/" + papel.ToUpper();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(pathQuery);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<Fluxos> listaVazia = new List<Fluxos>();
                    return (listaVazia, response.Headers);
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                //return responseBody;
                return (JsonConvert.DeserializeObject<List<Fluxos>>(responseBody), response.Headers);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<(List<Fluxos>, HttpResponseHeaders)> FluxoPapel(DateTime date, string papel, string page)
        {
            string url = "relatorio/fluxo/data/";
            url += date.ToString("yyyy-MM-dd") + "/";
            url += "/contrato/" + papel.Trim().ToUpper() + "/";

            var pathQuery = PathBuilder(url, page);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(pathQuery);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<Fluxos> listaVazia = new List<Fluxos>();
                    return (listaVazia, response.Headers);
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                //return responseBody;
                return (JsonConvert.DeserializeObject<List<Fluxos>>(responseBody), response.Headers);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<List<DadosCaracteristicos>> RelatorioCaracteristica(DateTime date)
        {
            string url = "relatorio/caracteristica/data/";
            url += date.ToString("yyyy-MM-dd") + "/";
            //url += "/contrato/" + papel.ToUpper();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<DadosCaracteristicos> listaVazia = new List<DadosCaracteristicos>();
                    return listaVazia;
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                //return responseBody;
                return JsonConvert.DeserializeObject<List<DadosCaracteristicos>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<(List<DadosCaracteristicos>, HttpResponseHeaders)> RelatorioCaracteristica(DateTime date, string page)
        {
            string url = "relatorio/caracteristica/data/";
            url += date.ToString("yyyy-MM-dd") + "/";
            //url += "/contrato/" + papel.ToUpper();

            var pathQuery = PathBuilder(url, page);
            try
            {
                HttpResponseMessage response = await _client.GetAsync(pathQuery);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<DadosCaracteristicos> listaVazia = new List<DadosCaracteristicos>();
                    return (listaVazia, response.Headers);
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                //return responseBody;
                return (JsonConvert.DeserializeObject<List<DadosCaracteristicos>>(responseBody), response.Headers);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<List<AssinaturaLuzResponse>> RemoverPapelLote(List<AssinaturaLuzRequest> listaPapeis)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(listaPapeis));
                //var content = new StringContent("", Encoding.UTF8, "");
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                //Console.WriteLine(content.Headers.ToString());
                var urlComplete = _client.GetBaseAddress();

                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Delete;
                request.Content = new StringContent(JsonConvert.SerializeObject(listaPapeis), System.Text.Encoding.UTF8, "application/json");
                request.RequestUri = new Uri(urlComplete.AbsoluteUri + "assinatura_lista/");

                HttpResponseMessage response = _client.SendAsync(request).Result;

                //HttpResponseMessage response = await _client.DeleteAsync("assinatura_lista/", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                return JsonConvert.DeserializeObject<List<AssinaturaLuzResponse>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<List<DadosCaracteristicos>> RelatorioCaracteristicaPapel(DateTime date, string papel)
        {
            string url = "relatorio/caracteristica/data/";
            url += date.ToString("yyyy-MM-dd") + "/";
            url += "contrato/" + papel.ToUpper() + "/";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<DadosCaracteristicos> listaVazia = new List<DadosCaracteristicos>();
                    return listaVazia;
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                //return responseBody;
                return JsonConvert.DeserializeObject<List<DadosCaracteristicos>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        public async Task<(List<DadosPrecoLuz>, HttpResponseHeaders)> RelatorioPrecoHistorico(string papel, string page)
        {

            string url = "historico/";
            url += papel.Trim().ToUpper() + "/";

            var pathQuery = PathBuilder(url, page);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(pathQuery);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<DadosPrecoLuz> listaVazia = new List<DadosPrecoLuz>();
                    response.Headers.Add("next", "");
                    return (listaVazia, response.Headers);
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation(responseBody);
                //return responseBody;
                var headers = response.Headers;

                return (JsonConvert.DeserializeObject<List<DadosPrecoLuz>>(responseBody), headers);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
                //return new EnviarArquivoResponse() { Arquivo = "", Id = " ", Status = "Erro" };
            }
        }

        private string PathBuilder(string path, string page)
        {
            var urlBuilder = new UriBuilder("luzsistemas");
            urlBuilder.Path = path;

            if (null != page)
            {
                urlBuilder.Query = page;
            }

            return urlBuilder.Uri.PathAndQuery.ToString().Substring(1);
        }

        public async Task<List<DadosPrecoLuz>> RelatorioPrecoPapel(DateTime date, string papel)
        {
            string url = "relatorio/preco/data/";
            url += date.ToString("yyyy-MM-dd") + "/";
            url += "contrato/" + papel.ToUpper() + "/";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<DadosPrecoLuz> listaVazia = new List<DadosPrecoLuz>();
                    return listaVazia;
                }
                _logger.LogInformation(responseBody);
                return JsonConvert.DeserializeObject<List<DadosPrecoLuz>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
            }
        }

        public async Task<(List<PuDeEventos>, HttpResponseHeaders)> RelatorioPagamento(DateTime date, string page)
        {
            string url = "pagamento/data/";
            url += date.ToString("yyyy-MM-dd") + "/";
            var pathQuery = PathBuilder(url, page);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(pathQuery);
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<PuDeEventos> listaVazia = new List<PuDeEventos>();
                    return (listaVazia, response.Headers);
                }
                _logger.LogInformation(responseBody);
                return (JsonConvert.DeserializeObject<List<PuDeEventos>>(responseBody), response.Headers);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
            }
        }
        public async Task<List<PuDeEventos>> RelatorioPagamentoPapel(DateTime date, string papel)
        {
            string url = "pagamento/data/";
            url += date.ToString("yyyy-MM-dd") + "/";
            url += "contrato/" + papel.ToUpper() + "/";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    List<PuDeEventos> listaVazia = new List<PuDeEventos>();
                    return listaVazia;
                }
                _logger.LogInformation(responseBody);
                return JsonConvert.DeserializeObject<List<PuDeEventos>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                throw e;
            }
        }
    }
}
