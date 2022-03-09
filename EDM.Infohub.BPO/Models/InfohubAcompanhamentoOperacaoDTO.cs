using Newtonsoft.Json;
using System;

namespace EDM.Infohub.Consumer.Models
{
    public class InfohubAcompanhamentoOperacao
    {
        [JsonProperty(PropertyName = "ID")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "ID_INFOHUB_MENSAGEM")]
        public long IdInfoHubMensagem { get; set; }
        [JsonProperty(PropertyName = "NM_ORIGEM")]
        public char? NmOrigem { get; set; }
        [JsonProperty(PropertyName = "DH_EVENTO")]
        public DateTime DhEvento { get; set; }
        [JsonProperty(PropertyName = "DH_EVENTO_INFOHUB_MENSAGEM")]
        public DateTime DhEventoInfoHubMensagem { get; set; }
        [JsonProperty(PropertyName = "DH_EVENTO_DMZ")]
        public DateTime DhEventoDmz { get; set; }
        [JsonProperty(PropertyName = "TX_CONTEUDO_MENSAGEM")]
        public string TxConteudoMensagem { get; set; }
        [JsonProperty(PropertyName = "ID_TIPO_MENSAGEM")]
        public InfoHubMessageType IdTipoMensagem { get; set; }
        [JsonProperty(PropertyName = "NM_USUARIO")]
        public string NmUsuario { get; set; }
        [JsonProperty(PropertyName = "CD_STATUS")]
        public String CdStatus { get; set; }
        [JsonProperty(PropertyName = "ES_AUTO_CONFIRMACAO")]
        public bool? EsAutoConfirmacao { get; set; }
        [JsonProperty(PropertyName = "NM_MAQUINA")]
        public string NmMaquina { get; set; }
        [JsonProperty(PropertyName = "CD_INSTRUMENTO")]
        public string CdInstrumento { get; set; }
        [JsonProperty(PropertyName = "DATA_LIQUIDACAO")]
        public DateTime DataLiquidacao { get; set; }
        [JsonProperty(PropertyName = "CD_OPERACAO_CETIP")]
        public int CdOperacaoCetip { get; set; }
        [JsonProperty(PropertyName = "CD_TIPO_INSTRUMENTO")]
        public string CdTipoInstrumento { get; set; }
        [JsonProperty(PropertyName = "CD_SITUACAO_CETIP")]
        public int CdSituacaoCetip { get; set; }
    }
}