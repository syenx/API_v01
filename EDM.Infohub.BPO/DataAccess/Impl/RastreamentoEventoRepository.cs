using Amazon.SecretsManager;
using AutoMapper;
using Dapper;
using EDM.Infohub.BPO.Models.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDM.Infohub.BPO.DataAccess.Impl
{
    public class RastreamentoEventoRepository : SqlRepository<RastreamentoEventoDAO>
    {
        private ILogger<RastreamentoEventoRepository> _logger;
        private readonly IMapper _mapper;

        public RastreamentoEventoRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<RastreamentoEventoRepository> logger, IMapper mapper)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
            _mapper = mapper;
        }

        public IEnumerable<RastreamentoEventoPortal> GetAllEventos(DateTime data, string tipoRequisicao, string statusEvento, string usuario, int pageSize, int pageNumber)
        {
            tipoRequisicao = tipoRequisicao.ToUpper();
            if (usuario != "")
                usuario = "%" + usuario + "%";
            var query = BuildQuery(tipoRequisicao, statusEvento, usuario);
            var param = new DynamicParameters();
            param.Add("@data", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@tipo", tipoRequisicao, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@status", statusEvento, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@usuario", usuario, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", pageNumber * pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                var result = Connection.Query<RastreamentoEventoDAO>(query, param);
                var listaEventos = _mapper.Map<List<RastreamentoEventoPortal>>(result);
                return listaEventos;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<RastreamentoEventoPortal>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public int CountAllEventos(DateTime data, string tipoRequisicao, string statusEvento, string usuario)
        {
            tipoRequisicao = tipoRequisicao.ToUpper();
            if (usuario != "")
                usuario = "%" + usuario + "%";
            var query = BuildQuery(tipoRequisicao, statusEvento, usuario, true);
            var param = new DynamicParameters();
            param.Add("@data", data, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@tipo", tipoRequisicao, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@status", statusEvento, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@usuario", usuario, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            try
            {
                var listaEventos = Connection.QueryFirst<int>(query, param);
                return listaEventos;
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

        internal string BuildQuery(string tipoRequisicao, string statusEvento, string usuario, bool count = false)
        {
            string query;
            if(count)
                query = "select count(*) from log.tb_rastreamento_evento where length(id_requisicao) = 16 and dh_inicio_evento::DATE = @data::DATE";
            else
                query = "select * from log.tb_rastreamento_evento where length(id_requisicao) = 16 and dh_inicio_evento::DATE = @data::DATE";
            if (tipoRequisicao != "")
                query = query + " and en_tipo_requisicao = @tipo::log.en_tipo_requisicao";
            if (statusEvento != "")
                query = query + " and en_status = @status::log.en_status_processamento";
            if (usuario != "")
                query = query + " and nm_login_usuario like @usuario";
            if(!count)
                query = query + " order by dh_rank desc limit @pageSize offset @page";
            return query;
        }
    }
}
