using Dapper.Contrib.Extensions;
using System;

namespace EDM.Infohub.BPO
{
    //public class AssinaturaFlag
    //{
    //    [JsonProperty(PropertyName = "papel")]
    //    public string Papel { get; set; }
    //    [JsonProperty(PropertyName = "dataAssinatura")]
    //    public string DataAssinatura { get; set; }
    //    [JsonProperty(PropertyName = "impactaMdp")]
    //    public string ImpactaMdp { get; set; }
    //    [JsonProperty(PropertyName = "assinado")]
    //    public string Assinado { get; set; }
    //}

    [Table("edm.TB_ASSINATURA_TIPO_IMPACTO")]
    public class AssinaturaFlagDAO
    {
        [Key]
        public int PK_ASSINATURA_IMPACTO { get; set; }
        public int FK_ASSINATURA { get; set; }
        public string EN_TIPO_IMPACTO { get; set; }
        public bool ES_IMPACTADO { get; set; }
        public DateTime dt_atualizacao_flag { get; set; }
    }

    public class AssinaturaFlag
    {
        [Key]
        public virtual int? pk_assinaturas { get; set; }
        public virtual string cd_sna { get; set; }
        public virtual byte[] cd_sna_hash { get; set; }
        public virtual DateTime dt_assinatura { get; set; }
        public virtual DateTime dt_atualizacao { get; set; }
        public virtual bool es_assinado { get; set; }
        public virtual bool impacta_mdp { get; set; }
        public virtual bool impacta_preco { get; set; }
        public virtual bool impacta_cadastro { get; set; }
        public virtual bool impacta_pu_evento { get; set; }
        public virtual bool impacta_pu_historico { get; set; }
    }
}