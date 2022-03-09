using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasAtivos;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasAtivosDMZ
{
    [XmlRoot(ElementName = "ReceberCaracteristicasBasicasAtivos")]
    public class ReceberCaracteristicasBasicasAtivosDMZ
    {
        [XmlElement(ElementName = "CaracteristicasBasicasAtivos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public CaracteristicasBasicasAtivos CaracteristicasBasicasAtivos { get; set; }
    }

    [XmlRoot(ElementName = "ReceberCaracteristicasBasicasAtivosRequest")]
    public class ReceberCaracteristicasBasicasAtivosRequestDMZ
    {
        [XmlElement(ElementName = "ReceberCaracteristicasBasicasAtivos")]
        public ReceberCaracteristicasBasicasAtivosDMZ ReceberCaracteristicasBasicasAtivos { get; set; }
        //[XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsd { get; set; }
        ////[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsi { get; set; }
    }

}