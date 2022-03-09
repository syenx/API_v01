using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresCredito
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "PercPgto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string PercPgto { get; set; }
        [XmlElement(ElementName = "Chassis", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Chassis { get; set; }
        [XmlElement(ElementName = "NomeSimplificadodoCredorCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string NomeSimplificadodoCredorCredorOriginal { get; set; }
        [XmlElement(ElementName = "ContadoCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ContadoCredorOriginal { get; set; }
        [XmlElement(ElementName = "CodigodoContrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CodigodoContrato { get; set; }
        [XmlElement(ElementName = "EmissorRazaoSocialEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string EmissorRazaoSocialEmitente { get; set; }
        [XmlElement(ElementName = "NaturezaEmissorEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string NaturezaEmissorEmitente { get; set; }
        [XmlElement(ElementName = "CPFCNPJEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CPFCNPJEmitente { get; set; }
        [XmlElement(ElementName = "LocalMunicipioCidadedeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string LocalMunicipioCidadedeEmissao { get; set; }
        [XmlElement(ElementName = "UFdeEmissaoFisicadoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string UFdeEmissaoFisicadoIF { get; set; }
        [XmlElement(ElementName = "OrigemdeCredito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string OrigemdeCredito { get; set; }
        [XmlElement(ElementName = "ValorParcela", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ValorParcela { get; set; }
        [XmlElement(ElementName = "Parcelaacada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Parcelaacada { get; set; }
        [XmlElement(ElementName = "Parcelaacadaapartir", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Parcelaacadaapartir { get; set; }
        [XmlElement(ElementName = "PeriodicidadedaParcela", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string PeriodicidadedaParcela { get; set; }
        [XmlElement(ElementName = "Garantidor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Garantidor { get; set; }
        [XmlElement(ElementName = "NaturezaGarantidor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string NaturezaGarantidor { get; set; }
        [XmlElement(ElementName = "CPFCNPJGarantidor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CPFCNPJGarantidor { get; set; }
        [XmlElement(ElementName = "TipodeGarantia1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string TipodeGarantia1 { get; set; }
        [XmlElement(ElementName = "TipodeGarantia2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string TipodeGarantia2 { get; set; }
        [XmlElement(ElementName = "TipodeGarantia3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string TipodeGarantia3 { get; set; }
        [XmlElement(ElementName = "TipodeGarantia4", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string TipodeGarantia4 { get; set; }
        [XmlElement(ElementName = "TipodeGarantia5", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string TipodeGarantia5 { get; set; }
        [XmlElement(ElementName = "Proprietario1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Proprietario1 { get; set; }
        [XmlElement(ElementName = "Proprietario2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Proprietario2 { get; set; }
        [XmlElement(ElementName = "Proprietario3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Proprietario3 { get; set; }
        [XmlElement(ElementName = "Proprietario4", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Proprietario4 { get; set; }
        [XmlElement(ElementName = "Proprietario5", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Proprietario5 { get; set; }
        [XmlElement(ElementName = "ValorNominalUnitariodeResgate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ValorNominalUnitariodeResgate { get; set; }
        [XmlElement(ElementName = "DescricaoMercadoria", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string DescricaoMercadoria { get; set; }
        [XmlElement(ElementName = "NomeExportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string NomeExportador { get; set; }
        [XmlElement(ElementName = "EnderecoExportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string EnderecoExportador { get; set; }
        [XmlElement(ElementName = "NumeroExportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string NumeroExportador { get; set; }
        [XmlElement(ElementName = "ComplementoExportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ComplementoExportador { get; set; }
        [XmlElement(ElementName = "BairroExportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string BairroExportador { get; set; }
        [XmlElement(ElementName = "CEPExportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CEPExportador { get; set; }
        [XmlElement(ElementName = "CEPImportador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string CEPImportador { get; set; }
        [XmlElement(ElementName = "Prazoemdiascorridos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Prazoemdiascorridos { get; set; }
        [XmlElement(ElementName = "Prazoemdiasuteis", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string Prazoemdiasuteis { get; set; }
        [XmlElement(ElementName = "ConsolidarEventos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ConsolidarEventos { get; set; }
        [XmlElement(ElementName = "DetentordascedulasConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string DetentordascedulasConta { get; set; }
        [XmlElement(ElementName = "DetentordascedulasNomeSimplificado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string DetentordascedulasNomeSimplificado { get; set; }
        [XmlElement(ElementName = "QuantidadedeCedulas", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string QuantidadedeCedulas { get; set; }
        [XmlElement(ElementName = "ValorFinanceiro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public string ValorFinanceiro { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasComplementaresCredito")]
    public class CaracteristicasComplementaresCredito
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasComplementaresCreditoRequest")]
    public class ReceberCaracteristicasComplementaresCreditoRequest
    {
        [XmlElement(ElementName = "CaracteristicasComplementaresCredito")]
        public CaracteristicasComplementaresCredito CaracteristicasComplementaresCredito { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}