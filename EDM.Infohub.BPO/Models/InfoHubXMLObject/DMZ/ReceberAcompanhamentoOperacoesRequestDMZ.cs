using EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoes;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoesDMZ
{
    [XmlRoot(ElementName = "Acompanhamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
    public class Acompanhamento
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public BusMsg BusMsg { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "ReceberAcompanhamentoOperacoes")]
    public class ReceberAcompanhamentoOperacoes
    {
        [XmlElement(ElementName = "Acompanhamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public Acompanhamento Acompanhamento { get; set; }
    }

    [XmlRoot(ElementName = "ReceberAcompanhamentoOperacoesRequest")]
    public class ReceberAcompanhamentoOperacoesRequestDMZ
    {
        [XmlElement(ElementName = "ReceberAcompanhamentoOperacoes")]
        public ReceberAcompanhamentoOperacoes ReceberAcompanhamentoOperacoes { get; set; }
        ////[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsi { get; set; }
        //[XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsd { get; set; }
    }

}
