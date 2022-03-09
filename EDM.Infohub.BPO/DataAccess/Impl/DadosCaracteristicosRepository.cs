using Amazon.SecretsManager;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.DataAccess
{
    public class DadosCaracteristicosRepository : SqlRepository<DadosCaracteristicosDAO>
    {
        private ILogger<DadosCaracteristicosRepository> _logger;
        private IMemoryCache _cache;

        public DadosCaracteristicosRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<DadosCaracteristicosRepository> logger, IMemoryCache cache)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
            this._cache = cache;
        }

        public bool BulkInsert(List<DadosCaracteristicosDAO> list)
        {
            List<byte[]> listaHash = new List<byte[]>();

            foreach (var cadastro in list)
            {
                listaHash.Add(cadastro.cd_sna_hash);
            }

            var query = "delete from edm.tb_dados_caracteristicos where cd_sna_hash = ANY(@lista::bytea[])";
            var param = new DynamicParameters();
            param.Add("@lista", listaHash.ToArray());

            try
            {
                Connection.Query<DadosCaracteristicosDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
            }

            using (var writer = Connection.BeginBinaryImport("COPY edm.TB_DADOS_CARACTERISTICOS (CD_SNA, CD_SNA_HASH, TP_EVENTO, DT_CRIACAO, DT_ATUALIZACAO, ES_ATIVO, TP_PAPEL, CD_ISIN, TX_EMISSOR, TX_CNPJ_EMISSOR, DT_EMISSAO, DT_INICIO_RENTABILIDADE, DT_VENCIMENTO, VL_NOMINAL_EMISSAO, TX_INSTRUCAO_CVM, TP_CLEARING, TX_AGENTE_FIDUCIARIO, ES_POSSIBILIDADE_RESGATE_ANTECIPADO, ES_CONVERSIVEL_ACAO, ES_DEBENTURE_INCENTIVADA, TP_CRITERIO_CALCULO_INDEXADOR, TP_CRITERIO_CALCULO_JUROS, TP_INDEXADOR, VL_TAXA_PRE, VL_TAXA_POS, TX_PROJECAO, TP_AMORTIZACAO, TP_PERIODICIDADE_CORRECAO, TP_UNIDADE_INDEXADOR, VL_DEFASAGEM_INDEXADOR, DI_REFERENCIA_INDEXADOR, ME_REFERENCIA_INDEXADOR, TX_DEVEDOR, TP_REGIME, TP_ANIVERSARIO, ES_CONSIDERA_DEFLACAO, DT_ULTIMA_ALTERACAO, TX_CNPJ_DEVEDOR, TX_CNPJ_AGENTE_FIDUCIARIO) FROM STDIN (FORMAT BINARY)"))
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
                        writer.Write(l.cd_isin, NpgsqlDbType.Char);
                        writer.Write(l.tx_emissor, NpgsqlDbType.Char);
                        writer.Write(l.tx_cnpj_emissor, NpgsqlDbType.Char);
                        writer.Write(l.dt_emissao, NpgsqlDbType.Timestamp);
                        writer.Write(l.dt_inicio_rentabilidade, NpgsqlDbType.Timestamp);
                        writer.Write(l.dt_vencimento, NpgsqlDbType.Timestamp);
                        writer.Write(l.vl_nominal_emissao, NpgsqlDbType.Double);
                        writer.Write(l.tx_instrucao_cvm, NpgsqlDbType.Char);
                        writer.Write(l.tp_clearing, NpgsqlDbType.Char);
                        writer.Write(l.tx_agente_fiduciario, NpgsqlDbType.Char);
                        writer.Write(l.es_possibilidade_resgate_antecipado, NpgsqlDbType.Boolean);
                        writer.Write(l.es_conversivel_acao, NpgsqlDbType.Boolean);
                        writer.Write(l.es_debenture_incentivada, NpgsqlDbType.Boolean);
                        writer.Write(l.tp_criterio_calculo_indexador, NpgsqlDbType.Char);
                        writer.Write(l.tp_criterio_calculo_juros, NpgsqlDbType.Char);
                        writer.Write(l.tp_indexador, NpgsqlDbType.Char);
                        writer.Write(l.vl_taxa_pre, NpgsqlDbType.Double);
                        writer.Write(l.vl_taxa_pos, NpgsqlDbType.Double);
                        writer.Write(l.tx_projecao, NpgsqlDbType.Char);
                        writer.Write(l.tp_amortizacao, NpgsqlDbType.Char);
                        writer.Write(l.tp_periodicidade_correcao, NpgsqlDbType.Char);
                        writer.Write(l.tp_unidade_indexador, NpgsqlDbType.Char);
                        writer.Write(l.vl_defasagem_indexador, NpgsqlDbType.Integer);
                        writer.Write(l.di_referencia_indexador, NpgsqlDbType.Smallint);
                        writer.Write(l.me_referencia_indexador, NpgsqlDbType.Smallint);
                        writer.Write(l.tx_devedor, NpgsqlDbType.Char);
                        writer.Write(l.tp_regime, NpgsqlDbType.Char);
                        writer.Write(l.tp_aniversario, NpgsqlDbType.Char);
                        writer.Write(l.es_considera_deflacao, NpgsqlDbType.Boolean);
                        writer.Write(l.dt_ultima_alteracao, NpgsqlDbType.Timestamp);
                        writer.Write(l.tx_cnpj_devedor, NpgsqlDbType.Char);
                        writer.Write(l.tx_cnpj_agente_fiduciario, NpgsqlDbType.Char);
                    }
                    writer.Complete();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Nao foi possivel realizar o Bulk Insert na tabela de Dados Caracteristicos: " + e.Message);
                    throw e;
                }
                finally
                {
                    CloseConnection();
                }
            }

            return true;
        }

        public DadosCaracteristicosDAO GetByHash(byte[] snaHash)
        {
            var query = "select * from edm.tb_dados_caracteristicos where cd_sna_hash = @hash order by dt_criacao desc";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.QueryFirst<DadosCaracteristicosDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new DadosCaracteristicosDAO();
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<DadosCaracteristicosDAO> GetCadastros()
        {
            var query = "select distinct on (cd_sna) * from edm.tb_dados_caracteristicos order by cd_sna, dt_criacao desc";
            var param = new DynamicParameters();

            IEnumerable<DadosCaracteristicosDAO> dadosCadastro;

            try
            {
                if (!_cache.TryGetValue("DADOS_CADASTRO", out dadosCadastro))
                {
                    var connString = Utils.GetSecret(_config, _secret, "Base");
                    NpgsqlConnection conn = new NpgsqlConnection(connString);
                    dadosCadastro = conn.Query<DadosCaracteristicosDAO>(query, param);
                    conn.Close();
                    //Console.WriteLine("criando cache");
                    // Key not in cache, so get data.
                    //Console.WriteLine($"Message Result:{response.MessageResult}");

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                    // Keep in cache for this time, reset time if accessed.
                    //.SetSlidingExpiration(TimeSpan.FromDays(1));
                    //_logger.LogInformation($"Solicitando token do Secure Gateway: {token}");
                    //Console.WriteLine($"Solicitando token do Secure Gateway: {token}");

                    // Save data in cache.
                    _cache.Set("DADOS_CADASTRO", dadosCadastro, cacheEntryOptions);
                }
                else
                {
                    dadosCadastro = _cache.Get<IEnumerable<DadosCaracteristicosDAO>>("DADOS_CADASTRO");
                    //Console.WriteLine($"Pegando do cache: {token}");
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<DadosCaracteristicosDAO>();
            }
            finally
            {
                CloseConnection();
            }

            return dadosCadastro;
        }


        public void ClearCache()
        {
            _cache.Remove("DADOS_CADASTRO");
        }

        internal DateTime GetUltimaAtualizacao()
        {
            const string query = "select dt_criacao from edm.tb_dados_caracteristicos order by dt_criacao desc limit 1";
            var param = new DynamicParameters();
            try
            {
                var connString = Utils.GetSecret(_config, _secret, "Base");
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                var result = conn.QueryFirst<DateTime>(query, param);
                conn.Close();
                return result;
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

        public IEnumerable<DadosCaracteristicosDAO> DadosCaracteristicosAssinados(int pageSize, int pageNumber)
        {
            var offset = pageNumber * pageSize;
            var param = new DynamicParameters();

            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            var query = "SELECT distinct on (d.cd_sna_hash) d.* " +
                        "FROM " +
                        "( " +
                            "SELECT cd_sna_hash " +
                            "FROM edm.tb_assinatura " +
                            "WHERE es_assinado = true " +
                        ") es " +
                        "JOIN edm.tb_dados_caracteristicos d " +
                        "ON es.cd_sna_hash = d.cd_sna_hash " +
                        "order by d.cd_sna_hash, d.dt_criacao desc " +
                        "LIMIT @pageSize OFFSET @page";

            try
            {
                return Connection.Query<DadosCaracteristicosDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<DadosCaracteristicosDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public int CountDadosCaracteristicosAssinados()
        {
            var query = "SELECT count(distinct(d.cd_sna_hash)) " +
                        "FROM " +
                        "( " +
                            "SELECT cd_sna_hash " +
                            "FROM edm.tb_assinatura " +
                            "WHERE es_assinado = true " +
                        ") es " +
                        "JOIN edm.tb_dados_caracteristicos d " +
                        "ON es.cd_sna_hash = d.cd_sna_hash ";

            try
            {
                return Connection.QueryFirst<int>(query);
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

        public DateTime DataInicioRentabilidade(byte[] codSnaHash)
        {
            var query = "SELECT dt_inicio_rentabilidade " +
                        "FROM edm.tb_dados_caracteristicos " +
                        "WHERE cd_sna_hash = @hash";
            var param = new DynamicParameters();
            param.Add("@hash", codSnaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.QueryFirst<DateTime>(query, param);
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
    }
}
