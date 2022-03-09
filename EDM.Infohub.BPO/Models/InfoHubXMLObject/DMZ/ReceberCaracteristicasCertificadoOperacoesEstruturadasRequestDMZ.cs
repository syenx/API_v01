using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasCertificadoOperacoesEstruturadas;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasCertificadoOperacoesEstruturadasDMZ
{

    [XmlRoot(ElementName = "ReceberCaracteristicasCertificadoOperacoesEstruturadas")]
    public class ReceberCaracteristicasCertificadoOperacoesEstruturadasDMZ
    {
        [XmlElement(ElementName = "CaracteristicaCertificadoOperaocesEstruturadas", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public CaracteristicaCertificadoOperaocesEstruturadas CaracteristicaCertificadoOperaocesEstruturadas { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasCertificadoOperacoesEstruturadasRequest")]
    public class ReceberCaracteristicasCertificadoOperacoesEstruturadasRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasCertificadoOperacoesEstruturadas")]
        public ReceberCaracteristicasCertificadoOperacoesEstruturadasDMZ ReceberCaracteristicasCertificadoOperacoesEstruturadas { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
    }

}