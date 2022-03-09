using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasTermo
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "CodigodaParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigodaParte { get; set; }
        [XmlElement(ElementName = "NomedaParte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string NomedaParte { get; set; }
        [XmlElement(ElementName = "CPFCNPJdoParticipante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CPFCNPJdoParticipante { get; set; }
        [XmlElement(ElementName = "DatadeVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string DatadeVencimento { get; set; }
        [XmlElement(ElementName = "Contrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string Contrato { get; set; }
        [XmlElement(ElementName = "DatadeRegistro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string DatadeRegistro { get; set; }
        [XmlElement(ElementName = "DatadeIniciodeVigencia", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string DatadeIniciodeVigencia { get; set; }
        [XmlElement(ElementName = "CodigodaContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigodaContraparte { get; set; }
        [XmlElement(ElementName = "NomedaContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string NomedaContraparte { get; set; }
        [XmlElement(ElementName = "CPFCNPJdaContraparte", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CPFCNPJdaContraparte { get; set; }
        [XmlElement(ElementName = "CodigoSisbacendaMoedaCotada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigoSisbacendaMoedaCotada { get; set; }
        [XmlElement(ElementName = "SimbolodaMoedaCotada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string SimbolodaMoedaCotada { get; set; }
        [XmlElement(ElementName = "MoedaBase", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string MoedaBase { get; set; }
        [XmlElement(ElementName = "MoedaCotada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string MoedaCotada { get; set; }
        [XmlElement(ElementName = "ValorBasenoregistro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ValorBasenoregistro { get; set; }
        [XmlElement(ElementName = "ValorBaseatual", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ValorBaseatual { get; set; }
        [XmlElement(ElementName = "TaxaForward", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string TaxaForward { get; set; }
        [XmlElement(ElementName = "PosicaodoParticipante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string PosicaodoParticipante { get; set; }
        [XmlElement(ElementName = "DescricaodaposicaodoParticipante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string DescricaodaposicaodoParticipante { get; set; }
        [XmlElement(ElementName = "CrossRate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CrossRate { get; set; }
        [XmlElement(ElementName = "SituacaodoContrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string SituacaodoContrato { get; set; }
        [XmlElement(ElementName = "TipodoContrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string TipodoContrato { get; set; }
        [XmlElement(ElementName = "CodigodoBoletim", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigodoBoletim { get; set; }
        [XmlElement(ElementName = "HorariodoBoletim", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string HorariodoBoletim { get; set; }
        [XmlElement(ElementName = "CodigodaCotacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigodaCotacao { get; set; }
        [XmlElement(ElementName = "NomedoFeeder", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string NomedoFeeder { get; set; }
        [XmlElement(ElementName = "TipodeCotacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string TipodeCotacao { get; set; }
        [XmlElement(ElementName = "TipodeParidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string TipodeParidade { get; set; }
        [XmlElement(ElementName = "DatadeAvaliacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string DatadeAvaliacao { get; set; }
        [XmlElement(ElementName = "CodigodoCedente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigodoCedente { get; set; }
        [XmlElement(ElementName = "CodigodoAdquirente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigodoAdquirente { get; set; }
        [XmlElement(ElementName = "CodigodoAnuente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CodigodoAnuente { get; set; }
        [XmlElement(ElementName = "ContratoGlobal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ContratoGlobal { get; set; }
        [XmlElement(ElementName = "TipoIndicadorFinanceiro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string TipoIndicadorFinanceiro { get; set; }
        [XmlElement(ElementName = "BolsaReferencia", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string BolsaReferencia { get; set; }
        [XmlElement(ElementName = "IndicadorFinanceiro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string IndicadorFinanceiro { get; set; }
        [XmlElement(ElementName = "CriteriodeApuracao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string CriteriodeApuracao { get; set; }
        [XmlElement(ElementName = "UnidadedeNegociacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string UnidadedeNegociacao { get; set; }
        [XmlElement(ElementName = "FontedeInformacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string FontedeInformacao { get; set; }
        [XmlElement(ElementName = "MesVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string MesVencimento { get; set; }
        [XmlElement(ElementName = "AnodeVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string AnodeVencimento { get; set; }
        [XmlElement(ElementName = "QuantidadedeCommoditiesouQuantidadedeindice", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string QuantidadedeCommoditiesouQuantidadedeindice { get; set; }
        [XmlElement(ElementName = "PrecodeOperacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string PrecodeOperacao { get; set; }
        [XmlElement(ElementName = "Premio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string Premio { get; set; }
        [XmlElement(ElementName = "TipoCAP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string TipoCAP { get; set; }
        [XmlElement(ElementName = "Alavancagem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string Alavancagem { get; set; }
        [XmlElement(ElementName = "Limites", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string Limites { get; set; }
        [XmlElement(ElementName = "QuantidadeEventos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string QuantidadeEventos { get; set; }
        [XmlElement(ElementName = "IndicadordeTermoaTermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string IndicadordeTermoaTermo { get; set; }
        [XmlElement(ElementName = "FormadeAtualizacaodataxaatermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string FormadeAtualizacaodataxaatermo { get; set; }
        [XmlElement(ElementName = "ValorouPercentualNegociado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ValorouPercentualNegociado { get; set; }
        [XmlElement(ElementName = "AjustarTaxa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string AjustarTaxa { get; set; }
        [XmlElement(ElementName = "Premioaserpagopelo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string Premioaserpagopelo { get; set; }
        [XmlElement(ElementName = "ValordoPremio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ValordoPremio { get; set; }
        [XmlElement(ElementName = "ModalidadedeLiquidacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public string ModalidadedeLiquidacao { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasBasicasTermo")]
    public class CaracteristicasBasicasTermo
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasTermo")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasBasicasTermoRequest")]
    public class ReceberCaracteristicasBasicasTermoRequest
    {
        [XmlElement(ElementName = "CaracteristicasBasicasTermo")]
        public CaracteristicasBasicasTermo CaracteristicasBasicasTermo { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
