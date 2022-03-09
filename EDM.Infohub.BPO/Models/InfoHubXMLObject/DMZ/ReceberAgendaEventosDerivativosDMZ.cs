using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosDerivativos;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosDerivativosDMZ
{
    [XmlRoot(ElementName = "ReceberAgendaEventosDerivativos")]
    public class ReceberAgendaEventosDerivativosDMZ
    {
        [XmlElement(ElementName = "AgendaEventosDerivativos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosDerivativos")]
        public AgendaEventosDerivativos AgendaEventosDerivativos { get; set; }
    }

    [XmlRoot(ElementName = "ReceberAgendaEventosDerivativosRequest")]
    public class ReceberAgendaEventosDerivativosRequestDMZ
    {
        [XmlElement(ElementName = "ReceberAgendaEventosDerivativos")]
        public ReceberAgendaEventosDerivativosDMZ ReceberAgendaEventosDerivativos { get; set; }
        //[XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsd { get; set; }
        ////[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsi { get; set; }
    }

}
