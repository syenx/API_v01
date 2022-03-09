using Amazon.SecretsManager;
using Dapper;
using EDM.Infohub.BPO.Models.DadosAtuais;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.DataAccess.Impl
{
    public class PuDeEventosRepository : SqlRepository<PuDeEventosDAO>
    {
        private ILogger<PuDeEventosRepository> _logger;

        public PuDeEventosRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<PuDeEventosRepository> logger)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
        }

        public bool BulkInsert(List<PuDeEventosDAO> list, DateTime data_evento)
        {

            List<byte[]> listaHash = new List<byte[]>();

            foreach (var fluxo in list)
            {
                listaHash.Add(fluxo.cd_sna_hash);
            }

            var query = "delete from edm.tb_pu_de_eventos where cd_sna_hash = ANY(@lista::bytea[]) and dt_evento::date = @data::date";
            var param = new DynamicParameters();
            param.Add("@lista", listaHash.ToArray());
            param.Add("@data", data_evento, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            try
            {
                Connection.Query<PuDeEventosDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
            }
            using (var writer = Connection.BeginBinaryImport("COPY edm.tb_pu_de_eventos (CD_SNA, CD_SNA_HASH, TP_EVENTO, DT_CRIACAO, DT_ATUALIZACAO, ES_ATIVO, TP_PAPEL, TP_INDEXADOR, VL_TAXA_POS, VL_TAXA_PRE, DT_EVENTO, VL_NOMINAL_BASE, VL_NOMINAL_ATUALIZADO, VL_FATOR_CORRECAO, VL_FATOR_JUROS, VL_PU_ABERTURA, VL_PAGAMENTOS, VL_PU_FECHAMENTO, VL_PRINCIPAL, VL_INFLACAO, VL_JUROS, VL_INCORPORADO, VL_INCORPORAR, VL_AMORTIZACAO, VL_AMEX, VL_VENCIMENTO, VL_PREMIO, VL_PAGAMENTO_JUROS, VL_PORCENTUAL_AMORTIZADO, VL_PORCENTUAL_JUROS_INCORPORADO, STATUS_PGTO, DT_ATT_STATUS_PGTO) FROM STDIN (FORMAT BINARY)"))
            {
                try
                {
                    foreach (var l in list)
                    {
                        writer.StartRow();
                        writer.Write(l.cd_sna, NpgsqlDbType.Char);
                        writer.Write(l.cd_sna_hash, NpgsqlDbType.Bytea);
                        writer.Write(l.tp_evento, NpgsqlDbType.Smallint);
                        writer.Write(l.dt_criacao, NpgsqlDbType.Timestamp);
                        writer.Write(l.dt_atualizacao, NpgsqlDbType.Timestamp);
                        writer.Write(l.es_ativo, NpgsqlDbType.Boolean);
                        writer.Write(l.tp_papel, NpgsqlDbType.Char);
                        writer.Write(l.tp_indexador, NpgsqlDbType.Char);
                        writer.Write(l.vl_taxa_pos, NpgsqlDbType.Double);
                        writer.Write(l.vl_taxa_pre, NpgsqlDbType.Double);
                        writer.Write(l.dt_evento, NpgsqlDbType.Timestamp);
                        writer.Write(l.vl_nominal_base, NpgsqlDbType.Double);
                        writer.Write(l.vl_nominal_atualizado, NpgsqlDbType.Double);
                        writer.Write(l.vl_fator_correcao, NpgsqlDbType.Double);
                        writer.Write(l.vl_fator_juros, NpgsqlDbType.Double);
                        writer.Write(l.vl_pu_abertura, NpgsqlDbType.Double);
                        writer.Write(l.vl_pagamentos, NpgsqlDbType.Double);
                        writer.Write(l.vl_pu_fechamento, NpgsqlDbType.Double);
                        writer.Write(l.vl_principal, NpgsqlDbType.Double);
                        writer.Write(l.vl_inflacao, NpgsqlDbType.Double);
                        writer.Write(l.vl_juros, NpgsqlDbType.Double);
                        writer.Write(l.vl_incorporado, NpgsqlDbType.Double);
                        writer.Write(l.vl_incorporar, NpgsqlDbType.Integer);
                        writer.Write(l.vl_amortizacao, NpgsqlDbType.Double);
                        writer.Write(l.vl_amex, NpgsqlDbType.Double);
                        writer.Write(l.vl_vencimento, NpgsqlDbType.Double);
                        writer.Write(l.vl_premio, NpgsqlDbType.Double);
                        writer.Write(l.vl_pagamento_juros, NpgsqlDbType.Double);
                        writer.Write(l.vl_porcentual_amortizado, NpgsqlDbType.Double);
                        writer.Write(l.vl_porcentual_juros_incorporado, NpgsqlDbType.Double);
                        writer.Write(l.status_pgto, NpgsqlDbType.Char);
                        writer.Write(l.dt_att_status_pgto, NpgsqlDbType.Timestamp);
                    }
                    writer.Complete();
                }
                catch (Exception e)
                {
                    _logger.LogError("Nao foi possivel fazer bulk insert na tabela de precos: " + e.Message);
                    throw e;
                }
                finally
                {
                    CloseConnection();
                }
            }

            return true;
        }

        internal IEnumerable<PuDeEventosDAO> GetByData(DateTime data)
        {
            const string query = "select * from edm.tb_pu_de_eventos where dt_evento::date = @data::date";
            var param = new DynamicParameters();
            param.Add("@data", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.Query<PuDeEventosDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

        internal PuDeEventosDAO GetPapelByData(DateTime data, byte[] snaHash)
        {
            const string query = "select * from edm.tb_pu_de_eventos where dt_evento::date = @data::date and cd_sna_hash = @hash";
            var param = new DynamicParameters();
            param.Add("@data", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.QueryFirstOrDefault<PuDeEventosDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
