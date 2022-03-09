using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;

namespace EDM.Infohub.BPO
{
    public class AssinaturaLog
    {
        public int PK_Assinaturas_Log { get; set; }
        public int FK_Assinaturas { get; set; }
        public DateTime DT_Criacao { get; set; }
        public bool ES_Assinado { get; set; }
        public string CD_CGE { get; set; }
    }
    [Table("edm.tb_assinatura_log")]
    public class AssinaturaLogDAO
    {
        [Key]
        public int pk_assinaturas_log { get; set; }
        public int fk_assinaturas { get; set; }
        public DateTime dt_criacao { get; set; }
        public bool es_assinado { get; set; }
        public string cd_cge { get; set; }
        public string usuario { get; set; }
        public JObject tx_estado { get; set; }
    }

    public class AssinaturaLogResponse
    {
        //public int pk_assinaturas_log { get; set; }
        //public int fk_assinaturas { get; set; }
        [JsonProperty(PropertyName = "dataAtualizacao")]
        public DateTime DataAtualizacao { get; set; }
        [JsonProperty(PropertyName = "papel")]
        public string Papel { get; set; }
        [JsonProperty(PropertyName = "usuario")]
        public string Usuario { get; set; }
        [JsonProperty(PropertyName = "estado")]
        public EstadoLog Estado { get; set; }
    }

    public class EstadoLog
    {
        [JsonProperty(PropertyName = "assinado")]
        public string Assinado { get; set; }
        [JsonProperty(PropertyName = "impactaCadastro")]
        public string ImpactaCadastro { get; set; }
        [JsonProperty(PropertyName = "impactaPreco")]
        public string ImpactaPreco { get; set; }
        [JsonProperty(PropertyName = "impactaEvento")]
        public string ImpactaEvento { get; set; }
        [JsonProperty(PropertyName = "impactaHistorico")]
        public string ImpactaHistorico { get; set; }
    }

    public class AssinaturaLogInput
    {
        public AssinaturaLogDAO logObject { get; set; }
        public AssinaturaFlag flagObject { get; set; }
        public AssinaturaObject impactos { get; set; }
        public bool newData { get; set; }
    }
}