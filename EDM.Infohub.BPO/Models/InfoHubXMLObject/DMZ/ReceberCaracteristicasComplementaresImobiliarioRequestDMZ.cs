using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresImobiliario;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresImobiliarioDMZ
{
    [XmlRoot(ElementName = "ReceberCaracteristicasComplementaresImobiliario")]
    public class ReceberCaracteristicasComplementaresImobiliarioDMZ
    {
        [XmlElement(ElementName = "CaracteristicasComplementaresImobiliario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public CaracteristicasComplementaresImobiliario CaracteristicasComplementaresImobiliario { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasComplementaresImobiliarioRequest")]
    public class ReceberCaracteristicasComplementaresImobiliarioRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasComplementaresImobiliario")]
        public ReceberCaracteristicasComplementaresImobiliarioDMZ ReceberCaracteristicasComplementaresImobiliario { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}