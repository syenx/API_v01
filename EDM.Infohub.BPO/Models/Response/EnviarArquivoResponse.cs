using Newtonsoft.Json;

namespace EDM.Infohub.BPO.Services
{
    public class EnviarArquivoResponse
    {
        [JsonProperty(PropertyName = "Arquivo")]
        public string Arquivo { get; set; }
        [JsonProperty(PropertyName = "ID")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }
    }
}