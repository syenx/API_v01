using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.Models.Response
{
    public class AssinaturaResponse<T>
    {
        [JsonProperty(PropertyName = "items")]
        public List<T> Items { get; set; }
        [JsonProperty(PropertyName = "mensagem")]
        public string Mensagem { get; set; }
        [JsonProperty(PropertyName = "hasNext")]
        public bool HasNext { get; set; }

    }

    public class PapelAssinado
    {
        [JsonProperty(PropertyName = "papel")]
        public string Papel { get; set; }
        [JsonProperty(PropertyName = "dataAssinatura")]
        public DateTime DataAssinatura { get; set; }
        [JsonProperty(PropertyName = "impactaPreco")]
        public string ImpactaPreco { get; set; }
        [JsonProperty(PropertyName = "impactaCadastro")]
        public string ImpactaCadastro { get; set; }
        [JsonProperty(PropertyName = "impactaEvento")]
        public string ImpactaEvento { get; set; }
        [JsonProperty(PropertyName = "impactaHistorico")]
        public string ImpactaHistorico { get; set; }
    }
}
