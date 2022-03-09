using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Services.impl
{
    public class HangfireService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;
        private readonly ILogger<LuzService> _logger;

        public HangfireService(IConfiguration config, ILogger<LuzService> logger)
        {
            // _client = client;
            _logger = logger;
            _config = config;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_config["HangfireUrl"]);
            //_client.DefaultRequestHeaders.Add("Authorization", _config["LuzSettings:Token"]);
        }

        public async Task<bool> AgendarRecorrencia(string idAgendamento, string url, string cron)
        {
            try
            {
                var obj = JsonConvert.SerializeObject(new { id = idAgendamento, url = url, cron = cron });
                var content = new StringContent(obj, Encoding.UTF8, "text/json");
                //content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/json");
                //Console.WriteLine(content.Headers.ToString());
                HttpResponseMessage response = await _client.PostAsync("Agendamento/recorrente", content);
                if (response.StatusCode.Equals(HttpStatusCode.UnsupportedMediaType))
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError(error, new HttpRequestException(error));
                    return false;
                }
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation($"Agendamento {idAgendamento} = {responseBody}");
                return false;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                return false;
            }
        }

        public async Task<bool> AgendaTemporizador(string url, int tempoMinutos)
        {
            try
            {
                var obj = JsonConvert.SerializeObject(new { url = url, intervalo = tempoMinutos });
                var content = new StringContent(obj, Encoding.UTF8, "text/json");
                //content.Headers.ContentType = MediaTypeHeaderValue.Parse("text/json");
                //Console.WriteLine(content.Headers.ToString());
                HttpResponseMessage response = await _client.PostAsync("Agendamento/unitario", content);
                if (response.StatusCode.Equals(HttpStatusCode.UnsupportedMediaType))
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError(error, new HttpRequestException(error));
                    return false;
                }
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                _logger.LogInformation($"Agendamento {url} para {tempoMinutos} minutos = {responseBody}");
                return false;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message, e);
                return false;
            }
        }

    }
}
