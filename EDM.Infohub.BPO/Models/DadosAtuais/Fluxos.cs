using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;

namespace EDM.Infohub.BPO
{
    public class Fluxos
    {
        [JsonProperty(PropertyName = "codigoSNA")]
        public string CodigoSNA { get; set; }
        [JsonProperty(PropertyName = "dataBase")]
        public DateTime DataBase { get; set; }
        [JsonProperty(PropertyName = "dataLiquidacao")]
        public DateTime DataLiquidacao { get; set; }
        [JsonProperty(PropertyName = "tipoEvento")]
        public string TipoEvento { get; set; }
        [JsonProperty(PropertyName = "taxa")]
        public decimal Taxa { get; set; }
    }
    [Table("edm.tb_fluxos")]
    public class FluxosDAO
    {
        [Key]
        public string cd_sna { get; set; }
        public byte[] cd_sna_hash { get; set; }
        public int tp_papel { get; set; }
        public DateTime dt_base { get; set; }
        public DateTime dt_criacao { get; set; }
        public DateTime dt_atualizacao { get; set; }
        public bool es_ativo { get; set; }
        public DateTime dt_liquidacao { get; set; }
        public string tp_evento { get; set; }
        public double vl_taxa { get; set; }
        public double vl_amortizacao { get; set; }
    }
}