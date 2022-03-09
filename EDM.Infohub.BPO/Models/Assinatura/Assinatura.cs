using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO
{
    public class Assinatura
    {
        [JsonProperty(PropertyName = "papel")]
        public string Papel { get; set; }
        [JsonProperty(PropertyName = "dataAssinatura")]
        public string DataAssinatura { get; set; }
        [JsonProperty(PropertyName = "impactaPreco")]
        public virtual bool ImpactaPreco { get; set; }
        [JsonProperty(PropertyName = "impactaCadastro")]
        public virtual bool ImpactaCadastro { get; set; }
        [JsonProperty(PropertyName = "impactaEvento")]
        public virtual bool ImpactaEvento { get; set; }
        [JsonProperty(PropertyName = "impactaHistorico")]
        public virtual bool ImpactaHistorico { get; set; }
        [JsonProperty(PropertyName = "assinado")]
        public bool Assinado { get; set; }

    }

    [Table("edm.tb_assinatura")]
    public class AssinaturaDAO
    {
        [Key]
        public virtual int? pk_assinaturas { get; set; }
        public virtual string cd_sna { get; set; }
        public virtual byte[] cd_sna_hash { get; set; }
        public virtual DateTime dt_assinatura { get; set; }
        public virtual DateTime dt_atualizacao { get; set; }
        public virtual bool es_assinado { get; set; }
        public virtual bool impacta_mdp { get; set; }


    }

    public class AssinaturaNullable : AssinaturaObject
    {
        [JsonProperty(PropertyName = "papel")]
        public override string Papel { get => Papel; set { Papel = ""; } }
        [JsonProperty(PropertyName = "impactaPreco")]
        public override bool ImpactaPreco { get => false; set { ImpactaPreco = false; } }
        [JsonProperty(PropertyName = "impactaCadastro")]
        public override bool ImpactaCadastro { get => false; set { ImpactaCadastro = false; } }
        [JsonProperty(PropertyName = "impactaEvento")]
        public override bool ImpactaEvento { get => false; set { ImpactaEvento = false; } }
        [JsonProperty(PropertyName = "impactaHistorico")]
        public override bool ImpactaHistorico { get => false; set { ImpactaHistorico = false; } }

        //public override int? pk_assinaturas { get =>  pk_assinaturas; set { pk_assinaturas = 0; } }
        //public override string cd_sna { get => cd_sna; set { cd_sna = ""; } }
        //public override DateTime dt_assinatura { get => dt_assinatura; set { dt_assinatura = new DateTime(); } }
        //public override DateTime dt_atualizacao { get => dt_atualizacao; set { dt_atualizacao = new DateTime(); } }
        //public override bool es_assinado { get => false; set { es_assinado = false; } }
        //public override bool impacta_mdp { get => false; set { impacta_mdp = false; } }
    }

    public class Items<T>
    {
        [JsonProperty(PropertyName = "items")]
        public List<T> items { get; set; }
    }

    public class AssinaturaObject
    {
        [JsonProperty(PropertyName = "papel")]
        public virtual string Papel { get; set; }
        [JsonProperty(PropertyName = "impactaCadastro")]
        public virtual bool ImpactaCadastro { get; set; }
        [JsonProperty(PropertyName = "impactaPreco")]
        public virtual bool ImpactaPreco { get; set; }
        [JsonProperty(PropertyName = "impactaEvento")]
        public virtual bool ImpactaEvento { get; set; }
        [JsonProperty(PropertyName = "impactaHistorico")]
        public virtual bool ImpactaHistorico { get; set; }
    }
}