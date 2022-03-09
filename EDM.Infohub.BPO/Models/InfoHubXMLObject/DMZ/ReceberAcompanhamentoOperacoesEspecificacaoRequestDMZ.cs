using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAcompanhamentoOperacoesEspecificacao;
using System.Xml.Serialization;


namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAcompanhamentoOperacoesEspecificacaoDMZ
{
    [XmlRoot(ElementName = "ReceberAcompanhamentoOperacoesEspecificacao")]
    public class ReceberAcompanhamentoOperacoesEspecificacaoDMZ
    {
        [XmlElement(ElementName = "CtpOperEsp", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public CtpOperEsp CtpOperEsp { get; set; }
    }

    [XmlRoot(ElementName = "ReceberAcompanhamentoOperacoesEspecificacaoRequest")]
    public class ReceberAcompanhamentoOperacoesEspecificacaoRequestDMZ
    {
        [XmlElement(ElementName = "ReceberAcompanhamentoOperacoesEspecificacao")]
        public ReceberAcompanhamentoOperacoesEspecificacaoDMZ ReceberAcompanhamentoOperacoesEspecificacao { get; set; }
        //[XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsd { get; set; }
        ////[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        ////public string Xsi { get; set; }
    }

}
