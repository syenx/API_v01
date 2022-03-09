using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.receberEscalonamentoAtivo
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "GrupoEscalonamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
    public class GrupoEscalonamento
    {
        [XmlElement(ElementName = "DataaPartir", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string DataaPartir { get; set; }
        [XmlElement(ElementName = "PercTaxaFlutuante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string PercTaxaFlutuante { get; set; }
        [XmlElement(ElementName = "TaxadeJurosSpread", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string TaxadeJurosSpread { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "GrupoEscalonamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public GrupoEscalonamento GrupoEscalonamento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "EscalonamentoAtivo")]
    public class EscalonamentoAtivo
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberEscalonamentoAtivo")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberEscalonamentoAtivoRequest")]
    public class ReceberEscalonamentoAtivoRequest
    {
        [XmlElement(ElementName = "EscalonamentoAtivo")]
        public EscalonamentoAtivo EscalonamentoAtivo { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
