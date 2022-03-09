using System.Xml.Serialization;
namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementosAgricolas
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "PercPgto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string PercPgto { get; set; }
        [XmlElement(ElementName = "ContadoCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContadoCredorOriginal { get; set; }
        [XmlElement(ElementName = "CodigodoContrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodigodoContrato { get; set; }
        [XmlElement(ElementName = "Coobrigacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Coobrigacao { get; set; }
        [XmlElement(ElementName = "NomeEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NomeEmitente { get; set; }
        [XmlElement(ElementName = "EmissorRazaoSocialEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string EmissorRazaoSocialEmitente { get; set; }
        [XmlElement(ElementName = "NaturezaEmissorEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NaturezaEmissorEmitente { get; set; }
        [XmlElement(ElementName = "CPFCNPJEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CPFCNPJEmitente { get; set; }
        [XmlElement(ElementName = "EstadoEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string EstadoEmitente { get; set; }
        [XmlElement(ElementName = "LocalMunicipioCidadedeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string LocalMunicipioCidadedeEmissao { get; set; }
        [XmlElement(ElementName = "LiquidacaoAntecipada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string LiquidacaoAntecipada { get; set; }
        [XmlElement(ElementName = "NomeSimplificadodoCredorCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NomeSimplificadodoCredorCredorOriginal { get; set; }
        [XmlElement(ElementName = "Garantidor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Garantidor { get; set; }
        [XmlElement(ElementName = "NaturezaGarantidor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NaturezaGarantidor { get; set; }
        [XmlElement(ElementName = "CPFCNPJGarantidor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CPFCNPJGarantidor { get; set; }
        [XmlElement(ElementName = "ContadoGarantidor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContadoGarantidor { get; set; }
        [XmlElement(ElementName = "ContadaSeguradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContadaSeguradora { get; set; }
        [XmlElement(ElementName = "TipodeGarantia1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string TipodeGarantia1 { get; set; }
        [XmlElement(ElementName = "TipodeGarantia2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string TipodeGarantia2 { get; set; }
        [XmlElement(ElementName = "TipodeGarantia3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string TipodeGarantia3 { get; set; }
        [XmlElement(ElementName = "Proprietario1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Proprietario1 { get; set; }
        [XmlElement(ElementName = "Proprietario2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Proprietario2 { get; set; }
        [XmlElement(ElementName = "Proprietario3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Proprietario3 { get; set; }
        [XmlElement(ElementName = "Cartular", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Cartular { get; set; }
        [XmlElement(ElementName = "JurosVencidosNaoPagos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string JurosVencidosNaoPagos { get; set; }
        [XmlElement(ElementName = "QtdedeCuponsVencidosaPagar", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string QtdedeCuponsVencidosaPagar { get; set; }
        [XmlElement(ElementName = "TipodeTDAINCRA", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string TipodeTDAINCRA { get; set; }
        [XmlElement(ElementName = "TipodeCPR", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string TipodeCPR { get; set; }
        [XmlElement(ElementName = "Produto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Produto { get; set; }
        [XmlElement(ElementName = "ClasseTipoPH", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ClasseTipoPH { get; set; }
        [XmlElement(ElementName = "Safra", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Safra { get; set; }
        [XmlElement(ElementName = "Caracteristica", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Caracteristica { get; set; }
        [XmlElement(ElementName = "Quantidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Quantidade { get; set; }
        [XmlElement(ElementName = "UnidadedeMedida", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string UnidadedeMedida { get; set; }
        [XmlElement(ElementName = "FormadeAcondicionamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string FormadeAcondicionamento { get; set; }
        [XmlElement(ElementName = "SituacaoCPR", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string SituacaoCPR { get; set; }
        [XmlElement(ElementName = "ProducaoCPR", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ProducaoCPR { get; set; }
        [XmlElement(ElementName = "ImovelCPR", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ImovelCPR { get; set; }
        [XmlElement(ElementName = "PrazodeDocumentoEntregaCPR", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string PrazodeDocumentoEntregaCPR { get; set; }
        [XmlElement(ElementName = "LocalEntregaCPR", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string LocalEntregaCPR { get; set; }
        [XmlElement(ElementName = "UFEntregaCPR", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string UFEntregaCPR { get; set; }
        [XmlElement(ElementName = "MunicipioEntregaCPR", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string MunicipioEntregaCPR { get; set; }
        [XmlElement(ElementName = "Emissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Emissao { get; set; }
        [XmlElement(ElementName = "Serie", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Serie { get; set; }
        [XmlElement(ElementName = "TipodaSerie", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string TipodaSerie { get; set; }
        [XmlElement(ElementName = "RegimeFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string RegimeFiduciario { get; set; }
        [XmlElement(ElementName = "ContadoAgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContadoAgenteFiduciario { get; set; }
        [XmlElement(ElementName = "RazaoSocialAgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string RazaoSocialAgenteFiduciario { get; set; }
        [XmlElement(ElementName = "NaturezaAgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NaturezaAgenteFiduciario { get; set; }
        [XmlElement(ElementName = "CPFCNPJAgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CPFCNPJAgenteFiduciario { get; set; }
        [XmlElement(ElementName = "CodigodoBancoLiquidante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodigodoBancoLiquidante { get; set; }
        [XmlElement(ElementName = "AgencianoBancoLiquidante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string AgencianoBancoLiquidante { get; set; }
        [XmlElement(ElementName = "ContaCorrentenoBancoLiquidante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContaCorrentenoBancoLiquidante { get; set; }
        [XmlElement(ElementName = "ClassificadoradeRisco1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ClassificadoradeRisco1 { get; set; }
        [XmlElement(ElementName = "ClassificadoradeRisco2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ClassificadoradeRisco2 { get; set; }
        [XmlElement(ElementName = "Rating1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Rating1 { get; set; }
        [XmlElement(ElementName = "Rating2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Rating2 { get; set; }
        [XmlElement(ElementName = "Desdobramento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Desdobramento { get; set; }
        [XmlElement(ElementName = "DatadeDesdobramento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string DatadeDesdobramento { get; set; }
        [XmlElement(ElementName = "ProporcaodeDesdobramento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ProporcaodeDesdobramento { get; set; }
        [XmlElement(ElementName = "EsforcoRestrito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string EsforcoRestrito { get; set; }
        [XmlElement(ElementName = "RegistroCVM", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string RegistroCVM { get; set; }
        [XmlElement(ElementName = "Datadoregistrodefinitivo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Datadoregistrodefinitivo { get; set; }
        [XmlElement(ElementName = "Datadoregistroprovisorio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Datadoregistroprovisorio { get; set; }
        [XmlElement(ElementName = "RegistroDefinitivo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string RegistroDefinitivo { get; set; }
        [XmlElement(ElementName = "ResgateAntecipadoUnilateral", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ResgateAntecipadoUnilateral { get; set; }
        [XmlElement(ElementName = "TermoSecuritizacaoFormalizado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string TermoSecuritizacaoFormalizado { get; set; }
        [XmlElement(ElementName = "GarantiaFlutuante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string GarantiaFlutuante { get; set; }
        [XmlElement(ElementName = "LocaldeNegociacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string LocaldeNegociacao { get; set; }
        [XmlElement(ElementName = "MunicipiodePagamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string MunicipiodePagamento { get; set; }
        [XmlElement(ElementName = "CodigoCDA", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodigoCDA { get; set; }
        [XmlElement(ElementName = "CodigoWA", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CodigoWA { get; set; }
        [XmlElement(ElementName = "ContaCETIP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContaCETIP { get; set; }
        [XmlElement(ElementName = "RazaoSocial", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string RazaoSocial { get; set; }
        [XmlElement(ElementName = "Identificacaodosdireitosconferidospelostitulos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Identificacaodosdireitosconferidospelostitulos { get; set; }
        [XmlElement(ElementName = "NumerodeControle", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NumerodeControle { get; set; }
        [XmlElement(ElementName = "Descricaoeespecificacaodoproduto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Descricaoeespecificacaodoproduto { get; set; }
        [XmlElement(ElementName = "Numerodevolumes", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Numerodevolumes { get; set; }
        [XmlElement(ElementName = "Pesobruto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Pesobruto { get; set; }
        [XmlElement(ElementName = "Pesoliquido", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Pesoliquido { get; set; }
        [XmlElement(ElementName = "ContaCETIPDepositante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ContaCETIPDepositante { get; set; }
        [XmlElement(ElementName = "CPFCNPJDepositante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CPFCNPJDepositante { get; set; }
        [XmlElement(ElementName = "NomeRazaoSocialdoDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NomeRazaoSocialdoDepositario { get; set; }
        [XmlElement(ElementName = "CNPJDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CNPJDepositario { get; set; }
        [XmlElement(ElementName = "Qualificacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Qualificacao { get; set; }
        [XmlElement(ElementName = "LogradouroDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string LogradouroDepositario { get; set; }
        [XmlElement(ElementName = "NumeroDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NumeroDepositario { get; set; }
        [XmlElement(ElementName = "ComplementoDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ComplementoDepositario { get; set; }
        [XmlElement(ElementName = "BairroDepositarioExportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string BairroDepositarioExportador { get; set; }
        [XmlElement(ElementName = "CEPDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CEPDepositario { get; set; }
        [XmlElement(ElementName = "EstadoDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string EstadoDepositario { get; set; }
        [XmlElement(ElementName = "MunicipioDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string MunicipioDepositario { get; set; }
        [XmlElement(ElementName = "NomeRazaoSocialdosRepresentantesLegaisdoDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NomeRazaoSocialdosRepresentantesLegaisdoDepositario { get; set; }
        [XmlElement(ElementName = "IdentificacaocomercialdoDepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string IdentificacaocomercialdoDepositario { get; set; }
        [XmlElement(ElementName = "QualificacaodaGarantiaoferecidapelodepositario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string QualificacaodaGarantiaoferecidapelodepositario { get; set; }
        [XmlElement(ElementName = "Seguradordoproduto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Seguradordoproduto { get; set; }
        [XmlElement(ElementName = "Valordoseguro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Valordoseguro { get; set; }
        [XmlElement(ElementName = "Localdoarmazenamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Localdoarmazenamento { get; set; }
        [XmlElement(ElementName = "LogradouroArmazenagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string LogradouroArmazenagem { get; set; }
        [XmlElement(ElementName = "NumeroArmazenagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NumeroArmazenagem { get; set; }
        [XmlElement(ElementName = "ComplementoArmazenagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ComplementoArmazenagem { get; set; }
        [XmlElement(ElementName = "BairroArmazenagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string BairroArmazenagem { get; set; }
        [XmlElement(ElementName = "CEPArmazenagemImportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string CEPArmazenagemImportador { get; set; }
        [XmlElement(ElementName = "MunicipioArmazenagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string MunicipioArmazenagem { get; set; }
        [XmlElement(ElementName = "EstadoArmazenagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string EstadoArmazenagem { get; set; }
        [XmlElement(ElementName = "Dataderecebimentodoproduto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string Dataderecebimentodoproduto { get; set; }
        [XmlElement(ElementName = "NomeRazaoSocialdoResponsavelpeloPagamentodosServicos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string NomeRazaoSocialdoResponsavelpeloPagamentodosServicos { get; set; }
        [XmlElement(ElementName = "ValorouCriterioArmazenagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ValorouCriterioArmazenagem { get; set; }
        [XmlElement(ElementName = "ValorouCriterioConservacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ValorouCriterioConservacao { get; set; }
        [XmlElement(ElementName = "ValorouCriterioExpedicao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ValorouCriterioExpedicao { get; set; }
        [XmlElement(ElementName = "ValorouCriterioTotaldeServicos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string ValorouCriterioTotaldeServicos { get; set; }
        [XmlElement(ElementName = "PeriodicidadeArmazenagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string PeriodicidadeArmazenagem { get; set; }
        [XmlElement(ElementName = "PeriodicidadeConservacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string PeriodicidadeConservacao { get; set; }
        [XmlElement(ElementName = "PeriodicidadeExpedicao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string PeriodicidadeExpedicao { get; set; }
        [XmlElement(ElementName = "PeriodicidadeTotaldeServicos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public string PeriodicidadeTotaldeServicos { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasComplementosAgricolas")]
    public class CaracteristicasComplementosAgricolas
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasComplementosAgricolasRequest")]
    public class ReceberCaracteristicasComplementosAgricolasRequest
    {
        [XmlElement(ElementName = "CaracteristicasComplementosAgricolas")]
        public CaracteristicasComplementosAgricolas CaracteristicasComplementosAgricolas { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
