using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasCffCfa
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "AdministradorLegal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string AdministradorLegal { get; set; }
        [XmlElement(ElementName = "Carencia", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string Carencia { get; set; }
        [XmlElement(ElementName = "Classe", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string Classe { get; set; }
        [XmlElement(ElementName = "CNPJdoFundo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string CNPJdoFundo { get; set; }
        [XmlElement(ElementName = "CustodeCustodia", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string CustodeCustodia { get; set; }
        [XmlElement(ElementName = "DatadeRegistro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string DatadeRegistro { get; set; }
        [XmlElement(ElementName = "Escriturador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string Escriturador { get; set; }
        [XmlElement(ElementName = "ContaEscriturador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string ContaEscriturador { get; set; }
        [XmlElement(ElementName = "EscrituradorNomeSimplificado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string EscrituradorNomeSimplificado { get; set; }
        [XmlElement(ElementName = "EscrituratorconfirmaEspecificacaodeQuantidadedeCotasdeAplicacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string EscrituratorconfirmaEspecificacaodeQuantidadedeCotasdeAplicacao { get; set; }
        [XmlElement(ElementName = "FundoConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string FundoConta { get; set; }
        [XmlElement(ElementName = "Motivo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string Motivo { get; set; }
        [XmlElement(ElementName = "MotivoInadimplencia", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string MotivoInadimplencia { get; set; }
        [XmlElement(ElementName = "NomeSimplificadoFundoConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string NomeSimplificadoFundoConta { get; set; }
        [XmlElement(ElementName = "QuantidadedeCotasDepositadas", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string QuantidadedeCotasDepositadas { get; set; }
        [XmlElement(ElementName = "QuantidadeTotaldeCotasEmitidas", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string QuantidadeTotaldeCotasEmitidas { get; set; }
        [XmlElement(ElementName = "RazaoSocialFundo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string RazaoSocialFundo { get; set; }
        [XmlElement(ElementName = "RespPeloLancamentoDepositoRetirada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string RespPeloLancamentoDepositoRetirada { get; set; }
        [XmlElement(ElementName = "Serie", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string Serie { get; set; }
        [XmlElement(ElementName = "TipodeFundo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string TipodeFundo { get; set; }
        [XmlElement(ElementName = "ValorAtual", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string ValorAtual { get; set; }
        [XmlElement(ElementName = "ValortotaldaEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public string ValortotaldaEmissao { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasBasicasCffCfa")]
    public class CaracteristicasBasicasCffCfa
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasCffCfa")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasBasicasCffCfaRequest")]
    public class ReceberCaracteristicasBasicasCffCfaRequest
    {
        [XmlElement(ElementName = "CaracteristicasBasicasCffCfa")]
        public CaracteristicasBasicasCffCfa CaracteristicasBasicasCffCfa { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
