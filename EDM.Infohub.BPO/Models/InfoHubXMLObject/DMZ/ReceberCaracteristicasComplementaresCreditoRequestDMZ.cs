using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresCredito;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.CaracteristicasComplementaresCreditoDMZ
{

    [XmlRoot(ElementName = "ReceberCaracteristicasComplementaresCredito")]
    public class ReceberCaracteristicasComplementaresCreditoDMZ
    {
        [XmlElement(ElementName = "CaracteristicasComplementaresCredito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresCredito")]
        public CaracteristicasComplementaresCredito CaracteristicasComplementaresCredito { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasComplementaresCreditoRequest")]
    public class ReceberCaracteristicasComplementaresCreditoRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasComplementaresCredito")]
        public ReceberCaracteristicasComplementaresCreditoDMZ ReceberCaracteristicasComplementaresCredito { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}
