using Dapper.Contrib.Extensions;
using Newtonsoft.Json.Linq;
using System;

namespace EDM.Infohub.BPO
{
    public class RawDataEventosProcessados
    {
        public int PK_Eventos_Processados { get; set; }
        public string CD_Sna { get; set; }
        public byte[] CD_Sna_Hash { get; set; }
        public int TP_Dado { get; set; }
        public JObject TX_Json { get; set; }
        public DateTime DT_Criacao { get; set; }
        public Int64 NR_Rank { get; set; }
    }
    [Table("edm.tb_raw_data_eventos_processados")]
    public class RawDataEventosProcessadosDAO
    {
        [Key]
        public int pk_eventos_processados { get; set; }
        public string cd_sna { get; set; }
        public byte[] cd_sna_hash { get; set; }
        public int tp_dado { get; set; }
        public JObject tx_json { get; set; }
        public DateTime dt_criacao { get; set; }
        public Int64 nr_rank { get; set; }
    }

    public class RawDataFluxosAntigos
    {
        public string tx_json_str { get; set; }
        public DateTime dt_criacao { get; set; }
    }
}