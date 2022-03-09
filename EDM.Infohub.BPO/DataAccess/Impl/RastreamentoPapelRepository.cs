using Amazon.SecretsManager;
using AutoMapper;
using Dapper;
using EDM.Infohub.BPO.Models.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.DataAccess.Impl
{
    public class RastreamentoPapelRepository : SqlRepository<RastreamentoPapelDAO>
    {
        private ILogger<RastreamentoPapelRepository> _logger;
        private readonly IMapper _mapper;

        public RastreamentoPapelRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<RastreamentoPapelRepository> logger, IMapper mapper)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
            _mapper = mapper;
        }

        public IEnumerable<RastreamentoPapel>  GetAllPapeis(DateTime data, byte[] idRequisicao, string codSna, string statusMensagem, int pageSize, int pageNumber)
        {
            if (codSna != "")
                codSna = "%" + codSna.ToUpper() + "%";
            var query = BuildQuery(codSna, statusMensagem);
            var param = new DynamicParameters();
            param.Add("@data", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@requisicao", idRequisicao, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            param.Add("@sna", codSna, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@status", statusMensagem, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", pageNumber * pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                var result = Connection.Query<RastreamentoPapelDAO>(query, param);
                var listaPapeis = _mapper.Map<List<RastreamentoPapel>>(result);
                return listaPapeis;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<RastreamentoPapel>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public int CountImpactosdoDia(DateTime data, string tipoRequisicao)
        {
            var query = $@"select count(distinct(cd_sna)) from log.tb_rastreamento_log 
                            where dt_evento::date = @data::date
                            and cd_sna not like '%L-%'
                            and en_tipo = @tipoRequisicao::log.en_tipo_log
                            and en_status = 'PROCESSADO_MDP'::log.en_status_mensagem";
            var param = new DynamicParameters();
            param.Add("@data", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@tipoRequisicao", tipoRequisicao, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            try
            {
                var listaPapeis = Connection.QueryFirst<int>(query, param);
                return listaPapeis;
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

        public int CountAllPapeis(DateTime data, byte[] idRequisicao, string codSna, string statusPapel)
        {
            if (codSna != "")
                codSna = "%" + codSna.ToUpper() + "%";
            var query = BuildQuery(codSna, statusPapel, true);
            var param = new DynamicParameters();
            param.Add("@data", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@requisicao", idRequisicao, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            param.Add("@sna", codSna, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@status", statusPapel, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            try
            {
                var listaPapeis = Connection.QueryFirst<int>(query, param);
                return listaPapeis;
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

        internal string BuildQuery(string codSna, string statusPapel, bool count = false)
        {
            string query;
            if(count)
                query = "select count(*) from log.tb_rastreamento where dh_inicio_evento = @data and fk_id_requisicao = @requisicao";
            else
                query = "select * from log.tb_rastreamento where dh_inicio_evento = @data and fk_id_requisicao = @requisicao";
            if (codSna != "")
                query = query + " and cd_sna like @sna";
            if (statusPapel != "")
                query = query + " and en_status = @status::log.en_status_mensagem";
            if (!count)
                query = query + " order by dh_rank desc limit @pageSize offset @page";
            return query;
        }
    }
}
