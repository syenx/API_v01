using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosAtivo;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosAtivoDMZ
{

    [XmlRoot(ElementName = "ReceberAgendaEventosAtivo")]
    public class ReceberAgendaEventosAtivoDMZ
    {
        [XmlElement(ElementName = "AgendaEventosAtivo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAgendaEventosAtivo")]
        public AgendaEventosAtivo AgendaEventosAtivo { get; set; }
    }

    [XmlRoot(ElementName = "ReceberAgendaEventosAtivoRequest")]
    public class ReceberAgendaEventosAtivoRequestDMZ
    {
        [XmlElement(ElementName = "ReceberAgendaEventosAtivo")]
        public ReceberAgendaEventosAtivoDMZ ReceberAgendaEventosAtivo { get; set; }
        //[XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsd { get; set; }
        ////[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsi { get; set; }
    }

}
