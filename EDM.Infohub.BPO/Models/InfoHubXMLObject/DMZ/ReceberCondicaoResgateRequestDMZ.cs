using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCondicaoResgate;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCondicaoResgateDMZ
{
    [XmlRoot(ElementName = "ReceberCondicaoResgate")]
    public class ReceberCondicaoResgateDMZ
    {
        [XmlElement(ElementName = "CondicaoResgate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public CondicaoResgate CondicaoResgate { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCondicaoResgateRequest")]
    public class ReceberCondicaoResgateRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCondicaoResgate")]
        public ReceberCondicaoResgateDMZ ReceberCondicaoResgate { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}