using Amazon.SecretsManager;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NpgsqlTypes;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.DataAccess
{
    public class RawDataRepository : SqlRepository<RawDataEventosProcessadosDAO>
    {
        private ILogger<RawDataRepository> _logger;

        public RawDataRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<RawDataRepository> logger)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
        }

        public bool BulkInsert(List<RawDataEventosProcessadosDAO> list)
        {
            using (var writer = Connection.BeginBinaryImport("COPY edm.TB_RAW_DATA_EVENTOS_PROCESSADOS (CD_SNA, CD_SNA_HASH, TP_DADO, TX_JSON, DT_CRIACAO, NR_RANK) FROM STDIN (FORMAT BINARY)"))
            {
                try
                {
                    foreach (var l in list)
                    {
                        writer.StartRow();
                        writer.Write(l.cd_sna, NpgsqlDbType.Char);
                        writer.Write(l.cd_sna_hash, NpgsqlDbType.Bytea);
                        writer.Write(l.tp_dado, NpgsqlDbType.Smallint);
                        writer.Write(l.tx_json, NpgsqlDbType.Jsonb);
                        writer.Write(l.dt_criacao, NpgsqlDbType.Timestamp);
                        writer.Write(l.nr_rank, NpgsqlDbType.Bigint);
                    }
                    writer.Complete();
                }
                catch (Exception e)
                {
                    _logger.LogError("Nao foi possivel fazer bulk insert na tabela raw data: " + e.Message);
                    throw e;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return true;
        }

        public IEnumerable<RawDataFluxosAntigos> GetOldFlow(List<RawDataEventosProcessadosDAO> report, byte[] snaHash, int pageSize, int pageNumber)
        {
            var offset = pageNumber * pageSize;
            List<string> listJson = new List<string>();

            foreach (var flow in report)
            {
                listJson.Add(flow.tx_json.ToString());
            }

            var query = "select distinct(tx_json) as tx_json_str,max(dt_criacao) as dt_criacao from edm.tb_raw_data_eventos_processados where cd_sna_hash = @hash and tp_dado = 3 and tx_json = ANY(@list::jsonb[]) group by tx_json order by max(dt_criacao) desc, tx_json limit @pagesize offset @page";
            var param = new DynamicParameters();
            param.Add("@list", listJson.ToArray());
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            try
            {
                return Connection.Query<RawDataFluxosAntigos>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<RawDataFluxosAntigos>();//new List<RawDataEventosProcessadosDAO>();
            }
            finally
            {
                CloseConnection();
            }

        }

        internal bool GetByIntrumentCode(string codigoSNA, DateTime data)
        {
            data = data.AddDays(-1);
            const string query = "select count(*) from edm.tb_controle_mensagem where (tx_mensagem->> 'instrumentCode') = @IntrumentCode and dh_processamento::date >= @DATE";
            var param = new DynamicParameters();
            param.Add("@IntrumentCode", codigoSNA, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@DATE", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            try
            {
                var count = Connection.QueryFirst<Int32>(query, param);
                return count > 0;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return false;
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
