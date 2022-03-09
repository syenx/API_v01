using System.Collections.Generic;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAcompanhamentoOperacoesEspecificacao
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "MotivoEnvio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string MotivoEnvio { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string ValDataHoraEvento { get; set; }
        [XmlElement(ElementName = "NumCtrlMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string NumCtrlMsg { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Identificacao_do_Comitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
    public class Identificacao_do_Comitente
    {
        [XmlElement(ElementName = "TpPessoa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string TpPessoa { get; set; }
        [XmlElement(ElementName = "CNPJ_CPF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string CNPJ_CPF { get; set; }
        [XmlElement(ElementName = "QtdCTPEspec", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string QtdCTPEspec { get; set; }
    }

    [XmlRoot(ElementName = "Identificacao_dos_Comitentes", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
    public class Identificacao_dos_Comitentes
    {
        [XmlElement(ElementName = "Identificacao_do_Comitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public List<Identificacao_do_Comitente> Identificacao_do_Comitente { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
    public class BusMsg
    {
        [XmlElement(ElementName = "NumCtrlCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string NumCtrlCTP { get; set; }
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "ContaParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string ContaParte { get; set; }
        [XmlElement(ElementName = "PapelParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string PapelParte { get; set; }
        [XmlElement(ElementName = "Identificacao_dos_Comitentes", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public Identificacao_dos_Comitentes Identificacao_dos_Comitentes { get; set; }
        [XmlElement(ElementName = "ContaContraParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string ContaContraParte { get; set; }
        [XmlElement(ElementName = "PapelContraParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string PapelContraParte { get; set; }
        [XmlElement(ElementName = "CodOpCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string CodOpCTP { get; set; }
        [XmlElement(ElementName = "SubTpAtv", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string SubTpAtv { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "QtdCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string QtdCTP { get; set; }
        [XmlElement(ElementName = "VlrFinanc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string VlrFinanc { get; set; }
        [XmlElement(ElementName = "NumOpPart", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string NumOpPart { get; set; }
        [XmlElement(ElementName = "SitOpCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string SitOpCTP { get; set; }
        [XmlElement(ElementName = "DataHoraSit", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string DataHoraSit { get; set; }
        [XmlElement(ElementName = "DataMovto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public string DataMovto { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CtpOperEsp")]
    public class CtpOperEsp
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoesEspecificacao")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberAcompanhamentoOperacoesEspecificacaoRequest")]
    public class ReceberAcompanhamentoOperacoesEspecificacaoRequest
    {
        [XmlElement(ElementName = "CtpOperEsp")]
        public CtpOperEsp CtpOperEsp { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
