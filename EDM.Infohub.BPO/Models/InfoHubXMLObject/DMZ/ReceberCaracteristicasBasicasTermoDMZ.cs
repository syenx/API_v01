using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasTermo;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasTermoDMZ
{
    [XmlRoot(ElementName = "ReceberCaracteristicasBasicasTermo")]
    public class ReceberCaracteristicasBasicasTermoDMZ
    {
        [XmlElement(ElementName = "CaracteristicasBasicasTermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public CaracteristicasBasicasTermo CaracteristicasBasicasTermo { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasBasicasTermoRequest")]
    public class ReceberCaracteristicasBasicasTermoRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasBasicasTermo")]
        public ReceberCaracteristicasBasicasTermoDMZ ReceberCaracteristicasBasicasTermo { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}
