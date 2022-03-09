using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasCffCfa;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasCffCfaDMZ
{

    [XmlRoot(ElementName = "ReceberCaracteristicasBasicasCffCfa")]
    public class ReceberCaracteristicasBasicasCffCfaDMZ
    {
        [XmlElement(ElementName = "CaracteristicasBasicasCffCfa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public CaracteristicasBasicasCffCfa CaracteristicasBasicasCffCfa { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasBasicasCffCfaRequest")]
    public class ReceberCaracteristicasBasicasCffCfaRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasBasicasCffCfa")]
        public ReceberCaracteristicasBasicasCffCfaDMZ ReceberCaracteristicasBasicasCffCfa { get; set; }
        ////[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}
