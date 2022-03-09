using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.receberCaracteristicasBasicasSwap
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "TipodeContrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string TipodeContrato { get; set; }
        [XmlElement(ElementName = "DatadeRegistro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string DatadeRegistro { get; set; }
        [XmlElement(ElementName = "Contrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Contrato { get; set; }
        [XmlElement(ElementName = "Participante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Participante { get; set; }
        [XmlElement(ElementName = "Contraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Contraparte { get; set; }
        [XmlElement(ElementName = "DatadeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string DatadeEmissao { get; set; }
        [XmlElement(ElementName = "DatadeVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string DatadeVencimento { get; set; }
        [XmlElement(ElementName = "TipodeAdesao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string TipodeAdesao { get; set; }
        [XmlElement(ElementName = "Valorbase", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Valorbase { get; set; }
        [XmlElement(ElementName = "ValorBaseRemanescente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string ValorBaseRemanescente { get; set; }
        [XmlElement(ElementName = "SinalSaldo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string SinalSaldo { get; set; }
        [XmlElement(ElementName = "DatadoSaldo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string DatadoSaldo { get; set; }
        [XmlElement(ElementName = "AgendadePremio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string AgendadePremio { get; set; }
        [XmlElement(ElementName = "Reset", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Reset { get; set; }
        [XmlElement(ElementName = "Jurosacada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Jurosacada { get; set; }
        [XmlElement(ElementName = "ExpressoemJurosaCada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string ExpressoemJurosaCada { get; set; }
        [XmlElement(ElementName = "Datainiciopagamentojuros", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Datainiciopagamentojuros { get; set; }
        [XmlElement(ElementName = "Amortizacaoacada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Amortizacaoacada { get; set; }
        [XmlElement(ElementName = "ExpressoemAmortizacaoaCada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string ExpressoemAmortizacaoaCada { get; set; }
        [XmlElement(ElementName = "Datainiciopagamentoamortizacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Datainiciopagamentoamortizacao { get; set; }
        [XmlElement(ElementName = "Tipodeamortizacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string Tipodeamortizacao { get; set; }
        [XmlElement(ElementName = "PercentualCurvaAtualizacaoParticipante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string PercentualCurvaAtualizacaoParticipante { get; set; }
        [XmlElement(ElementName = "CodigoindiceCurvaAtualizacaoParticipante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodigoindiceCurvaAtualizacaoParticipante { get; set; }
        [XmlElement(ElementName = "ValorCurvaAtualizadoCurvaAtualizacaoParticipante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string ValorCurvaAtualizadoCurvaAtualizacaoParticipante { get; set; }
        [XmlElement(ElementName = "DataCorrecaoCurvaAtualizacaoParticipante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string DataCorrecaoCurvaAtualizacaoParticipante { get; set; }
        [XmlElement(ElementName = "PercentualCurvaAtualizacaoContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string PercentualCurvaAtualizacaoContraparte { get; set; }
        [XmlElement(ElementName = "CodigoindiceCurvaAtualizacaoContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodigoindiceCurvaAtualizacaoContraparte { get; set; }
        [XmlElement(ElementName = "ValorCurvaAtualizadoCurvaAtualizacaoContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string ValorCurvaAtualizadoCurvaAtualizacaoContraparte { get; set; }
        [XmlElement(ElementName = "DataCorrecaoCurvaAtualizacaoContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string DataCorrecaoCurvaAtualizacaoContraparte { get; set; }
        [XmlElement(ElementName = "TipoLibormoedaCurvaLiborParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string TipoLibormoedaCurvaLiborParte { get; set; }
        [XmlElement(ElementName = "TipoLiborperiodoCurvaLiborParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string TipoLiborperiodoCurvaLiborParte { get; set; }
        [XmlElement(ElementName = "TipoLibormoedaCurvaLiborContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string TipoLibormoedaCurvaLiborContraparte { get; set; }
        [XmlElement(ElementName = "TipoLiborperiodoCurvaLiborContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string TipoLiborperiodoCurvaLiborContraparte { get; set; }
        [XmlElement(ElementName = "CodigoCommodityCurvaCommodityParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodigoCommodityCurvaCommodityParte { get; set; }
        [XmlElement(ElementName = "CodigoCommodityCurvaCommodityContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public string CodigoCommodityCurvaCommodityContraparte { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasBasicasSwap")]
    public class CaracteristicasBasicasSwap
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasBasicasSwapRequest")]
    public class ReceberCaracteristicasBasicasSwapRequest
    {
        [XmlElement(ElementName = "CaracteristicasBasicasSwap")]
        public CaracteristicasBasicasSwap CaracteristicasBasicasSwap { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
