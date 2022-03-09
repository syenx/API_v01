using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;

namespace EDM.Infohub.BPO
{
    public class DadosCaracteristicos
    {
        [JsonProperty(PropertyName = "codigoSNA")]
        public string CodigoSNA { get; set; }
        [JsonProperty(PropertyName = "tipo")]
        public string Tipo { get; set; }
        [JsonProperty(PropertyName = "isin")]
        public string Isin { get; set; }
        [JsonProperty(PropertyName = "emissor")]
        public string Emissor { get; set; }
        [JsonProperty(PropertyName = "cnpjEmissor")]
        public string CnpjEmissor { get; set; }
        [JsonProperty(PropertyName = "dataEmissao")]
        public DateTime DataEmissao { get; set; }
        [JsonProperty(PropertyName = "dataInicioRentabilidade")]
        public DateTime DataInicioRentabilidade { get; set; }
        [JsonProperty(PropertyName = "dataVencimento")]
        public DateTime DataVencimento { get; set; }
        [JsonProperty(PropertyName = "valorNominalEmissao")]
        public decimal ValorNominalEmissao { get; set; }
        [JsonProperty(PropertyName = "instrucaoCVM")]
        public string InstrucaoCVM { get; set; }
        [JsonProperty(PropertyName = "clearing")]
        public string Clearing { get; set; }
        [JsonProperty(PropertyName = "agenteFiduciario")]
        public string AgenteFiduciario { get; set; }
        [JsonProperty(PropertyName = "possibilidadeResgateAntecipado")]
        public bool PossibilidadeResgateAntecipado { get; set; }
        [JsonProperty(PropertyName = "conversivelAcao")]
        public bool ConversivelAcao { get; set; }
        [JsonProperty(PropertyName = "debentureIncentivada")]
        public bool DebentureIncentivada { get; set; }
        [JsonProperty(PropertyName = "criterioCalculoIndexador")]
        public string CriterioCalculoIndexador { get; set; }
        [JsonProperty(PropertyName = "criterioCalculoJuros")]
        public string CriterioCalculoJuros { get; set; }
        [JsonProperty(PropertyName = "indexador")]
        public string Indexador { get; set; }
        [JsonProperty(PropertyName = "taxaPre")]
        public decimal TaxaPre { get; set; }
        [JsonProperty(PropertyName = "taxaPos")]
        public decimal TaxaPos { get; set; }
        [JsonProperty(PropertyName = "projecao")]
        public string Projecao { get; set; }
        [JsonProperty(PropertyName = "tipoAmortizacao")]
        public string Amortizacao { get; set; }
        [JsonProperty(PropertyName = "periodicidadeCorrecao")]
        public string PeriodicidadeCorrecao { get; set; }
        [JsonProperty(PropertyName = "unidadeIndexador")]
        public string UnidadeIndexador { get; set; }
        [JsonProperty(PropertyName = "defasagemIndexador")]
        public int DefasagemIndexador { get; set; }
        [JsonProperty(PropertyName = "diaReferenciaIndexador")]
        public int DiaReferenciaIndexador { get; set; }
        [JsonProperty(PropertyName = "mesReferenciaIndexador")]
        public int MesReferenciaIndexador { get; set; }
        [JsonProperty(PropertyName = "devedor")]
        public string Devedor { get; set; }
        [JsonProperty(PropertyName = "tipoRegime")]
        public string TipoRegime { get; set; }
        [JsonProperty(PropertyName = "tipoAniversario")]
        public string TipoAniversario { get; set; }
        [JsonProperty(PropertyName = "consideraCorrecaoNegativa")]
        public bool ConsideraCorrecaoNegativa { get; set; }
        [JsonProperty(PropertyName = "dataUltimaAlteracao")]
        public DateTime DataUltimaAlteracao { get; set; }
        [JsonProperty(PropertyName = "cnpjDevedor")]
        public string CnpjDevedor { get; set; }
        [JsonProperty(PropertyName = "cnpjAgenteFiduciario")]
        public string CnpjAgenteFiduciario { get; set; }
    }

    [Table("edm.tb_dados_caracteristicos")]
    public class DadosCaracteristicosDAO
    {
        [Key]
        public int pk_dados_caracteristicos { get; set; }
        public string cd_sna { get; set; }
        public byte[] cd_sna_hash { get; set; }
        public int tp_evento { get; set; }
        public DateTime dt_criacao { get; set; }
        public DateTime dt_atualizacao { get; set; }
        public bool es_ativo { get; set; }
        public string tp_papel { get; set; }
        public string cd_isin { get; set; }
        public string tx_emissor { get; set; }
        public string tx_cnpj_emissor { get; set; }
        public DateTime dt_emissao { get; set; }
        public DateTime dt_inicio_rentabilidade { get; set; }
        public DateTime dt_vencimento { get; set; }
        public double vl_nominal_emissao { get; set; }
        public string tx_instrucao_cvm { get; set; }
        public string tp_clearing { get; set; }
        public string tx_agente_fiduciario { get; set; }
        public bool es_possibilidade_resgate_antecipado { get; set; }
        public bool es_conversivel_acao { get; set; }
        public bool es_debenture_incentivada { get; set; }
        public string tp_criterio_calculo_indexador { get; set; }
        public string tp_criterio_calculo_juros { get; set; }
        public string tp_indexador { get; set; }
        public double vl_taxa_pre { get; set; }
        public double vl_taxa_pos { get; set; }
        public string tx_projecao { get; set; }
        public string tp_amortizacao { get; set; }
        public string tp_periodicidade_correcao { get; set; }
        public string tp_unidade_indexador { get; set; }
        public int vl_defasagem_indexador { get; set; }
        public int di_referencia_indexador { get; set; }
        public int me_referencia_indexador { get; set; }
        public string tx_devedor { get; set; }
        public string tp_regime { get; set; }
        public string tp_aniversario { get; set; }
        public bool es_considera_deflacao { get; set; }
        public DateTime dt_ultima_alteracao { get; set; }
        public string tx_cnpj_devedor { get; set; }
        public string tx_cnpj_agente_fiduciario { get; set; }
    }
}