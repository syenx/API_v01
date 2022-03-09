using EDM.Infohub.BPO.Models.InfoHubXMLObject.receberCaracteristicasBasicasSwap;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.receberCaracteristicasBasicasSwapDMZ
{

    [XmlRoot(ElementName = "ReceberCaracteristicasBasicasSwap")]
    public class ReceberCaracteristicasBasicasSwapDMZ
    {
        [XmlElement(ElementName = "CaracteristicasBasicasSwap", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasSwap")]
        public CaracteristicasBasicasSwap CaracteristicasBasicasSwap { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasBasicasSwapRequest")]
    public class ReceberCaracteristicasBasicasSwapRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasBasicasSwap")]
        public ReceberCaracteristicasBasicasSwapDMZ ReceberCaracteristicasBasicasSwap { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}
