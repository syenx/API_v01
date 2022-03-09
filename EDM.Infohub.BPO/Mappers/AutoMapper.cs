using AutoMapper;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.Assinatura;
using EDM.Infohub.BPO.Models.DadosAtuais;
using EDM.Infohub.BPO.Models.Response;
using EDM.Infohub.BPO.Models.SQS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace EDM.Infohub.BPO.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {           
            CreateMap<DadosPrecoLuz, RawDataEventosProcessadosDAO>()
                .ForMember(dest =>
                    dest.cd_sna,
                    opt => opt.MapFrom(src => src.CodigoSNA))
                .ForMember(dest =>
                    dest.cd_sna_hash,
                    opt => opt.MapFrom(src => Utils.GenerateHash(src.CodigoSNA)))
                .ForMember(dest =>
                    dest.tp_dado,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest =>
                    dest.tx_json,
                    opt => opt.MapFrom(src => JObject.Parse(JsonConvert.SerializeObject(src))))
                .ForMember(dest =>
                    dest.dt_criacao,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest =>
                    dest.nr_rank,
                    opt => opt.MapFrom(src => Utils.GenerateRank(DateTime.Now)));

            CreateMap<DadosPrecoLuz, DadosPrecoDAO>()
                    .ForMember(dest =>
                        dest.cd_sna,
                        opt => opt.MapFrom(src => src.CodigoSNA))
                    .ForMember(dest =>
                        dest.cd_sna_hash,
                        opt => opt.MapFrom(src => Utils.GenerateHash(src.CodigoSNA)))
                    .ForMember(dest =>
                        dest.tp_evento,
                        opt => opt.MapFrom(src => 1))
                    .ForMember(dest =>
                        dest.dt_criacao,
                        opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(dest =>
                        dest.dt_atualizacao,
                        opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(dest =>
                        dest.es_ativo,
                        opt => opt.MapFrom(src => true))
                    .ForMember(dest =>
                        dest.tp_papel,
                        opt => opt.MapFrom(src => src.Tipo))
                    .ForMember(dest =>
                        dest.tp_indexador,
                        opt => opt.MapFrom(src => src.Indexador))
                    .ForMember(dest =>
                        dest.vl_taxa_pos,
                        opt => opt.MapFrom(src => src.TaxaPos))
                    .ForMember(dest =>
                        dest.vl_taxa_pre,
                        opt => opt.MapFrom(src => src.TaxaPre))
                    .ForMember(dest =>
                        dest.dt_evento,
                        opt => opt.MapFrom(src => src.DataEvento))
                    .ForMember(dest =>
                        dest.vl_nominal_base,
                        opt => opt.MapFrom(src => src.ValorNominalBase))
                    .ForMember(dest =>
                        dest.vl_nominal_atualizado,
                        opt => opt.MapFrom(src => src.ValorNominalAtualizado))
                    .ForMember(dest =>
                        dest.vl_fator_correcao,
                        opt => opt.MapFrom(src => src.FatorCorrecao))
                    .ForMember(dest =>
                        dest.vl_fator_juros,
                        opt => opt.MapFrom(src => src.FatorJuros))
                    .ForMember(dest =>
                        dest.vl_pu_abertura,
                        opt => opt.MapFrom(src => src.PuAbertura))
                    .ForMember(dest =>
                        dest.vl_pagamentos,
                        opt => opt.MapFrom(src => src.Pagamentos))
                    .ForMember(dest =>
                        dest.vl_pu_fechamento,
                        opt => opt.MapFrom(src => src.PuFechamento))
                    .ForMember(dest =>
                        dest.vl_principal,
                        opt => opt.MapFrom(src => src.Principal))
                    .ForMember(dest =>
                        dest.vl_inflacao,
                        opt => opt.MapFrom(src => src.Inflacao))
                    .ForMember(dest =>
                        dest.vl_juros,
                        opt => opt.MapFrom(src => src.Juros))
                    .ForMember(dest =>
                        dest.vl_incorporado,
                        opt => opt.MapFrom(src => src.Incorporado))
                    .ForMember(dest =>
                        dest.vl_incorporar,
                        opt => opt.MapFrom(src => src.Incorporar))
                    .ForMember(dest =>
                        dest.vl_amortizacao,
                        opt => opt.MapFrom(src => src.PagamentoAmortizacao))
                    .ForMember(dest =>
                        dest.vl_amex,
                        opt => opt.MapFrom(src => src.PagamentoAmex))
                    .ForMember(dest =>
                        dest.vl_vencimento,
                        opt => opt.MapFrom(src => src.PagamentoVencimento))
                    .ForMember(dest =>
                        dest.vl_premio,
                        opt => opt.MapFrom(src => src.PagamentoPremio))
                    .ForMember(dest =>
                        dest.vl_pagamento_juros,
                        opt => opt.MapFrom(src => src.PagamentoJuros))
                    .ForMember(dest =>
                        dest.vl_porcentual_amortizado,
                        opt => opt.MapFrom(src => src.PorcentualAmortizado))
                    .ForMember(dest =>
                        dest.vl_porcentual_juros_incorporado,
                        opt => opt.MapFrom(src => src.PorcentualJurosIncorporado))
                    .ForMember(dest =>
                        dest.status_pgto,
                        opt => opt.MapFrom(src => src.StatusPgto))
                    .ForMember(dest =>
                        dest.dt_att_status_pgto,
                        opt => opt.MapFrom(src => src.DataAttStatusPgto));

            CreateMap<DadosPrecoDAO, DadosPrecoLuz>()
                        .ForMember(dest =>
                        dest.CodigoSNA,
                        opt => opt.MapFrom(src => src.cd_sna))
                    .ForMember(dest =>
                        dest.Tipo,
                        opt => opt.MapFrom(src => src.tp_papel))
                    .ForMember(dest =>
                        dest.Indexador,
                        opt => opt.MapFrom(src => src.tp_indexador))
                    .ForMember(dest =>
                        dest.TaxaPos,
                        opt => opt.MapFrom(src => src.vl_taxa_pos))
                    .ForMember(dest =>
                        dest.TaxaPre,
                        opt => opt.MapFrom(src => src.vl_taxa_pre))
                    .ForMember(dest =>
                        dest.DataEvento,
                        opt => opt.MapFrom(src => src.dt_evento))
                    .ForMember(dest =>
                        dest.ValorNominalBase,
                        opt => opt.MapFrom(src => src.vl_nominal_base))
                    .ForMember(dest =>
                        dest.ValorNominalAtualizado,
                        opt => opt.MapFrom(src => src.vl_nominal_atualizado))
                    .ForMember(dest =>
                        dest.FatorCorrecao,
                        opt => opt.MapFrom(src => src.vl_fator_correcao))
                    .ForMember(dest =>
                        dest.FatorJuros,
                        opt => opt.MapFrom(src => src.vl_fator_juros))
                    .ForMember(dest =>
                        dest.PuAbertura,
                        opt => opt.MapFrom(src => src.vl_pu_abertura))
                    .ForMember(dest =>
                        dest.Pagamentos,
                        opt => opt.MapFrom(src => src.vl_pagamentos))
                    .ForMember(dest =>
                        dest.PuFechamento,
                        opt => opt.MapFrom(src => src.vl_pu_fechamento))
                    .ForMember(dest =>
                        dest.Principal,
                        opt => opt.MapFrom(src => src.vl_principal))
                    .ForMember(dest =>
                        dest.Inflacao,
                        opt => opt.MapFrom(src => src.vl_inflacao))
                    .ForMember(dest =>
                        dest.Juros,
                        opt => opt.MapFrom(src => src.vl_juros))
                    .ForMember(dest =>
                        dest.Incorporado,
                        opt => opt.MapFrom(src => src.vl_incorporado))
                    .ForMember(dest =>
                        dest.Incorporar,
                        opt => opt.MapFrom(src => src.vl_incorporar))
                    .ForMember(dest =>
                        dest.PagamentoAmortizacao,
                        opt => opt.MapFrom(src => src.vl_amortizacao))
                    .ForMember(dest =>
                        dest.PagamentoAmex,
                        opt => opt.MapFrom(src => src.vl_amex))
                    .ForMember(dest =>
                        dest.PagamentoVencimento,
                        opt => opt.MapFrom(src => src.vl_vencimento))
                    .ForMember(dest =>
                        dest.PagamentoPremio,
                        opt => opt.MapFrom(src => src.vl_premio))
                    .ForMember(dest =>
                        dest.PagamentoJuros,
                        opt => opt.MapFrom(src => src.vl_pagamento_juros))
                    .ForMember(dest =>
                        dest.PorcentualAmortizado,
                        opt => opt.MapFrom(src => src.vl_porcentual_amortizado))
                    .ForMember(dest =>
                        dest.PorcentualJurosIncorporado,
                        opt => opt.MapFrom(src => src.vl_porcentual_juros_incorporado))
                    .ForMember(dest =>
                        dest.StatusPgto,
                        opt => opt.MapFrom(src => src.status_pgto))
                    .ForMember(dest =>
                        dest.DataAttStatusPgto,
                        opt => opt.MapFrom(src => src.dt_att_status_pgto));



            CreateMap<DadosCaracteristicos, RawDataEventosProcessadosDAO>()
                .ForMember(dest =>
                    dest.cd_sna,
                    opt => opt.MapFrom(src => src.CodigoSNA))
                .ForMember(dest =>
                    dest.cd_sna_hash,
                    opt => opt.MapFrom(src => Utils.GenerateHash(src.CodigoSNA)))
                .ForMember(dest =>
                    dest.tp_dado,
                    opt => opt.MapFrom(src => 2))
                .ForMember(dest =>
                    dest.tx_json,
                    opt => opt.MapFrom(src => JObject.Parse(JsonConvert.SerializeObject(src))))
                .ForMember(dest =>
                    dest.dt_criacao,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest =>
                    dest.nr_rank,
                    opt => opt.MapFrom(src => Utils.GenerateRank(DateTime.Now)));

            CreateMap<DadosCaracteristicos, DadosCaracteristicosDAO>()
                .ForMember(dest =>
                    dest.cd_sna,
                    opt => opt.MapFrom(src => src.CodigoSNA))
                .ForMember(dest =>
                    dest.cd_sna_hash,
                    opt => opt.MapFrom(src => Utils.GenerateHash(src.CodigoSNA)))
                .ForMember(dest =>
                    dest.tp_evento,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest =>
                    dest.dt_criacao,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest =>
                    dest.dt_atualizacao,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest =>
                    dest.es_ativo,
                    opt => opt.MapFrom(src => true))
                .ForMember(dest =>
                    dest.tp_papel,
                    opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest =>
                    dest.cd_isin,
                    opt => opt.MapFrom(src => src.Isin))
                .ForMember(dest =>
                    dest.tx_emissor,
                    opt => opt.MapFrom(src => src.Emissor))
                .ForMember(dest =>
                    dest.tx_cnpj_emissor,
                    opt => opt.MapFrom(src => src.CnpjEmissor))
                .ForMember(dest =>
                    dest.dt_emissao,
                    opt => opt.MapFrom(src => src.DataEmissao))
                .ForMember(dest =>
                    dest.dt_inicio_rentabilidade,
                    opt => opt.MapFrom(src => src.DataInicioRentabilidade))
                .ForMember(dest =>
                    dest.dt_vencimento,
                    opt => opt.MapFrom(src => src.DataVencimento))
                .ForMember(dest =>
                    dest.vl_nominal_emissao,
                    opt => opt.MapFrom(src => src.ValorNominalEmissao))
                .ForMember(dest =>
                    dest.tx_instrucao_cvm,
                    opt => opt.MapFrom(src => src.InstrucaoCVM))
                .ForMember(dest =>
                    dest.tp_clearing,
                    opt => opt.MapFrom(src => src.Clearing))
                .ForMember(dest =>
                    dest.tx_agente_fiduciario,
                    opt => opt.MapFrom(src => src.AgenteFiduciario))
                .ForMember(dest =>
                    dest.es_possibilidade_resgate_antecipado,
                    opt => opt.MapFrom(src => src.PossibilidadeResgateAntecipado))
                .ForMember(dest =>
                    dest.es_conversivel_acao,
                    opt => opt.MapFrom(src => src.ConversivelAcao))
                .ForMember(dest =>
                    dest.es_debenture_incentivada,
                    opt => opt.MapFrom(src => src.DebentureIncentivada))
                .ForMember(dest =>
                    dest.tp_criterio_calculo_indexador,
                    opt => opt.MapFrom(src => src.CriterioCalculoIndexador))
                .ForMember(dest =>
                    dest.tp_criterio_calculo_juros,
                    opt => opt.MapFrom(src => src.CriterioCalculoJuros))
                .ForMember(dest =>
                    dest.tp_indexador,
                    opt => opt.MapFrom(src => src.Indexador))
                .ForMember(dest =>
                    dest.vl_taxa_pre,
                    opt => opt.MapFrom(src => src.TaxaPre))
                .ForMember(dest =>
                    dest.vl_taxa_pos,
                    opt => opt.MapFrom(src => src.TaxaPos))
                .ForMember(dest =>
                    dest.tx_projecao,
                    opt => opt.MapFrom(src => src.Projecao))
                .ForMember(dest =>
                    dest.tp_amortizacao,
                    opt => opt.MapFrom(src => src.Amortizacao))
                .ForMember(dest =>
                    dest.tp_periodicidade_correcao,
                    opt => opt.MapFrom(src => src.PeriodicidadeCorrecao))
                .ForMember(dest =>
                    dest.tp_unidade_indexador,
                    opt => opt.MapFrom(src => src.UnidadeIndexador))
                .ForMember(dest =>
                    dest.vl_defasagem_indexador,
                    opt => opt.MapFrom(src => src.DefasagemIndexador))
                .ForMember(dest =>
                    dest.di_referencia_indexador,
                    opt => opt.MapFrom(src => src.DiaReferenciaIndexador))
                .ForMember(dest =>
                    dest.me_referencia_indexador,
                    opt => opt.MapFrom(src => src.MesReferenciaIndexador))
                .ForMember(dest =>
                    dest.tx_devedor,
                    opt => opt.MapFrom(src => src.Devedor))
                .ForMember(dest =>
                    dest.tp_regime,
                    opt => opt.MapFrom(src => src.TipoRegime))
                .ForMember(dest =>
                    dest.tp_aniversario,
                    opt => opt.MapFrom(src => src.TipoAniversario))
                .ForMember(dest =>
                    dest.es_considera_deflacao,
                    opt => opt.MapFrom(src => src.ConsideraCorrecaoNegativa))
                .ForMember(dest =>
                    dest.dt_ultima_alteracao,
                    opt => opt.MapFrom(src => src.DataUltimaAlteracao))
                .ForMember(dest =>
                    dest.tx_cnpj_devedor,
                    opt => opt.MapFrom(src => src.CnpjDevedor))
                .ForMember(dest =>
                    dest.tx_cnpj_agente_fiduciario,
                    opt => opt.MapFrom(src => src.CnpjAgenteFiduciario));

            CreateMap<DadosCaracteristicosDAO, DadosCaracteristicos>()
                .ForMember(dest =>
                    dest.CodigoSNA,
                    opt => opt.MapFrom(src => src.cd_sna))
                .ForMember(dest =>
                    dest.Tipo,
                    opt => opt.MapFrom(src => src.tp_papel))
                .ForMember(dest =>
                    dest.Isin,
                    opt => opt.MapFrom(src => src.cd_isin))
                .ForMember(dest =>
                    dest.Emissor,
                    opt => opt.MapFrom(src => src.tx_emissor))
                .ForMember(dest =>
                    dest.CnpjEmissor,
                    opt => opt.MapFrom(src => src.tx_cnpj_emissor))
                .ForMember(dest =>
                    dest.DataEmissao,
                    opt => opt.MapFrom(src => src.dt_emissao))
                .ForMember(dest =>
                    dest.DataInicioRentabilidade,
                    opt => opt.MapFrom(src => src.dt_inicio_rentabilidade))
                .ForMember(dest =>
                    dest.DataVencimento,
                    opt => opt.MapFrom(src => src.dt_vencimento))
                .ForMember(dest =>
                    dest.ValorNominalEmissao,
                    opt => opt.MapFrom(src => src.vl_nominal_emissao))
                .ForMember(dest =>
                    dest.InstrucaoCVM,
                    opt => opt.MapFrom(src => src.tx_instrucao_cvm))
                .ForMember(dest =>
                    dest.Clearing,
                    opt => opt.MapFrom(src => src.tp_clearing))
                .ForMember(dest =>
                    dest.AgenteFiduciario,
                    opt => opt.MapFrom(src => src.tx_agente_fiduciario))
                .ForMember(dest =>
                    dest.PossibilidadeResgateAntecipado,
                    opt => opt.MapFrom(src => src.es_possibilidade_resgate_antecipado))
                .ForMember(dest =>
                    dest.ConversivelAcao,
                    opt => opt.MapFrom(src => src.es_conversivel_acao))
                .ForMember(dest =>
                    dest.DebentureIncentivada,
                    opt => opt.MapFrom(src => src.es_debenture_incentivada))
                .ForMember(dest =>
                    dest.CriterioCalculoIndexador,
                    opt => opt.MapFrom(src => src.tp_criterio_calculo_indexador))
                .ForMember(dest =>
                    dest.CriterioCalculoJuros,
                    opt => opt.MapFrom(src => src.tp_criterio_calculo_juros))
                .ForMember(dest =>
                    dest.Indexador,
                    opt => opt.MapFrom(src => src.tp_indexador))
                .ForMember(dest =>
                    dest.TaxaPre,
                    opt => opt.MapFrom(src => src.vl_taxa_pre))
                .ForMember(dest =>
                    dest.TaxaPos,
                    opt => opt.MapFrom(src => src.vl_taxa_pos))
                .ForMember(dest =>
                    dest.Projecao,
                    opt => opt.MapFrom(src => src.tx_projecao))
                .ForMember(dest =>
                    dest.Amortizacao,
                    opt => opt.MapFrom(src => src.tp_amortizacao))
                .ForMember(dest =>
                    dest.PeriodicidadeCorrecao,
                    opt => opt.MapFrom(src => src.tp_periodicidade_correcao))
                .ForMember(dest =>
                    dest.UnidadeIndexador,
                    opt => opt.MapFrom(src => src.tp_unidade_indexador))
                .ForMember(dest =>
                    dest.DefasagemIndexador,
                    opt => opt.MapFrom(src => src.vl_defasagem_indexador))
                .ForMember(dest =>
                    dest.DiaReferenciaIndexador,
                    opt => opt.MapFrom(src => src.di_referencia_indexador))
                .ForMember(dest =>
                    dest.MesReferenciaIndexador,
                    opt => opt.MapFrom(src => src.me_referencia_indexador))
                .ForMember(dest =>
                    dest.Devedor,
                    opt => opt.MapFrom(src => src.tx_devedor))
                .ForMember(dest =>
                    dest.TipoRegime,
                    opt => opt.MapFrom(src => src.tp_regime))
                .ForMember(dest =>
                    dest.TipoAniversario,
                    opt => opt.MapFrom(src => src.tp_aniversario))
                .ForMember(dest =>
                    dest.ConsideraCorrecaoNegativa,
                    opt => opt.MapFrom(src => src.es_considera_deflacao))
                .ForMember(dest =>
                    dest.DataUltimaAlteracao,
                    opt => opt.MapFrom(src => src.dt_ultima_alteracao))
                .ForMember(dest =>
                    dest.CnpjDevedor,
                    opt => opt.MapFrom(src => src.tx_cnpj_devedor))
                .ForMember(dest =>
                    dest.CnpjAgenteFiduciario,
                    opt => opt.MapFrom(src => src.tx_cnpj_agente_fiduciario));


            CreateMap<Fluxos, RawDataEventosProcessadosDAO>()
                .ForMember(dest =>
                    dest.cd_sna,
                    opt => opt.MapFrom(src => src.CodigoSNA))
                .ForMember(dest =>
                    dest.cd_sna_hash,
                    opt => opt.MapFrom(src => Utils.GenerateHash(src.CodigoSNA)))
                .ForMember(dest =>
                    dest.tp_dado,
                    opt => opt.MapFrom(src => 3))
                .ForMember(dest =>
                    dest.tx_json,
                    opt => opt.MapFrom(src => JObject.FromObject(src)))
                .ForMember(dest =>
                    dest.dt_criacao,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest =>
                    dest.nr_rank,
                    opt => opt.MapFrom(src => Utils.GenerateRank(DateTime.Now)));

            CreateMap<Fluxos, FluxosDAO>()
                .ForMember(dest =>
                    dest.cd_sna,
                    opt => opt.MapFrom(src => src.CodigoSNA))
                .ForMember(dest =>
                    dest.cd_sna_hash,
                    opt => opt.MapFrom(src => Utils.GenerateHash(src.CodigoSNA)))
                .ForMember(dest =>
                    dest.tp_papel,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest =>
                    dest.dt_base,
                    opt => opt.MapFrom(src => src.DataBase))
                .ForMember(dest =>
                    dest.dt_criacao,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest =>
                    dest.dt_atualizacao,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest =>
                    dest.es_ativo,
                    opt => opt.MapFrom(src => true))
                .ForMember(dest =>
                    dest.dt_liquidacao,
                    opt => opt.MapFrom(src => src.DataLiquidacao))
                .ForMember(dest =>
                    dest.tp_evento,
                    opt => opt.MapFrom(src => src.TipoEvento))
                .ForMember(dest =>
                    dest.vl_taxa,
                    opt => opt.MapFrom(src => src.Taxa));

            CreateMap<FluxosDAO, Fluxos>()
                .ForMember(dest =>
                    dest.CodigoSNA,
                    opt => opt.MapFrom(src => src.cd_sna))
                .ForMember(dest =>
                    dest.DataBase,
                    opt => opt.MapFrom(src => src.dt_base))
                .ForMember(dest =>
                    dest.DataLiquidacao,
                    opt => opt.MapFrom(src => src.dt_liquidacao))
                .ForMember(dest =>
                    dest.TipoEvento,
                    opt => opt.MapFrom(src => src.tp_evento))
                .ForMember(dest =>
                    dest.Taxa,
                    opt => opt.MapFrom(src => src.vl_taxa));

            CreateMap<AssinaturaLogDAO, AssinaturaLogResponse>()

                .ForMember(dest =>
                    dest.DataAtualizacao,
                    opt => opt.MapFrom(src => src.dt_criacao))
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.cd_cge))
                .ForMember(dest =>
                    dest.Usuario,
                    opt => opt.MapFrom(src => src.usuario))
                .ForMember(dest =>
                    dest.Estado,
                    opt => opt.MapFrom((src, dest) =>
                    {
                        var obj = src.tx_estado is null ? new AssinaturaNullable() : src.tx_estado.ToObject<AssinaturaObject>();
                        return new EstadoLog()
                        {
                            Assinado = src.es_assinado ? "Sim" : "Não",
                            ImpactaCadastro = obj.ImpactaCadastro ? "Sim" : "Não",
                            ImpactaPreco = obj.ImpactaPreco ? "Sim" : "Não",
                            ImpactaEvento = obj.ImpactaEvento ? "Sim" : "Não",
                            ImpactaHistorico = obj.ImpactaHistorico ? "Sim" : "Não"
                        };
                    }));

            //CreateMap<AssinaturaDAO, Assinatura>()
            //.ForMember(dest =>
            //    dest.Assinado,
            //     opt => opt.MapFrom(src => src.es_assinado ? "Sim" : "Não"))
            //    .ForMember(dest =>
            //        dest.DataAssinatura,
            //        opt => opt.MapFrom(src => src.dt_assinatura.ToString("dd/MM/yyyy HH:mm")))
            //    .ForMember(dest =>
            //        dest.Papel,
            //        opt => opt.MapFrom(src => src.cd_sna))
            //    .ForMember(dest =>
            //        dest.ImpactaMdp,
            //        opt => opt.MapFrom(src => src.impacta_mdp ? "Sim" : "Não"));


            CreateMap<AssinaturaObject, AssinaturaLuzRequest>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.Papel.ToUpper()));

            CreateMap<AssinaturaFlag, PapelAssinado>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.cd_sna))
                .ForMember(dest =>
                    dest.DataAssinatura,
                    opt => opt.MapFrom(src => src.dt_assinatura))
                .ForMember(dest =>
                    dest.ImpactaPreco,
                    opt => opt.MapFrom(src => src.impacta_preco ? "Sim" : "Não"))
                .ForMember(dest =>
                    dest.ImpactaCadastro,
                    opt => opt.MapFrom(src => src.impacta_cadastro ? "Sim" : "Não"))
                .ForMember(dest =>
                    dest.ImpactaHistorico,
                    opt => opt.MapFrom(src => src.impacta_pu_historico ? "Sim" : "Não"))
                .ForMember(dest =>
                    dest.ImpactaEvento,
                    opt => opt.MapFrom(src => src.impacta_pu_evento ? "Sim" : "Não"));

            CreateMap<AssinaturaFlag, AssinaturaObject>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.cd_sna))
                .ForMember(dest =>
                    dest.ImpactaPreco,
                    opt => opt.MapFrom(src => src.impacta_preco))
                .ForMember(dest =>
                    dest.ImpactaCadastro,
                    opt => opt.MapFrom(src => src.impacta_cadastro))
                .ForMember(dest =>
                    dest.ImpactaHistorico,
                    opt => opt.MapFrom(src => src.impacta_pu_historico))
                .ForMember(dest =>
                    dest.ImpactaEvento,
                    opt => opt.MapFrom(src => src.impacta_pu_evento));

            CreateMap<AssinaturaFlag, Assinatura>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.cd_sna))
                .ForMember(dest =>
                    dest.DataAssinatura,
                    opt => opt.MapFrom(src => src.dt_assinatura))
                .ForMember(dest =>
                   dest.Assinado,
                    opt => opt.MapFrom(src => src.es_assinado))
                .ForMember(dest =>
                    dest.ImpactaPreco,
                    opt => opt.MapFrom(src => src.impacta_preco))
                .ForMember(dest =>
                    dest.ImpactaCadastro,
                    opt => opt.MapFrom(src => src.impacta_cadastro))
                .ForMember(dest =>
                    dest.ImpactaHistorico,
                    opt => opt.MapFrom(src => src.impacta_pu_historico))
                .ForMember(dest =>
                    dest.ImpactaEvento,
                    opt => opt.MapFrom(src => src.impacta_pu_evento));

            CreateMap<AssinaturaFlag, AssinaturaMdp>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.cd_sna))
                .ForMember(dest =>
                    dest.ImpactaPreco,
                    opt => opt.MapFrom(src => src.impacta_preco))
                .ForMember(dest =>
                    dest.ImpactaCadastro,
                    opt => opt.MapFrom(src => src.impacta_cadastro))
                .ForMember(dest =>
                    dest.ImpactaEvento,
                    opt => opt.MapFrom(src => src.impacta_pu_evento))
                .ForMember(dest =>
                    dest.ImpactaHistorico,
                    opt => opt.MapFrom(src => src.impacta_pu_historico))
                .ForMember(dest =>
                    dest.Assinado,
                    opt => opt.MapFrom(src => src.es_assinado));

            CreateMap<AssinaturaMdp, AssinaturaObject>()
               .ForMember(dest =>
                   dest.Papel,
                   opt => opt.MapFrom(src => src.Papel))
               .ForMember(dest =>
                   dest.ImpactaPreco,
                   opt => opt.MapFrom(src => src.ImpactaPreco))
               .ForMember(dest =>
                   dest.ImpactaCadastro,
                   opt => opt.MapFrom(src => src.ImpactaCadastro))
               .ForMember(dest =>
                   dest.ImpactaEvento,
                   opt => opt.MapFrom(src => src.ImpactaEvento))
               .ForMember(dest =>
                   dest.ImpactaHistorico,
                   opt => opt.MapFrom(src => src.ImpactaHistorico));


            CreateMap<PuDeEventos, RawDataEventosProcessadosDAO>()
                .ForMember(dest =>
                    dest.cd_sna,
                    opt => opt.MapFrom(src => src.CodigoSNA))
                .ForMember(dest =>
                    dest.cd_sna_hash,
                    opt => opt.MapFrom(src => Utils.GenerateHash(src.CodigoSNA)))
                .ForMember(dest =>
                    dest.tp_dado,
                    opt => opt.MapFrom(src => 4))
                .ForMember(dest =>
                    dest.tx_json,
                    opt => opt.MapFrom(src => JObject.Parse(JsonConvert.SerializeObject(src))))
                .ForMember(dest =>
                    dest.dt_criacao,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest =>
                    dest.nr_rank,
                    opt => opt.MapFrom(src => Utils.GenerateRank(DateTime.Now)));

            CreateMap<DadosPrecoLuz, RastreamentoPapel>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.CodigoSNA));

            CreateMap<DadosCaracteristicos, RastreamentoPapel>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.CodigoSNA));

            CreateMap<Fluxos, RastreamentoPapel>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.CodigoSNA));

            CreateMap<PuDeEventos, RastreamentoPapel>()
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.CodigoSNA));

            CreateMap<PuDeEventos, PuDeEventosDAO>()
                    .ForMember(dest =>
                        dest.cd_sna,
                        opt => opt.MapFrom(src => src.CodigoSNA))
                    .ForMember(dest =>
                        dest.cd_sna_hash,
                        opt => opt.MapFrom(src => Utils.GenerateHash(src.CodigoSNA)))
                    .ForMember(dest =>
                        dest.tp_evento,
                        opt => opt.MapFrom(src => 1))
                    .ForMember(dest =>
                        dest.dt_criacao,
                        opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(dest =>
                        dest.dt_atualizacao,
                        opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(dest =>
                        dest.es_ativo,
                        opt => opt.MapFrom(src => true))
                    .ForMember(dest =>
                        dest.tp_papel,
                        opt => opt.MapFrom(src => src.Tipo))
                    .ForMember(dest =>
                        dest.tp_indexador,
                        opt => opt.MapFrom(src => src.Indexador))
                    .ForMember(dest =>
                        dest.vl_taxa_pos,
                        opt => opt.MapFrom(src => src.TaxaPos))
                    .ForMember(dest =>
                        dest.vl_taxa_pre,
                        opt => opt.MapFrom(src => src.TaxaPre))
                    .ForMember(dest =>
                        dest.dt_evento,
                        opt => opt.MapFrom(src => src.DataEvento))
                    .ForMember(dest =>
                        dest.vl_nominal_base,
                        opt => opt.MapFrom(src => src.ValorNominalBase))
                    .ForMember(dest =>
                        dest.vl_nominal_atualizado,
                        opt => opt.MapFrom(src => src.ValorNominalAtualizado))
                    .ForMember(dest =>
                        dest.vl_fator_correcao,
                        opt => opt.MapFrom(src => src.FatorCorrecao))
                    .ForMember(dest =>
                        dest.vl_fator_juros,
                        opt => opt.MapFrom(src => src.FatorJuros))
                    .ForMember(dest =>
                        dest.vl_pu_abertura,
                        opt => opt.MapFrom(src => src.PuAbertura))
                    .ForMember(dest =>
                        dest.vl_pagamentos,
                        opt => opt.MapFrom(src => src.Pagamentos))
                    .ForMember(dest =>
                        dest.vl_pu_fechamento,
                        opt => opt.MapFrom(src => src.PuFechamento))
                    .ForMember(dest =>
                        dest.vl_principal,
                        opt => opt.MapFrom(src => src.Principal))
                    .ForMember(dest =>
                        dest.vl_inflacao,
                        opt => opt.MapFrom(src => src.Inflacao))
                    .ForMember(dest =>
                        dest.vl_juros,
                        opt => opt.MapFrom(src => src.Juros))
                    .ForMember(dest =>
                        dest.vl_incorporado,
                        opt => opt.MapFrom(src => src.Incorporado))
                    .ForMember(dest =>
                        dest.vl_incorporar,
                        opt => opt.MapFrom(src => src.Incorporar))
                    .ForMember(dest =>
                        dest.vl_amortizacao,
                        opt => opt.MapFrom(src => src.PagamentoAmortizacao))
                    .ForMember(dest =>
                        dest.vl_amex,
                        opt => opt.MapFrom(src => src.PagamentoAmex))
                    .ForMember(dest =>
                        dest.vl_vencimento,
                        opt => opt.MapFrom(src => src.PagamentoVencimento))
                    .ForMember(dest =>
                        dest.vl_premio,
                        opt => opt.MapFrom(src => src.PagamentoPremio))
                    .ForMember(dest =>
                        dest.vl_pagamento_juros,
                        opt => opt.MapFrom(src => src.PagamentoJuros))
                    .ForMember(dest =>
                        dest.vl_porcentual_amortizado,
                        opt => opt.MapFrom(src => src.PorcentualAmortizado))
                    .ForMember(dest =>
                        dest.vl_porcentual_juros_incorporado,
                        opt => opt.MapFrom(src => src.PorcentualJurosIncorporado))
                    .ForMember(dest =>
                        dest.status_pgto,
                        opt => opt.MapFrom(src => src.StatusPgto))
                    .ForMember(dest =>
                        dest.dt_att_status_pgto,
                        opt => opt.MapFrom(src => src.DataAttStatusPgto));

            CreateMap<PuDeEventosDAO, PuDeEventos>()
                        .ForMember(dest =>
                        dest.CodigoSNA,
                        opt => opt.MapFrom(src => src.cd_sna))
                    .ForMember(dest =>
                        dest.Tipo,
                        opt => opt.MapFrom(src => src.tp_papel))
                    .ForMember(dest =>
                        dest.Indexador,
                        opt => opt.MapFrom(src => src.tp_indexador))
                    .ForMember(dest =>
                        dest.TaxaPos,
                        opt => opt.MapFrom(src => src.vl_taxa_pos))
                    .ForMember(dest =>
                        dest.TaxaPre,
                        opt => opt.MapFrom(src => src.vl_taxa_pre))
                    .ForMember(dest =>
                        dest.DataEvento,
                        opt => opt.MapFrom(src => src.dt_evento))
                    .ForMember(dest =>
                        dest.ValorNominalBase,
                        opt => opt.MapFrom(src => src.vl_nominal_base))
                    .ForMember(dest =>
                        dest.ValorNominalAtualizado,
                        opt => opt.MapFrom(src => src.vl_nominal_atualizado))
                    .ForMember(dest =>
                        dest.FatorCorrecao,
                        opt => opt.MapFrom(src => src.vl_fator_correcao))
                    .ForMember(dest =>
                        dest.FatorJuros,
                        opt => opt.MapFrom(src => src.vl_fator_juros))
                    .ForMember(dest =>
                        dest.PuAbertura,
                        opt => opt.MapFrom(src => src.vl_pu_abertura))
                    .ForMember(dest =>
                        dest.Pagamentos,
                        opt => opt.MapFrom(src => src.vl_pagamentos))
                    .ForMember(dest =>
                        dest.PuFechamento,
                        opt => opt.MapFrom(src => src.vl_pu_fechamento))
                    .ForMember(dest =>
                        dest.Principal,
                        opt => opt.MapFrom(src => src.vl_principal))
                    .ForMember(dest =>
                        dest.Inflacao,
                        opt => opt.MapFrom(src => src.vl_inflacao))
                    .ForMember(dest =>
                        dest.Juros,
                        opt => opt.MapFrom(src => src.vl_juros))
                    .ForMember(dest =>
                        dest.Incorporado,
                        opt => opt.MapFrom(src => src.vl_incorporado))
                    .ForMember(dest =>
                        dest.Incorporar,
                        opt => opt.MapFrom(src => src.vl_incorporar))
                    .ForMember(dest =>
                        dest.PagamentoAmortizacao,
                        opt => opt.MapFrom(src => src.vl_amortizacao))
                    .ForMember(dest =>
                        dest.PagamentoAmex,
                        opt => opt.MapFrom(src => src.vl_amex))
                    .ForMember(dest =>
                        dest.PagamentoVencimento,
                        opt => opt.MapFrom(src => src.vl_vencimento))
                    .ForMember(dest =>
                        dest.PagamentoPremio,
                        opt => opt.MapFrom(src => src.vl_premio))
                    .ForMember(dest =>
                        dest.PagamentoJuros,
                        opt => opt.MapFrom(src => src.vl_pagamento_juros))
                    .ForMember(dest =>
                        dest.PorcentualAmortizado,
                        opt => opt.MapFrom(src => src.vl_porcentual_amortizado))
                    .ForMember(dest =>
                        dest.PorcentualJurosIncorporado,
                        opt => opt.MapFrom(src => src.vl_porcentual_juros_incorporado))
                    .ForMember(dest =>
                        dest.StatusPgto,
                        opt => opt.MapFrom(src => src.status_pgto))
                    .ForMember(dest =>
                        dest.DataAttStatusPgto,
                        opt => opt.MapFrom(src => src.dt_att_status_pgto));

            CreateMap<DadosPrecoLuz, PuDeEventos>()
                        .ForMember(dest =>
                        dest.CodigoSNA,
                        opt => opt.MapFrom(src => src.CodigoSNA))
                    .ForMember(dest =>
                        dest.Tipo,
                        opt => opt.MapFrom(src => src.Tipo))
                    .ForMember(dest =>
                        dest.Indexador,
                        opt => opt.MapFrom(src => src.Indexador))
                    .ForMember(dest =>
                        dest.TaxaPos,
                        opt => opt.MapFrom(src => src.TaxaPos))
                    .ForMember(dest =>
                        dest.TaxaPre,
                        opt => opt.MapFrom(src => src.TaxaPre))
                    .ForMember(dest =>
                        dest.DataEvento,
                        opt => opt.MapFrom(src => src.DataEvento))
                    .ForMember(dest =>
                        dest.ValorNominalBase,
                        opt => opt.MapFrom(src => src.ValorNominalBase))
                    .ForMember(dest =>
                        dest.ValorNominalAtualizado,
                        opt => opt.MapFrom(src => src.ValorNominalAtualizado))
                    .ForMember(dest =>
                        dest.FatorCorrecao,
                        opt => opt.MapFrom(src => src.FatorCorrecao))
                    .ForMember(dest =>
                        dest.FatorJuros,
                        opt => opt.MapFrom(src => src.FatorJuros))
                    .ForMember(dest =>
                        dest.PuAbertura,
                        opt => opt.MapFrom(src => src.PuAbertura))
                    .ForMember(dest =>
                        dest.Pagamentos,
                        opt => opt.MapFrom(src => src.Pagamentos))
                    .ForMember(dest =>
                        dest.PuFechamento,
                        opt => opt.MapFrom(src => src.PuFechamento))
                    .ForMember(dest =>
                        dest.Principal,
                        opt => opt.MapFrom(src => src.Principal))
                    .ForMember(dest =>
                        dest.Inflacao,
                        opt => opt.MapFrom(src => src.Inflacao))
                    .ForMember(dest =>
                        dest.Juros,
                        opt => opt.MapFrom(src => src.Juros))
                    .ForMember(dest =>
                        dest.Incorporado,
                        opt => opt.MapFrom(src => src.Incorporado))
                    .ForMember(dest =>
                        dest.Incorporar,
                        opt => opt.MapFrom(src => src.Incorporar))
                    .ForMember(dest =>
                        dest.PagamentoAmortizacao,
                        opt => opt.MapFrom(src => src.PagamentoAmortizacao))
                    .ForMember(dest =>
                        dest.PagamentoAmex,
                        opt => opt.MapFrom(src => src.PagamentoAmex))
                    .ForMember(dest =>
                        dest.PagamentoVencimento,
                        opt => opt.MapFrom(src => src.PagamentoVencimento))
                    .ForMember(dest =>
                        dest.PagamentoPremio,
                        opt => opt.MapFrom(src => src.PagamentoPremio))
                    .ForMember(dest =>
                        dest.PagamentoJuros,
                        opt => opt.MapFrom(src => src.PagamentoJuros))
                    .ForMember(dest =>
                        dest.PorcentualAmortizado,
                        opt => opt.MapFrom(src => src.PorcentualAmortizado))
                    .ForMember(dest =>
                        dest.PorcentualJurosIncorporado,
                        opt => opt.MapFrom(src => src.PorcentualJurosIncorporado))
                    .ForMember(dest =>
                        dest.StatusPgto,
                        opt => opt.MapFrom(src => src.StatusPgto))
                    .ForMember(dest =>
                        dest.DataAttStatusPgto,
                        opt => opt.MapFrom(src => src.DataAttStatusPgto));

            CreateMap<RastreamentoEventoDAO, RastreamentoEventoPortal>()
                .ForMember(dest =>
                    dest.IdRequisicao,
                    opt => opt.MapFrom(src => new Guid(src.id_requisicao)))
                .ForMember(dest =>
                    dest.TipoRequisicao,
                    opt => opt.MapFrom(src => src.en_tipo_requisicao))
                .ForMember(dest =>
                    dest.DataInicioEvento,
                    opt => opt.MapFrom(src => src.dh_inicio_evento))
                .ForMember(dest =>
                    dest.DataFimEvento,
                    opt => opt.MapFrom(src => src.dh_final_evento))
                .ForMember(dest =>
                    dest.Metodo,
                    opt => opt.MapFrom(src => src.metodo))
                .ForMember(dest =>
                    dest.StatusEvento,
                    opt => opt.MapFrom(src => src.en_status))
                .ForMember(dest =>
                    dest.InfoEventoErro,
                    opt => opt.MapFrom(src => src.tx_evento.ToObject<JsonEvento>().Erro))
                .ForMember(dest =>
                    dest.Usuario,
                    opt => opt.MapFrom(src => src.nm_login_usuario));

            CreateMap<RastreamentoEventoPortal, RastreamentoEventoCSV>()
                .ForMember(dest =>
                    dest.TipoRequisicao,
                    opt => opt.MapFrom(src => src.TipoRequisicao))
                .ForMember(dest =>
                    dest.DataInicioEvento,
                    opt => opt.MapFrom(src => src.DataInicioEvento))
                .ForMember(dest =>
                    dest.DataFimEvento,
                    opt => opt.MapFrom(src => src.DataFimEvento))
                .ForMember(dest =>
                    dest.StatusEvento,
                    opt => opt.MapFrom(src => src.StatusEvento))
                .ForMember(dest =>
                    dest.Usuario,
                    opt => opt.MapFrom(src => src.Usuario));

            CreateMap<RastreamentoPapelDAO, RastreamentoPapel>()
                .ForMember(dest =>
                    dest.IdRequisicao,
                    opt => opt.MapFrom(src => new Guid(src.fk_id_requisicao)))
                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.cd_sna))
                .ForMember(dest =>
                    dest.DataInicioEvento,
                    opt => opt.MapFrom(src => src.dh_inicio_evento))
                .ForMember(dest =>
                    dest.DataFimEvento,
                    opt => opt.MapFrom(src => src.dh_final_evento))
                .ForMember(dest =>
                    dest.TipoLog,
                    opt => opt.MapFrom(src => src.en_tipo))
                .ForMember(dest =>
                    dest.StatusMensagem,
                    opt => opt.MapFrom(src => src.en_status))
                .ForMember(dest =>
                    dest.StatusPapel,
                    opt => opt.MapFrom(src => src.en_status_processamento))
                .ForMember(dest =>
                    dest.MensagemErro,
                    opt => opt.MapFrom(src => src.tx_erro))
                .ForMember(dest =>
                    dest.Usuario,
                    opt => opt.MapFrom(src => src.nm_login_usuario));

            CreateMap<RastreamentoPapel, RastreamentoPapelCSV>()

                .ForMember(dest =>
                    dest.Papel,
                    opt => opt.MapFrom(src => src.Papel))
                .ForMember(dest =>
                    dest.DataInicioEvento,
                    opt => opt.MapFrom(src => src.DataInicioEvento))
                .ForMember(dest =>
                    dest.DataFimEvento,
                    opt => opt.MapFrom(src => src.DataFimEvento))
                .ForMember(dest =>
                    dest.TipoLog,
                    opt => opt.MapFrom(src => src.TipoLog))
                .ForMember(dest =>
                    dest.StatusPapel,
                    opt => opt.MapFrom(src => src.StatusMensagem))
                .ForMember(dest =>
                    dest.StatusEvento,
                    opt => opt.MapFrom(src => src.StatusPapel))
                .ForMember(dest =>
                    dest.MensagemErro,
                    opt => opt.MapFrom(src => src.MensagemErro));

        }
    }
}
