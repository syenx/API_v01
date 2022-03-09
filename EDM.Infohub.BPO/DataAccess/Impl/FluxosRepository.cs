using Amazon.SecretsManager;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NpgsqlTypes;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.DataAccess
{
    public class FluxosRepository : SqlRepository<FluxosDAO>
    {
        private ILogger<FluxosRepository> _logger;

        public FluxosRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<FluxosRepository> logger)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
        }

        public bool BulkInsert(List<FluxosDAO> list)
        {
            List<byte[]> listaHash = new List<byte[]>();

            foreach (var fluxo in list)
            {
                listaHash.Add(fluxo.cd_sna_hash);
            }

            var query = "delete from edm.tb_fluxos where cd_sna_hash = ANY(@lista::bytea[])";
            var param = new DynamicParameters();
            param.Add("@lista", listaHash.ToArray());
            //string sql = "SELECT * FROM SomeTable WHERE id IN @ids"
            //var results = conn.Query(sql, new { ids = new[] { 1, 2, 3, 4, 5 } });
            try
            {
                Connection.Query<FluxosDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
            }

            using (var writer = Connection.BeginBinaryImport("COPY edm.TB_FLUXOS (CD_SNA, CD_SNA_HASH, TP_PAPEL, DT_BASE, DT_CRIACAO, DT_ATUALIZACAO, ES_ATIVO, DT_LIQUIDACAO, TP_EVENTO, VL_TAXA, VL_AMORTIZACAO) FROM STDIN (FORMAT BINARY)"))
            {
                try
                {
                    foreach (var l in list)
                    {
                        writer.StartRow();
                        writer.Write(l.cd_sna, NpgsqlDbType.Char);
                        writer.Write(l.cd_sna_hash, NpgsqlDbType.Bytea);
                        writer.Write(l.tp_papel, NpgsqlDbType.Smallint);
                        writer.Write(l.dt_base, NpgsqlDbType.Timestamp);
                        writer.Write(l.dt_criacao, NpgsqlDbType.Timestamp);
                        writer.Write(l.dt_atualizacao, NpgsqlDbType.Timestamp);
                        writer.Write(l.es_ativo, NpgsqlDbType.Boolean);
                        writer.Write(l.dt_liquidacao, NpgsqlDbType.Timestamp);
                        writer.Write(l.tp_evento, NpgsqlDbType.Char);
                        writer.Write(l.vl_taxa, NpgsqlDbType.Double);
                        writer.Write(l.vl_amortizacao, NpgsqlDbType.Double);
                    }
                    writer.Complete();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Nao foi possivel realizar o Bulk Insert na tabela de Fluxos: " + e.Message);
                    throw e;
                }
                finally
                {
                    CloseConnection();
                }
            }

            return true;
        }

        public IEnumerable<FluxosDAO> GetByHash(byte[] snaHash, DateTime? data, int pageSize, int pageNumber, DateTime? dataBase = null)
        {
            var offset = pageNumber * pageSize;
            var query = "select * from edm.tb_fluxos where cd_sna_hash = @hash and (@date is null or dt_criacao::DATE = (@date)::DATE) and (@dateBase is null or dt_base::DATE = (@dateBase)::DATE) order by dt_liquidacao limit @pagesize offset @page";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            param.Add("@date", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@dateBase", dataBase, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.Query<FluxosDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<FluxosDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }
        public int CountByHash(byte[] snaHash, DateTime data)
        {
            var query = "select count(*) from edm.tb_fluxos where cd_sna_hash = @hash and dt_criacao::DATE = (@date)::DATE";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            param.Add("@date", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            try
            {
                return Connection.QueryFirst<int>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        internal DateTime GetUltimaAtualizacao(byte[] snaHash)
        {
            const string query = "select dt_criacao from edm.tb_fluxos where cd_sna_hash = @hash order by dt_criacao desc limit 1";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);

            try
            {
                var ultimaData = Connection.QueryFirst<DateTime>(query, param);
                return ultimaData;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new DateTime(0001, 01, 01);
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<FluxosDAO> FluxosAssinados()
        {
            var query = "SELECT f.* " +
                        "FROM " +
                        "( " +
                            "SELECT cd_sna_hash " +
                            "FROM edm.tb_assinatura " +
                            "WHERE es_assinado = true " +
                        ") es " +
                        "JOIN edm.tb_fluxos f " +
                        "ON es.cd_sna_hash = f.cd_sna_hash ";

            try
            {
                return Connection.Query<FluxosDAO>(query);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<FluxosDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }
    }

}
