using Dapper.Contrib.Extensions;
using EDM.Infohub.BPO.Models;
using Newtonsoft.Json.Linq;
using System;

namespace EDM.Infohub.BPO
{

    public class ControleMensagem
    {
        public int IU_PK_TB_Controle_Mensagem { get; set; }
        public DateTime DH_Processamento { get; set; }
        public string TP_Mensagem { get; set; }
        public string CD_ID_Processamento { get; set; }
        public InfohubMessageModel TX_Mensagem { get; set; }
        public StatusClearingEnum ES_Processamento { get; set; }
        public string ES_Processamento_BTG { get; set; }
    }
    [Table("edm.tb_controle_mensagem")]
    public class ControleMensagemDAO
    {
        [Key]
        public int iu_pk_tb_controle_mensagem { get; set; }
        public DateTime dh_processamento { get; set; }
        public string tp_mensagem { get; set; }
        public string cd_id_processamento { get; set; }
        public JObject tx_mensagem { get; set; }
        public StatusClearingEnum es_processamento { get; set; }
        public string es_processamento_btg { get; set; }
    }
}