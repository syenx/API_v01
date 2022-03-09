using EDM.Infohub.BPO.Models.InfoHubXMLObject.receberEscalonamentoAtivo;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.receberEscalonamentoAtivoDMZ
{
    [XmlRoot(ElementName = "ReceberEscalonamentoAtivo")]
    public class ReceberEscalonamentoAtivoDMZ
    {
        [XmlElement(ElementName = "EscalonamentoAtivo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public EscalonamentoAtivo EscalonamentoAtivo { get; set; }
    }

    [XmlRoot(ElementName = "ReceberEscalonamentoAtivoRequest")]
    public class ReceberEscalonamentoAtivoRequestDMZ
    {
        [XmlElement(ElementName = "ReceberEscalonamentoAtivo")]
        public ReceberEscalonamentoAtivoDMZ ReceberEscalonamentoAtivo { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}