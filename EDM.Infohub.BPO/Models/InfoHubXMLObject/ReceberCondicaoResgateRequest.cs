using System.Collections.Generic;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCondicaoResgate
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "GrupoCondResg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
    public class GrupoCondResg
    {
        [XmlElement(ElementName = "DataCondResg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string DataCondResg { get; set; }
        [XmlElement(ElementName = "PercTaxaFlutuante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string PercTaxaFlutuante { get; set; }
        [XmlElement(ElementName = "SpreadTaxa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string SpreadTaxa { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CondicaodeResgate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public string CondicaodeResgate { get; set; }
        [XmlElement(ElementName = "GrupoCondResg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public List<GrupoCondResg> GrupoCondResg { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CondicaoResgate")]
    public class CondicaoResgate
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCondicaoResgate")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCondicaoResgateRequest")]
    public class ReceberCondicaoResgateRequest
    {
        [XmlElement(ElementName = "CondicaoResgate")]
        public CondicaoResgate CondicaoResgate { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
