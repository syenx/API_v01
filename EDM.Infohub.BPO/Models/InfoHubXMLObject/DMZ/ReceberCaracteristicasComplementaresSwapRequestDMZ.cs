using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresSwap;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresSwapDMZ
{

    [XmlRoot(ElementName = "ReceberCaracteristicasComplementaresSwap")]
    public class ReceberCaracteristicasComplementaresSwapDMZ
    {
        [XmlElement(ElementName = "CaracteristicasComplementaresSwap", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public CaracteristicasComplementaresSwap CaracteristicasComplementaresSwap { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasComplementaresSwapRequest")]
    public class ReceberCaracteristicasComplementaresSwapRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasComplementaresSwap")]
        public ReceberCaracteristicasComplementaresSwapDMZ ReceberCaracteristicasComplementaresSwap { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }
}