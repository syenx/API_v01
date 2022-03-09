using System.Collections.Generic;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosAtivo
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "GrupoTipoEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
    public class GrupoTipoEvento
    {
        [XmlElement(ElementName = "DataOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string DataOriginal { get; set; }
        [XmlElement(ElementName = "DatadaEfetivacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string DatadaEfetivacao { get; set; }
        [XmlElement(ElementName = "DatadaLiquidacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string DatadaLiquidacao { get; set; }
        [XmlElement(ElementName = "Liquidacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string Liquidacao { get; set; }
        [XmlElement(ElementName = "NomeEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string NomeEvento { get; set; }
        [XmlElement(ElementName = "StatusEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string StatusEvento { get; set; }
        [XmlElement(ElementName = "TaxadoEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string TaxadoEvento { get; set; }
        [XmlElement(ElementName = "PUEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string PUEvento { get; set; }
        [XmlElement(ElementName = "Incorpora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string Incorpora { get; set; }
        [XmlElement(ElementName = "ValorResidual", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string ValorResidual { get; set; }
        [XmlElement(ElementName = "AgentePagamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string AgentePagamento { get; set; }
        [XmlElement(ElementName = "Observacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string Observacao { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "GrupoTipoEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public List<GrupoTipoEvento> GrupoTipoEvento { get; set; }
        [XmlElement(ElementName = "Sistema", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string Sistema { get; set; }
        [XmlElement(ElementName = "Registrador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string Registrador { get; set; }
        [XmlElement(ElementName = "SituacaoTitulo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public string SituacaoTitulo { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "AgendaEventosAtivo")]
    public class AgendaEventosAtivo
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberAgendaEventosAtivoRequest")]
    public class ReceberAgendaEventosAtivoRequest
    {
        [XmlElement(ElementName = "AgendaEventosAtivo")]
        public AgendaEventosAtivo AgendaEventosAtivo { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
