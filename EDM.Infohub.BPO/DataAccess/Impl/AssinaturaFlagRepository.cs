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
    public class AssinaturaFlagRepository : SqlRepository<AssinaturaFlagDAO>
    {
        private ILogger<AssinaturaFlagRepository> _logger;
        private AssinaturaLogRepository _tblog;
        private IDictionary<string, string> _dict;

        public AssinaturaFlagRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<AssinaturaFlagRepository> logger, AssinaturaLogRepository tblog)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
            this._tblog = tblog;
        }

        public AssinaturaFlagDAO InsertUpdate(AssinaturaFlagDAO item)
        {
            var ConnString = Utils.GetSecret(_config, _secret, "Base");
            var connection = new NpgsqlConnection(ConnString);
            string insertFlag = $@"INSERT INTO edm.TB_ASSINATURA_TIPO_IMPACTO(FK_ASSINATURA, EN_TIPO_IMPACTO, ES_IMPACTADO, dt_atualizacao_flag)
                                        VALUES(@fk_assinatura, @tipo_assinatura::edm.en_tipo_impacto, @flag_assinatura, @dt_atualizacao_flag)
                                        ON CONFLICT ON CONSTRAINT CONST_IMPACTO_PAPEL
                                        DO UPDATE SET ES_IMPACTADO = EXCLUDED.ES_IMPACTADO, dt_atualizacao_flag = EXCLUDED.dt_atualizacao_flag";

            try
            {
                var param = new DynamicParameters();
                param.Add("@fk_assinatura", item.FK_ASSINATURA, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                param.Add("@tipo_assinatura", item.EN_TIPO_IMPACTO, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@flag_assinatura", item.ES_IMPACTADO, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
                param.Add("@dt_atualizacao_flag", item.dt_atualizacao_flag, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

                connection.Query<AssinaturaFlagDAO>(insertFlag, param);
                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                //CloseConnection();
            }

        }

        public AssinaturaDAO Update(AssinaturaDAO item, string usuario)
        {
            try
            {
                Connection.Update<AssinaturaDAO>(item);
                _tblog.Insert(new AssinaturaLogDAO()
                {
                    fk_assinaturas = (int)item.pk_assinaturas,
                    dt_criacao = DateTime.Now,
                    es_assinado = item.es_assinado,
                    cd_cge = item.cd_sna,
                    usuario = usuario,
                    tx_estado = JObject.FromObject(item)
                }); ;
                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

        internal int CountAssinados(string instrumento, bool? impactaMdp, DateTime? dataAssinatura)
        {
            instrumento = "%" + instrumento.Trim().ToUpper() + "%";
            var param = new DynamicParameters();
            param.Add("@like", instrumento, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@mdp", impactaMdp, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@dataAss", dataAssinatura, System.Data.DbType.Date, System.Data.ParameterDirection.Input);

            try
            {
                return Connection.QueryFirst<int>($@"select count(*) from edm.tb_assinatura where es_assinado = true and cd_sna like @like  and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp)", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConnection();
            }
        }

        public AssinaturaDAO GetByHash(byte[] snaHash)
        {
            const string select = "SELECT ASS.*, " +
                                   "bool_or(imp.es_impactado) FILTER(WHERE imp.en_tipo_impacto = 'PRECO') AS impacta_preco," +
                                   "bool_or(imp.es_impactado) FILTER(WHERE imp.en_tipo_impacto = 'CADASTRO') AS impacta_cadastro," +
                                   "bool_or(imp.es_impactado) FILTER(WHERE imp.en_tipo_impacto = 'PU_EVENTO') AS impacta_pu_evento," +
                                   "bool_or(imp.es_impactado) FILTER(WHERE imp.en_tipo_impacto = 'PU_HISTORICO') AS impacta_pu_historico" +
                                   "FROM " +
                                   "edm.tb_assinatura ASS" +
                                   "INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp" +
                                   "ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA " +
                                   "WHERE CD_SNA_HASH = @hash" +
                                   "GROUP BY " +
                                   "    pk_assinaturas,cd_sna" +
                                   "ORDER BY" +
                                   "   cd_sna; ";

            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.QueryFirst<AssinaturaDAO>(select, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new AssinaturaDAO();
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<AssinaturaDAO> GetAllAssinados(int pageSize, int pageNumber, string instrumento, string order, bool? impactaMdp, DateTime? dataAssinatura)
        {
            string query = "";
            if (!_dict.TryGetValue(order, out query))
            {
                query = _dict[""];
            }

            var offset = pageNumber * pageSize;
            instrumento = "%" + instrumento + "%";
            var param = new DynamicParameters();

            param.Add("@like", instrumento, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@mdp", impactaMdp, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@dataAss", dataAssinatura, System.Data.DbType.Date, System.Data.ParameterDirection.Input);
            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.Query<AssinaturaDAO>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<AssinaturaDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<AssinaturaDAO> GetAllAssinados()
        {
            try
            {
                return Connection.Query<AssinaturaDAO>($@"select * from edm.tb_assinatura where es_assinado = true");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<AssinaturaDAO>();
            }
            finally
            {
                CloseConnection();
            }
        }


        //var parameters = new DynamicParameters();
        //parameters.Add("@id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

        //    return Connection.QueryFirst<ResponsavelModel>($@"select top 1 * from tb_responsavel (nolock) 
        //                                                    //where pk_responsavel = @id", parameters);
    }
}
