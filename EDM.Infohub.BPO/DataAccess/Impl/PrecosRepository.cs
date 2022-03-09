using Amazon.SecretsManager;
using Dapper;
using EDM.Infohub.BPO.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NpgsqlTypes;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.DataAccess
{
    public class PrecosRepository : SqlRepository<DadosPrecoDAO>
    {
        //private ILogger<PrecosRepository> _logger;

        public PrecosRepository(IConfiguration configuration, IAmazonSecretsManager secret)//,ILogger<PrecosRepository> logger)
        {
            //this._logger = logger;
            this._config = configuration;
            this._secret = secret;
        }

        public bool BulkInsert(List<DadosPrecoDAO> list, DateTime data_evento)
        {

            List<byte[]> listaHash = new List<byte[]>();

            foreach (var fluxo in list)
            {
                listaHash.Add(fluxo.cd_sna_hash);
            }

            var query = "delete from edm.tb_precos where cd_sna_hash = ANY(@lista::bytea[]) and dt_evento::date = @data::date";
            var param = new DynamicParameters();
            param.Add("@lista", listaHash.ToArray());
            param.Add("@data", data_evento, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            try
            {
                Connection.Query<DadosPrecoDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                //_logger.LogWarning(e, e.Message);
            }
            using (var writer = Connection.BeginBinaryImport("COPY edm.TB_PRECOS (CD_SNA, CD_SNA_HASH, TP_EVENTO, DT_CRIACAO, DT_ATUALIZACAO, ES_ATIVO, TP_PAPEL, TP_INDEXADOR, VL_TAXA_POS, VL_TAXA_PRE, DT_EVENTO, VL_NOMINAL_BASE, VL_NOMINAL_ATUALIZADO, VL_FATOR_CORRECAO, VL_FATOR_JUROS, VL_PU_ABERTURA, VL_PAGAMENTOS, VL_PU_FECHAMENTO, VL_PRINCIPAL, VL_INFLACAO, VL_JUROS, VL_INCORPORADO, VL_INCORPORAR, VL_AMORTIZACAO, VL_AMEX, VL_VENCIMENTO, VL_PREMIO, VL_PAGAMENTO_JUROS, VL_PORCENTUAL_AMORTIZADO, VL_PORCENTUAL_JUROS_INCORPORADO, STATUS_PGTO, DT_ATT_STATUS_PGTO) FROM STDIN (FORMAT BINARY)"))
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
                    //_logger.LogError("Nao foi possivel fazer bulk insert na tabela de precos: " + e.Message);
                    throw e;
                }
                finally
                {
                    CloseConnection();
                }
            }

            return true;
        }

        internal DateTime GetUltimaAtualizacao()
        {
            const string query = "select dt_criacao from edm.tb_precos order by dt_criacao desc limit 1";
            var param = new DynamicParameters();
            //param.Add("@DATE", date, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            try
            {
                var impactaMdp = Connection.QueryFirst<DateTime>(query, param);
                return impactaMdp;
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

        internal DateTime GetUltimaAtualizacaoPapel(byte[] snaHash)
        {
            const string query = "select dt_criacao from edm.tb_precos where cd_sna_hash = @hash order by dt_criacao desc limit 1";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.QueryFirst<DateTime>(query, param);
            }
            catch (InvalidOperationException e)
            {
                return new DateTime();
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<DadosPrecoDAO> GetByTimeInterval(byte[] snaHash, int pageSize, int pageNumber, DateTime dateStart, TimeSpan timeStart, DateTime dateEnd, TimeSpan timeEnd)
        {
            var offset = pageNumber * pageSize;

            var newDateStart = dateStart.Date + timeStart;
            var newDateEnd = dateEnd.Date + timeEnd;

            var query = "select * from edm.tb_precos where cd_sna_hash = @hash and dt_criacao between symmetric @dateStart and @dateEnd order by dt_criacao desc limit @pagesize offset @page";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            param.Add("@dateStart", newDateStart, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@dateEnd", newDateEnd, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                //return Connection.QueryFirst<DadosPrecoDAO>(query, param);
                return Connection.Query<DadosPrecoDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
               //return new DadosPrecoDAO();
                return new List<DadosPrecoDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public int CountByTimeInterval(byte[] snaHash, DateTime dateStart, TimeSpan timeStart, DateTime dateEnd, TimeSpan timeEnd)
        {
            var newDateStart = dateStart.Date + timeStart;
            var newDateEnd = dateEnd.Date + timeEnd;

            var query = "select count(*) from edm.tb_precos where cd_sna_hash = @hash and dt_criacao between symmetric @dateStart and @dateEnd ";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            param.Add("@dateStart", newDateStart, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@dateEnd", newDateEnd, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            try
            {
                //return Connection.QueryFirst<DadosPrecoDAO>(query, param);
                return Connection.QueryFirst<int>(query, param);
            }
            catch (InvalidOperationException e)
            {
                //return new DadosPrecoDAO();
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<DadosPrecoDAO> GetByData(DateTime date, int pageSize, int pageNumber)
        {
            var offset = pageNumber * pageSize;
            var query = "select * from edm.tb_precos where dt_criacao::DATE = @date::DATE order by dt_criacao desc limit @pagesize offset @page";
            var param = new DynamicParameters();
            param.Add("@date", date, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            //param.Add("@like", instrumento, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.Query<DadosPrecoDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                return new List<DadosPrecoDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<DadosPrecoDAO> GetFiltered(DateTime date, TimeSpan time, string papel, string tipo, int pageSize = -1, int pageNumber = 0)
        {
            papel = "%" + papel.ToUpper() + "%";
            var query = "select * from edm.tb_precos where cd_sna like @papel and (@type is null or tp_papel = @type) and dt_evento::DATE = @date::DATE and dt_criacao::time >= @time order by dt_criacao desc limit @pagesize offset @page";
            var param = new DynamicParameters();
            param.Add("@time", time, System.Data.DbType.Time, System.Data.ParameterDirection.Input);
            param.Add("@type", tipo, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@date", date, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@papel", papel, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            if (pageSize == -1) //paginacao nao desejada
            {
                query = "select * from edm.tb_precos where cd_sna like @papel and (@type is null or tp_papel = @type) and dt_evento::DATE = @date::DATE and dt_criacao::time >= @time order by dt_criacao desc";
            }
            else
            {
                var offset = pageNumber * pageSize;
                param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            }

            try
            {
                return Connection.Query<DadosPrecoDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                return new List<DadosPrecoDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }
        public DadosPrecoDAO GetByHash(byte[] snaHash)
        {
            var query = "select * from edm.tb_precos where cd_sna_hash = @hash order by dt_criacao desc";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.QueryFirst<DadosPrecoDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                return new DadosPrecoDAO();
            }
            finally
            {
                CloseConnection();
            }
        }

        internal int CountByData(DateTime date)
        {
            var query = "select count(*) from edm.tb_precos where dt_criacao::DATE = @date::DATE";
            var param = new DynamicParameters();
            param.Add("@date", date, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            try
            {
                return Connection.QueryFirst<int>(query, param);
            }
            catch (InvalidOperationException e)
            {
               // //_logger.LogWarning(e, e.Message);
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        internal int CountFiltered(DateTime date, TimeSpan time, string papel, string tipo)
        {
            papel = "%" + papel.ToUpper() + "%";
            var query = "select count(*) from edm.tb_precos where cd_sna like @papel and (@type is null or tp_papel = @type) and dt_evento::DATE = @date::DATE and dt_criacao::time >= @time";
            var param = new DynamicParameters();
            param.Add("@time", time, System.Data.DbType.Time, System.Data.ParameterDirection.Input);
            param.Add("@type", tipo, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@date", date, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@papel", papel, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            try
            {
                return Connection.QueryFirst<int>(query, param);
            }
            catch (InvalidOperationException e)
            {
                //_logger.LogWarning(e, e.Message);
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        internal IEnumerable<string> GetTypes()
        {
            var query = "select distinct(tp_papel) from edm.tb_precos where tp_papel is not null";
            var param = new DynamicParameters();
            try
            {
                return Connection.Query<string>(query, param);
            }
            catch (InvalidOperationException e)
            {
                //_logger.LogWarning(e, e.Message);
                return new List<string>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<DadosPrecoDAO> GetByDataEvento(DateTime date)
        {
            var query = "select * from edm.tb_precos where dt_evento::DATE = @date::DATE";
            var param = new DynamicParameters();
            param.Add("@date", date, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            
            try
            {
                return Connection.Query<DadosPrecoDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                //_logger.LogWarning(e, e.Message);
                return new List<DadosPrecoDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }

        internal int CountFilteredPrecosDatas(DateTime dateReferencia, string papel, string tipo, DateTime dateStart, TimeSpan timeStart, DateTime dateEnd, TimeSpan timeEnd)
        {
            var newDateStart = dateStart.Date + timeStart;
            var newDateEnd = dateEnd.Date + timeEnd;

            papel = "%" + papel.ToUpper() + "%";
            var query = "select count(*) from edm.tb_precos where cd_sna like @papel and (@type is null or tp_papel = @type) and dt_evento::DATE = @date::DATE and dt_criacao between symmetric @dateStart and @dateEnd";
            var param = new DynamicParameters();
            param.Add("@type", tipo, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@date", dateReferencia, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@papel", papel, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@dateStart", newDateStart, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@dateEnd", newDateEnd, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            try
            {
                return Connection.QueryFirst<int>(query, param);
            }
            catch (InvalidOperationException e)
            {
                //_logger.LogWarning(e, e.Message);
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }

        internal IEnumerable<DadosPrecoDAO> GetFilteredPrecosDatas(DateTime dateReferencia, string papel, string tipo, DateTime dateStart, TimeSpan timeStart, DateTime dateEnd, TimeSpan timeEnd, int pageSize = -1, int pageNumber = 0)
        {
            var newDateStart = dateStart.Date + timeStart;
            var newDateEnd = dateEnd.Date + timeEnd;

            papel = "%" + papel.ToUpper() + "%";
            var query = "select * from edm.tb_precos where cd_sna like @papel and (@type is null or tp_papel = @type) and dt_evento::DATE = @date::DATE and dt_criacao between symmetric @dateStart and @dateEnd order by dt_criacao desc limit @pagesize offset @page";
            var param = new DynamicParameters();
            param.Add("@type", tipo, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@date", dateReferencia, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@papel", papel, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@dateStart", newDateStart, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@dateEnd", newDateEnd, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            if (pageSize == -1) //paginacao nao desejada
            {
                query = "select * from edm.tb_precos where cd_sna like @papel and (@type is null or tp_papel = @type) and dt_evento::DATE = @date::DATE and dt_criacao between symmetric @dateStart and @dateEnd order by dt_criacao desc";
            }
            else
            {
                var offset = pageNumber * pageSize;
                param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            }

            try
            {
                return Connection.Query<DadosPrecoDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                //_logger.LogWarning(e, e.Message);
                return new List<DadosPrecoDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}

