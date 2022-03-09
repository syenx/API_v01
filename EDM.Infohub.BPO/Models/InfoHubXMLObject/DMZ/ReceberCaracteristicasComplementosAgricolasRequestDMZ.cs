using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementosAgricolas;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementosAgricolasDMZ
{
    [XmlRoot(ElementName = "ReceberCaracteristicasComplementosAgricolas")]
    public class ReceberCaracteristicasComplementosAgricolasDMZ
    {
        [XmlElement(ElementName = "CaracteristicasComplementosAgricolas", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosAgricolas")]
        public CaracteristicasComplementosAgricolas CaracteristicasComplementosAgricolas { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasComplementosAgricolasRequest")]
    public class ReceberCaracteristicasComplementosAgricolasRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasComplementosAgricolas")]
        public ReceberCaracteristicasComplementosAgricolasDMZ ReceberCaracteristicasComplementosAgricolas { get; set; }
        //[XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2001/XMLSchema")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        //public string Xsi { get; set; }
    }
}
