using Newtonsoft.Json;

namespace EDM.Infohub.BPO.Models.Assinatura
{
    public class AssinaturaLuzRequest
    {
        [JsonProperty(PropertyName = "codigoContrato")]
        public string Papel { get; set; }
    }

    public class AssinaturaLuzResponse
    {
        [JsonProperty(PropertyName = "codigoContrato")]
        public string Papel { get; set; }

        [JsonProperty(PropertyName = "mensagem")]
        public string Mensagem { get; set; }
    }
}
