using Newtonsoft.Json;

namespace EDM.Infohub.BPO.Services
{
    public class AutenticationResponse
    {
        [JsonProperty(PropertyName = "usuario")]
        string Usuario { get; set; }
        [JsonProperty(PropertyName = "status")]
        string Status { get; set; }
    }
}