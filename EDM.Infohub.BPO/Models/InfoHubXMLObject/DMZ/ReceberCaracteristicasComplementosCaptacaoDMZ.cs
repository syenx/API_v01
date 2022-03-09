using EDM.Infohub.BPO.Models.InfoHubXMLObject.CaracteristicasComplementosCaptacao;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.CaracteristicasComplementosCaptacaoDMZ
{
    [XmlRoot(ElementName = "ReceberCaracteristicasComplementosCaptacao")]
    public class ReceberCaracteristicasComplementosCaptacaoDMZ
    {
        [XmlElement(ElementName = "CaracteristicasComplementosCaptacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public ComplementosCaptacao CaracteristicasComplementosCaptacao { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasComplementosCaptacaoRequest")]
    public class ReceberCaracteristicasComplementosCaptacaoRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasComplementosCaptacao")]
        public ReceberCaracteristicasComplementosCaptacaoDMZ ReceberCaracteristicasComplementosCaptacao { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}