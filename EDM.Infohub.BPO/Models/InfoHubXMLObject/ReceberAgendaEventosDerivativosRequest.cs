using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosDerivativos
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "GrupoTipoEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
    public class GrupoTipoEvento
    {
        [XmlElement(ElementName = "TipodeEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string TipodeEvento { get; set; }
        [XmlElement(ElementName = "DataOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string DataOriginal { get; set; }
        [XmlElement(ElementName = "DatadaLiquidacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string DatadaLiquidacao { get; set; }
        [XmlElement(ElementName = "ParteContaTitular", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string ParteContaTitular { get; set; }
        [XmlElement(ElementName = "ParteNomeSimplificadoTitular", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string ParteNomeSimplificadoTitular { get; set; }
        [XmlElement(ElementName = "EstadoStatus", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string EstadoStatus { get; set; }
        [XmlElement(ElementName = "ValordoEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string ValordoEvento { get; set; }
        [XmlElement(ElementName = "AmortizacaoPeso", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string AmortizacaoPeso { get; set; }
        [XmlElement(ElementName = "TxdeJuros", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string TxdeJuros { get; set; }
        [XmlElement(ElementName = "TaxaaTermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string TaxaaTermo { get; set; }
        [XmlElement(ElementName = "ValordeReferencia", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string ValordeReferencia { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "GrupoTipoEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public GrupoTipoEvento GrupoTipoEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "AgendaEventosDerivativos")]
    public class AgendaEventosDerivativos
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public BusMsg BusMsg { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "receberAgendaEventosDerivativosRequest")]
    public class ReceberAgendaEventosDerivativosRequest
    {
        [XmlElement(ElementName = "AgendaEventosDerivativos")]
        public AgendaEventosDerivativos AgendaEventosDerivativos { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
