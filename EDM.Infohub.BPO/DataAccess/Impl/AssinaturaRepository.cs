using Amazon.SecretsManager;
using Dapper;
using Dapper.Contrib.Extensions;
using EDM.Infohub.BPO.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.DataAccess
{
    public class AssinaturaRepository : SqlRepository<AssinaturaDAO>
    {
        private ILogger<AssinaturaRepository> _logger;
        private AssinaturaLogRepository _tblog;
        private AssinaturaFlagRepository assinaturaFlagRepository;
        private IDictionary<string, string> _dict;
        private IMemoryCache _cache;

        public AssinaturaRepository(IConfiguration configuration, IAmazonSecretsManager secret, ILogger<AssinaturaRepository> logger, AssinaturaLogRepository tblog, AssinaturaFlagRepository assinaturaFlagRepository, IMemoryCache cache)
        {
            this._logger = logger;
            this._config = configuration;
            this._secret = secret;
            this._cache = cache;
            this.assinaturaFlagRepository = assinaturaFlagRepository;
            this._tblog = tblog;

            this._dict = new Dictionary<string, string>()
             {
                {"", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) GROUP BY pk_assinaturas, cd_sna ORDER BY dt_atualizacao) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{ "","select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by dt_atualizacao limit @pageSize offset @page"},
                {"papel", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY cd_sna) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"papel","select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by cd_sna limit @pageSize offset @page"},
                {"-papel", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY cd_sna desc ) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"-papel","select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by cd_sna desc limit @pageSize offset @page"},
                {"dataAssinatura", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY dt_assinatura) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"dataAssinatura", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by dt_assinatura limit @pageSize offset @page"},
                {"-dataAssinatura", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY dt_assinatura desc) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"-dataAssinatura", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by dt_assinatura desc limit @pageSize offset @page"},
                {"impactaPreco", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY impacta_preco) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"impactaMdp", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by impacta_mdp limit @pageSize offset @page"},
                {"-impactaPreco", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY impacta_preco desc) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"-impactaMdp", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by impacta_mdp desc limit @pageSize offset @page"}
                {"impactaCadastro", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY impacta_cadastro) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"impactaMdp", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by impacta_mdp limit @pageSize offset @page"},
                {"-impactaCadastro", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY impacta_cadastro desc) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"-impactaMdp", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by impacta_mdp desc limit @pageSize offset @page"}
                {"impactaEvento", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY impacta_pu_evento) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"impactaMdp", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by impacta_mdp limit @pageSize offset @page"},
                {"-impactaEvento", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY impacta_pu_evento desc) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"-impactaMdp", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by impacta_mdp desc limit @pageSize offset @page"}
                {"impactaHistorico", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss)   GROUP BY pk_assinaturas, cd_sna ORDER BY impacta_pu_historico) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"impactaMdp", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by impacta_mdp limit @pageSize offset @page"},
                {"-impactaHistorico", "Select * from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) GROUP BY pk_assinaturas, cd_sna ORDER BY impacta_pu_historico desc) assinados where (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) limit @pageSize offset @page" },
                //{"-impactaMdp", "select * from edm.tb_assinatura where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp is null or impacta_mdp = @mdp) order by impacta_mdp desc limit @pageSize offset @page"}
            };
        }

        public (AssinaturaDAO, AssinaturaLogDAO) Insert(AssinaturaDAO item, AssinaturaObject impactos, string usuario)
        {
            var ConnString = Utils.GetSecret(_config, _secret, "Base");
            var connection = new NpgsqlConnection(ConnString);
            try
            {                
                var id = connection.Insert<AssinaturaDAO>(item);
                var itemJObject = JObject.FromObject(item);
                var impactoJObject = JObject.FromObject(impactos);
                itemJObject.Merge(impactoJObject);

                //_tblog.Insert(new AssinaturaLogDAO()
                //{
                //    fk_assinaturas = (int)id,
                //    dt_criacao = DateTime.Now,
                //    es_assinado = item.es_assinado,
                //    cd_cge = item.cd_sna,
                //    usuario = usuario,
                //    tx_estado = itemJObject
                //});
                item.pk_assinaturas = (int)id;
                return (item,
                    new AssinaturaLogDAO()
                    {
                        fk_assinaturas = (int)id,
                        dt_criacao = DateTime.Now,
                        es_assinado = item.es_assinado,
                        cd_cge = item.cd_sna,
                        usuario = usuario,
                        tx_estado = itemJObject
                    });
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                ClearCache(TipoImpactoEnum.CADASTRO);
                ClearCache(TipoImpactoEnum.PRECO);
                ClearCache(TipoImpactoEnum.PU_EVENTO);
                ClearCache(TipoImpactoEnum.PU_HISTORICO);
                //CloseConnection();
            }

        }

        public (AssinaturaDAO, AssinaturaLogDAO) Update(AssinaturaDAO item, AssinaturaObject impactos, string usuario)
        {
            var ConnString = Utils.GetSecret(_config, _secret, "Base");
            var connection = new NpgsqlConnection(ConnString);
            try
            {
                connection.Update<AssinaturaDAO>(item);
                var itemJObject = JObject.FromObject(item);
                var impactoJObject = JObject.FromObject(impactos);
                itemJObject.Merge(impactoJObject);
                //_tblog.Insert(new AssinaturaLogDAO()
                //{
                //    fk_assinaturas = (int)item.pk_assinaturas,
                //    dt_criacao = DateTime.Now,
                //    es_assinado = item.es_assinado,
                //    cd_cge = item.cd_sna,
                //    usuario = usuario,
                //    tx_estado = itemJObject
                //}); ;
                return (item,
                    new AssinaturaLogDAO()
                    {
                        fk_assinaturas = (int)item.pk_assinaturas,
                        dt_criacao = DateTime.Now,
                        es_assinado = item.es_assinado,
                        cd_cge = item.cd_sna,
                        usuario = usuario,
                        tx_estado = itemJObject
                    });
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                ClearCache(TipoImpactoEnum.CADASTRO);
                ClearCache(TipoImpactoEnum.PRECO);
                ClearCache(TipoImpactoEnum.PU_EVENTO);
                ClearCache(TipoImpactoEnum.PU_HISTORICO);
                //CloseConnection();
            }
        }

        internal int CountAssinados(string instrumento, bool? impactaPreco, bool? impactaCadastro, bool? impactaEvento, bool? impactaHistorico, DateTime? dataAssinatura)
        {
            instrumento = "%" + instrumento.Trim().ToUpper() + "%";
            var param = new DynamicParameters();
            param.Add("@like", instrumento, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@mdp_preco", impactaPreco, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@mdp_cadastro", impactaCadastro, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@mdp_evento", impactaEvento, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@mdp_historico", impactaHistorico, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@dataAss", dataAssinatura, System.Data.DbType.Date, System.Data.ParameterDirection.Input);

            try
            {


                return Connection.QueryFirst<int>($@"select count(*) from (SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA GROUP BY pk_assinaturas,cd_sna) assinados where es_assinado = true and cd_sna like @like and (@dataAss is null or dt_assinatura::date = @dataAss) and (@mdp_preco is null or impacta_preco = @mdp_preco) and (@mdp_cadastro is null or impacta_cadastro = @mdp_cadastro) and (@mdp_evento is null or impacta_pu_evento = @mdp_evento) and (@mdp_historico is null or impacta_pu_historico = @mdp_historico) ", param);
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
            var ConnString = Utils.GetSecret(_config, _secret, "Base");
            var connection = new NpgsqlConnection(ConnString);
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            try
            {
                return connection.QueryFirst<AssinaturaDAO>($@"select * from edm.tb_assinatura where cd_sna_hash = @hash", param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new AssinaturaDAO();
            }
            finally
            {
                connection.Close();
                //CloseConnection();
            }
        }

        public AssinaturaFlag GetByHashWithFlags(byte[] snaHash)
        {
            var ConnString = Utils.GetSecret(_config, _secret, "Base");
            var connection = new NpgsqlConnection(ConnString);
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            try
            {
                return connection.QueryFirst<AssinaturaFlag>($@"SELECT ASS.*, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PRECO') AS impacta_preco, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='CADASTRO') AS impacta_cadastro, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_EVENTO') AS impacta_pu_evento, bool_or(imp.es_impactado) FILTER (WHERE imp.en_tipo_impacto='PU_HISTORICO') AS impacta_pu_historico FROM edm.tb_assinatura ASS INNER JOIN edm.TB_ASSINATURA_TIPO_IMPACTO imp ON ASS.PK_ASSINATURAS = IMP.FK_ASSINATURA where cd_sna_hash = @hash GROUP BY pk_assinaturas, cd_sna", param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new AssinaturaFlag();
            }
            finally
            {
                connection.Close();
                //CloseConnection();
            }
        }

        internal bool PapelImpactado(byte[] snaHash, TipoImpactoEnum flag)
        {
            var teste = flag.ToString();
            const string query = "select ES_IMPACTADO from edm.TB_ASSINATURA_TIPO_IMPACTO imp inner join edm.tb_assinatura a on fk_assinatura = pk_assinaturas where imp.EN_TIPO_IMPACTO = @tipoImpacto::edm.en_tipo_impacto and a.cd_sna_hash = @hash";
            var param = new DynamicParameters();
            param.Add("@hash", snaHash, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            param.Add("@tipoImpacto", teste, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            try
            {
                var impactaMdp = Connection.QueryFirst<bool>(query, param);
                return impactaMdp;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        internal IEnumerable<AssinaturaFlag> GetImpactados(TipoImpactoEnum flag)
        {
            var flagType = flag.ToString();
            const string query = "select * from edm.TB_ASSINATURA_TIPO_IMPACTO imp inner join edm.tb_assinatura a on fk_assinatura = pk_assinaturas where imp.EN_TIPO_IMPACTO = @tipoImpacto::edm.en_tipo_impacto and es_assinado = true and es_impactado = true";

            IEnumerable<AssinaturaFlag> assinaturaFlags;
            try
            {

                if (!_cache.TryGetValue("DADOS_IMPACTADOS_" + flagType, out assinaturaFlags))
                {
                    //logica de pagar os dados
                    var param = new DynamicParameters();
                    param.Add("@tipoImpacto", flagType, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                    var connString = Utils.GetSecret(_config, _secret, "Base");
                    NpgsqlConnection conn = new NpgsqlConnection(connString);
                    assinaturaFlags = conn.Query<AssinaturaFlag>(query, param);
                    conn.Close();
                    //Console.WriteLine($"Message Result:{response.MessageResult}");

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
                    // Keep in cache for this time, reset time if accessed.
                    //.SetSlidingExpiration(TimeSpan.FromDays(1));
                    //_logger.LogInformation($"Solicitando token do Secure Gateway: {token}");
                    //Console.WriteLine($"Solicitando token do Secure Gateway: {token}");

                    // Save data in cache.
                    _cache.Set("DADOS_IMPACTADOS_" + flagType, assinaturaFlags, cacheEntryOptions);
                }
                else
                {
                    assinaturaFlags = _cache.Get<IEnumerable<AssinaturaFlag>>("DADOS_IMPACTADOS_" + flagType);
                    //Console.WriteLine($"Pegando do cache: {token}");
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<AssinaturaFlag>();
            }
            finally
            {
                CloseConnection();
            }
            return assinaturaFlags;
        }

        public void ClearCache(TipoImpactoEnum flag)
        {
            _cache.Remove("DADOS_IMPACTADOS_" + flag.ToString());
        }

        public IEnumerable<AssinaturaFlag> GetAllAssinados(int pageSize, int pageNumber, string instrumento, string order, bool? impactaPreco, bool? impactaCadastro, bool? impactaEvento, bool? impactaHistorico, DateTime? dataAssinatura)
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
            param.Add("@mdp_preco", impactaPreco, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@mdp_cadastro", impactaCadastro, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@mdp_evento", impactaEvento, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@mdp_historico", impactaHistorico, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@dataAss", dataAssinatura, System.Data.DbType.Date, System.Data.ParameterDirection.Input);
            param.Add("@pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@page", offset, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                return Connection.Query<AssinaturaFlag>(query, param);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<AssinaturaFlag>();
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

        public IEnumerable<AssinaturaFlag> GetFlagsModif(TipoImpactoEnum flagType, DateTime dataRelatorio)
        {
            var param = new DynamicParameters();
            param.Add("@tipoImpacto", flagType.ToString(), System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@dataLastRelatorio", dataRelatorio, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            try
            {
                var connString = Utils.GetSecret(_config, _secret, "Base");
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                var assflagsModif = conn.Query<AssinaturaFlag>($@"select * from edm.TB_ASSINATURA_TIPO_IMPACTO imp inner join edm.tb_assinatura a on fk_assinatura = pk_assinaturas where imp.EN_TIPO_IMPACTO = @tipoImpacto::edm.en_tipo_impacto and es_assinado = true and es_impactado = true and dt_atualizacao_flag > @dataLastRelatorio", param);
                conn.Close();
                return assflagsModif;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e, e.Message);
                return new List<AssinaturaFlag>();
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
