using Amazon.SecretsManager;
using Dapper;
using EDM.Infohub.BPO.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.DataAccess
{
    public class RastreamentoRepository : SqlRepository<AssinaturaLogDAO>
    {
        private ILogger<RastreamentoRepository> _logger;
        public RastreamentoRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<RastreamentoRepository> logger)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
        }

        public RastreamentoModel Insert(RastreamentoModel item)
        {
            _logger.LogInformation("Adicionando na tabela de rastreamento: " + item.cd_sna);
            try
            {
                var param = new DynamicParameters();
                param.Add("@cd_sna", item.cd_sna, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@dh_evento", item.dh_evento, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                param.Add("@dh_rank", item.dh_rank, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                param.Add("@en_tipo", item.en_tipo, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@en_status", item.en_status, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@tx_erro", item.tx_erro, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@nm_login_usuario", item.nm_login_usuario, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                Connection.Query($@"insert into log.tb_rastreamento (cd_sna, dh_evento, dh_rank, en_tipo, en_status, tx_erro, nm_login_usuario) values
                (@cd_sna, @dh_evento, @dh_rank, @en_tipo::log.en_tipo_log, @en_status::log.en_status_mensagem, @tx_erro, @nm_login_usuario )", param);
                //Connection.Insert<RastreamentoModel>(item);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
            }
            finally
            {
                CloseConnection();
            }
            return item;
        }

        internal object CountImpactosdoDia(DateTime data, string tipoRequisicao)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RastreamentoModel> GetByData(DateTime item)
        {
            //var dataRank = Utils.GenerateRank(item);
            var param = new DynamicParameters();
            param.Add("@Data", item, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.Query<RastreamentoModel>($@"select * from log.tb_rastreamento where dh_evento::DATE = @Data::DATE order by dh_rank desc", param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<RastreamentoModel>();
            }
            finally
            {
                CloseConnection();
            }

        }
    }
}