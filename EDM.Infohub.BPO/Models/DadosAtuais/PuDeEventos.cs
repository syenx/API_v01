using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Models.DadosAtuais
{
    public class PuDeEventos
    {
        [JsonProperty(PropertyName = "codigoSNA")]
        public string CodigoSNA { get; set; }
        [JsonProperty(PropertyName = "tipo")]
        public string Tipo { get; set; }
        [JsonProperty(PropertyName = "indexador")]
        public string Indexador { get; set; }
        [JsonProperty(PropertyName = "taxaPos")]
        public decimal TaxaPos { get; set; }
        [JsonProperty(PropertyName = "taxaPre")]
        public decimal TaxaPre { get; set; }
        [JsonProperty(PropertyName = "dataEvento")]
        public DateTime DataEvento { get; set; }
        [JsonProperty(PropertyName = "valorNominalBase")]
        public decimal ValorNominalBase { get; set; }
        [JsonProperty(PropertyName = "valorNominalAtualizado")]
        public decimal ValorNominalAtualizado { get; set; }
        [JsonProperty(PropertyName = "fatorCorrecao")]
        public decimal FatorCorrecao { get; set; }
        [JsonProperty(PropertyName = "fatorJuros")]
        public decimal FatorJuros { get; set; }
        [JsonProperty(PropertyName = "puAbertura")]
        public decimal PuAbertura { get; set; }
        [JsonProperty(PropertyName = "pagamentos")]
        public decimal Pagamentos { get; set; }
        [JsonProperty(PropertyName = "puFechamento")]
        public decimal PuFechamento { get; set; }
        [JsonProperty(PropertyName = "principal")]
        public decimal Principal { get; set; }
        [JsonProperty(PropertyName = "inflacao")]
        public decimal Inflacao { get; set; }
        [JsonProperty(PropertyName = "juros")]
        public decimal Juros { get; set; }
        [JsonProperty(PropertyName = "incorporado")]
        public decimal Incorporado { get; set; }
        [JsonProperty(PropertyName = "incorporar")]
        public decimal Incorporar { get; set; }
        [JsonProperty(PropertyName = "pagamentoJuros")]
        public decimal PagamentoJuros { get; set; }
        [JsonProperty(PropertyName = "pagamentoAmortizacao")]
        public decimal PagamentoAmortizacao { get; set; }
        [JsonProperty(PropertyName = "pagamentoAmex")]
        public decimal PagamentoAmex { get; set; }
        [JsonProperty(PropertyName = "pagamentoVencimento")]
        public decimal PagamentoVencimento { get; set; }
        [JsonProperty(PropertyName = "pagamentoPremio")]
        public decimal PagamentoPremio { get; set; }
        [JsonProperty(PropertyName = "porcentualAmortizado")]
        public decimal PorcentualAmortizado { get; set; }
        [JsonProperty(PropertyName = "porcentualJurosIncorporado")]
        public decimal PorcentualJurosIncorporado { get; set; }
        [JsonProperty(PropertyName = "statusPagamento")]
        public string StatusPgto { get; set; }
        [JsonProperty(PropertyName = "alteracaoStatusPagamento")]
        public DateTime DataAttStatusPgto { get; set; }
    }

    [Table("edm.tb_pu_de_eventos")]
    public class PuDeEventosDAO
    {
        public string cd_sna { get; set; }
        public byte[] cd_sna_hash { get; set; }
        public int tp_evento { get; set; }
        public DateTime dt_criacao { get; set; }
        public DateTime dt_atualizacao { get; set; }
        public bool es_ativo { get; set; }
        public string tp_papel { get; set; }
        public string tp_indexador { get; set; }
        public double vl_taxa_pos { get; set; }
        public double vl_taxa_pre { get; set; }
        public DateTime dt_evento { get; set; }
        public double vl_nominal_base { get; set; }
        public double vl_nominal_atualizado { get; set; }
        public double vl_fator_correcao { get; set; }
        public double vl_fator_juros { get; set; }
        public double vl_pu_abertura { get; set; }
        public double vl_pagamentos { get; set; }
        public double vl_pu_fechamento { get; set; }
        public double vl_principal { get; set; }
        public double vl_inflacao { get; set; }
        public double vl_juros { get; set; }
        public double vl_incorporado { get; set; }
        public int vl_incorporar { get; set; }
        public double vl_amortizacao { get; set; }
        public double vl_amex { get; set; }
        public double vl_vencimento { get; set; }
        public double vl_premio { get; set; }
        public double vl_pagamento_juros { get; set; }
        public double vl_porcentual_amortizado { get; set; }
        public double vl_porcentual_juros_incorporado { get; set; }
        public string status_pgto { get; set; }
        public DateTime dt_att_status_pgto { get; set; }

    }
}