using System;
using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoes
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "MotivoEnvio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string MotivoEnvio { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string ValDataHoraEvento { get; set; }
        [XmlElement(ElementName = "NumCtrlMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string NumCtrlMsg { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
    public class BusMsg
    {
        [XmlElement(ElementName = "NumCtrlCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string NumCtrlCTP { get; set; }
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "ContaParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string ContaParte { get; set; }
        [XmlElement(ElementName = "ContaContraParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string ContaContraParte { get; set; }
        [XmlElement(ElementName = "CodOpCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public Int16 CodOpCTP { get; set; }
        [XmlElement(ElementName = "SubTpAtv", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string SubTpAtv { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "PUNegc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public double PUNegc { get; set; }
        [XmlElement(ElementName = "VlrFinanc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public double VlrFinanc { get; set; }
        [XmlElement(ElementName = "QtdCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string QtdCTP { get; set; }
        [XmlElement(ElementName = "PapelParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string PapelParte { get; set; }
        [XmlElement(ElementName = "PapelContraParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string PapelContraParte { get; set; }
        [XmlElement(ElementName = "NumOpPart", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string NumOpPart { get; set; }
        [XmlElement(ElementName = "NumOpCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string NumOpCTP { get; set; }
        [XmlElement(ElementName = "ModLiq", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string ModLiq { get; set; }
        [XmlElement(ElementName = "SitOpCTP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public Int16 SitOpCTP { get; set; }
        [XmlElement(ElementName = "DataHoraSit", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string DataHoraSit { get; set; }
        [XmlElement(ElementName = "DataMovto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string DataMovto { get; set; }
        [XmlElement(ElementName = "TpCompraVenda", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string TpCompraVenda { get; set; }
        [XmlElement(ElementName = "DataLiquidacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string DataLiquidacao { get; set; }
        [XmlElement(ElementName = "IndLiqAntecipada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public string IndLiqAntecipada { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Acompanhamento")]
    public class Acompanhamento
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes")]
        public BusMsg BusMsg { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "receberAcompanhamentoOperacoesRequest")]
    public class ReceberAcompanhamentoOperacoesRequest
    {
        [XmlElement(ElementName = "Acompanhamento")]
        public Acompanhamento Acompanhamento { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }

    }
}

