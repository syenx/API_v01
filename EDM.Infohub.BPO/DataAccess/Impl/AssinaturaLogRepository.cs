using Amazon.SecretsManager;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.DataAccess
{
    public class AssinaturaLogRepository : SqlRepository<AssinaturaLogDAO>
    {
        private ILogger<AssinaturaLogRepository> _logger;
        public AssinaturaLogRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<AssinaturaLogRepository> logger)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
        }

        public AssinaturaLogDAO Insert(AssinaturaLogDAO item)
        {
            _logger.LogInformation("adicionando na tabela de log: " + item.cd_cge);
            var teste = item.tx_estado;
            var ConnString = Utils.GetSecret(_config, _secret, "Base");
            var connection = new NpgsqlConnection(ConnString);
            SqlMapper.AddTypeHandler(typeof(JObject), JsonHandler.Instance);
            try
            {
                connection.Insert<AssinaturaLogDAO>(item);
                //Connection.Insert<AssinaturaLogDAO>(item);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
            }
            finally
            {
                connection.Close();
                //CloseConnection();
            }
            return item;
        }

        public IEnumerable<AssinaturaLogDAO> GetLogPapel(string item)
        {
            item = item.Trim().ToUpper();
            var param = new DynamicParameters();
            param.Add("@PAPEL", item, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.Query<AssinaturaLogDAO>($@"select * from edm.tb_assinatura_log where cd_cge = @PAPEL order by dt_criacao desc limit 5", param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<AssinaturaLogDAO>();
            }
            finally
            {
                CloseConnection();
            }

        }

        public IEnumerable<AssinaturaLogDAO> GetLastLogPapeis()
        {
            try
            {
                var connString = Utils.GetSecret(_config, _secret, "Base");
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                var result = conn.Query<AssinaturaLogDAO>($@"select distinct on (cd_cge) * from edm.tb_assinatura_log order by cd_cge, dt_criacao desc");
                conn.Close();
                return result;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<AssinaturaLogDAO>();
            }
            finally
            {
                CloseConnection();
            }

        }
    }
}