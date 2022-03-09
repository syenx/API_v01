using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasOpcoesFlexiveis;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasOpcoesFlexiveisDMZ
{

    [XmlRoot(ElementName = "ReceberCaracteristicasOpcoesFlexiveis")]
    public class ReceberCaracteristicasOpcoesFlexiveisDMZ
    {
        [XmlElement(ElementName = "CaracteristicasOpcoesFlexiveis", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public CaracteristicasOpcoesFlexiveis CaracteristicasOpcoesFlexiveis { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasOpcoesFlexiveisRequest")]
    public class ReceberCaracteristicasOpcoesFlexiveisRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasOpcoesFlexiveis")]
        public ReceberCaracteristicasOpcoesFlexiveisDMZ ReceberCaracteristicasOpcoesFlexiveis { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}
