using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace EDM.Infohub.BPO.Models.SQS
{
    public class RastreamentoEvento
    {

        [JsonProperty(PropertyName = "id_requisicao")]
        public Guid IdRequisicao { get; set; }
        [JsonProperty(PropertyName = "en_tipo_requisicao")]
        public string TipoRequisicao { get; set; }
        [JsonProperty(PropertyName = "dh_inicio_evento")]
        public DateTime DataInicioEvento { get; set; }
        [JsonProperty(PropertyName = "dh_final_evento")]
        public DateTime? DataFimEvento { get; set; }
        [JsonProperty(PropertyName = "metodo")]
        public string Metodo { get; set; }
        [JsonProperty(PropertyName = "en_status")]
        public string StatusEvento { get; set; }
        [JsonProperty(PropertyName = "tx_evento")]
        public JObject JsonEvento { get; set; }
        [JsonProperty(PropertyName = "nm_login_usuario")]
        public string Usuario { get; set; }
    }

    [Table("log.tb_rastreamento_evento")]
    public class RastreamentoEventoDAO
    {
        [Key]
        public byte[] id_requisicao { get; set; }
        public string en_tipo_requisicao { get; set; }
        public DateTime dh_inicio_evento { get; set; }
        public DateTime? dh_final_evento { get; set; }
        public long dh_rank { get; set; }
        public string metodo { get; set; }
        public string en_status { get; set; }
        public JObject tx_evento { get; set; } = JObject.FromObject(new { message = "", erro = "" });
        public string nm_login_usuario { get; set; } = "-";
    }

    public class RastreamentoEventoCSV
    {
        public string TipoRequisicao { get; set; }
        public DateTime DataInicioEvento { get; set; }
        public DateTime? DataFimEvento { get; set; }
        public string StatusEvento { get; set; }
        public string Usuario { get; set; }
    }

    public class RastreamentoEventoPortal
    {
        public Guid IdRequisicao { get; set; }
        public string TipoRequisicao { get; set; }
        public DateTime DataInicioEvento { get; set; }
        public DateTime? DataFimEvento { get; set; }
        public string Metodo { get; set; }
        public string StatusEvento { get; set; }
        public string InfoEventoErro { get; set; } = "Não houve erro";
        public string Usuario { get; set; }
    }
    public class JsonEvento
    {
        public string Message { get; set; }
        public string Erro { get; set; }
    }

    public enum TipoRequisicaoEnum
    {
        [NpgsqlTypes.PgName("PRECO")]
        PRECO = 1,
        [NpgsqlTypes.PgName("CADASTRO")]
        CADASTRO = 2,
        [NpgsqlTypes.PgName("FLUXO")]
        FLUXO = 3,
        [NpgsqlTypes.PgName("EVENTO")]
        EVENTO = 4,
        [NpgsqlTypes.PgName("PRECO_HISTORICO")]
        PRECO_HISTORICO = 5
    }

    
}











