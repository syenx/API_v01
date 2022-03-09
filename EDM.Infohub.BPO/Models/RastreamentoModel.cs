using Dapper.Contrib.Extensions;
using System;

namespace EDM.Infohub.BPO.Models
{
    [Table("log.tb_rastreamento")]
    public class RastreamentoModel
    {
        public int pk_tb_rastreamento { get; set; }
        public string cd_sna { get; set; }
        public DateTime dh_evento { get; set; }
        public long dh_rank { get; set; }
        public string en_tipo { get; set; }
        public string en_status { get; set; }
        public string tx_erro { get; set; }
        public string nm_login_usuario { get; set; }
    }

    //public enum StatusMensagemEnum
    //{
    //    [NpgsqlTypes.PgName("ENVIADO_LUZ")]
    //    ENVIADO_LUZ = 1,
    //    [NpgsqlTypes.PgName("RECEBIDO_LUZ")]
    //    RECEBIDO_LUZ = 2,
    //    [NpgsqlTypes.PgName("PERSISTIDO_BPO")]
    //    PERSISTIDO_BPO = 3,
    //    [NpgsqlTypes.PgName("ENVIADO_MDP")]
    //    ENVIADO_MDP = 4,
    //    [NpgsqlTypes.PgName("PROCESSADO_MDP")]
    //    PROCESSADO_MDP = 5,
    //    [NpgsqlTypes.PgName("ERRO_MDP")]
    //    ERRO_MDP = 6
    //}

    //public enum TipoLogEnum
    //{
    //    [NpgsqlTypes.PgName("PRECO")]
    //    PRECO = 1,
    //    [NpgsqlTypes.PgName("CADASTRO")]
    //    CADASTRO = 2,
    //    [NpgsqlTypes.PgName("FLUXO")]
    //    FLUXO = 3,
    //    [NpgsqlTypes.PgName("EVENTO")]
    //    EVENTO = 4,
    //    [NpgsqlTypes.PgName("PRECO_HISTORICO")]
    //    PRECO_HISTORICO = 5
    //}
}
