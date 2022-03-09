using System.Xml.Serialization;


namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresSwap
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "ValorbaseinicialContratoaTermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ValorbaseinicialContratoaTermo { get; set; }
        [XmlElement(ElementName = "DataoperacaotermoContratoaTermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string DataoperacaotermoContratoaTermo { get; set; }
        [XmlElement(ElementName = "indiceTermoContratoaTermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string IndiceTermoContratoaTermo { get; set; }
        [XmlElement(ElementName = "PercentualTermoContratoaTermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string PercentualTermoContratoaTermo { get; set; }
        [XmlElement(ElementName = "PUInicialContratoaTermoVCP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string PUInicialContratoaTermoVCP { get; set; }
        [XmlElement(ElementName = "ParteContraparteTerceiraCurva", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ParteContraparteTerceiraCurva { get; set; }
        [XmlElement(ElementName = "CupomLimpoTerceiraCurva", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string CupomLimpoTerceiraCurva { get; set; }
        [XmlElement(ElementName = "PercentualTerceiraCurva", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string PercentualTerceiraCurva { get; set; }
        [XmlElement(ElementName = "CurvaTerceiraCurva", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string CurvaTerceiraCurva { get; set; }
        [XmlElement(ElementName = "SinalTaxaTerceiraCurva", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string SinalTaxaTerceiraCurva { get; set; }
        [XmlElement(ElementName = "TaxadeJurosTerceiraCurva", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string TaxadeJurosTerceiraCurva { get; set; }
        [XmlElement(ElementName = "LimitadorTerceiraCurva", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string LimitadorTerceiraCurva { get; set; }
        [XmlElement(ElementName = "ParteFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ParteFuncionalidade { get; set; }
        [XmlElement(ElementName = "ParteFatorValorTaxaFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ParteFatorValorTaxaFuncionalidade { get; set; }
        [XmlElement(ElementName = "ParteVerificacaoFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ParteVerificacaoFuncionalidade { get; set; }
        [XmlElement(ElementName = "ParteDataDisparoFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ParteDataDisparoFuncionalidade { get; set; }
        [XmlElement(ElementName = "ContraparteFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ContraparteFuncionalidade { get; set; }
        [XmlElement(ElementName = "ContraparteFatorValorTaxaFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ContraparteFatorValorTaxaFuncionalidade { get; set; }
        [XmlElement(ElementName = "ContraparteVerificacaoFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ContraparteVerificacaoFuncionalidade { get; set; }
        [XmlElement(ElementName = "ContraparteDataDisparoFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string ContraparteDataDisparoFuncionalidade { get; set; }
        [XmlElement(ElementName = "TitularFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string TitularFuncionalidade { get; set; }
        [XmlElement(ElementName = "Premio1Funcionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string Premio1Funcionalidade { get; set; }
        [XmlElement(ElementName = "RebateFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string RebateFuncionalidade { get; set; }
        [XmlElement(ElementName = "LiquidacaodoRebateFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string LiquidacaodoRebateFuncionalidade { get; set; }
        [XmlElement(ElementName = "DiasuteisaposoTriggerOutFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string DiasuteisaposoTriggerOutFuncionalidade { get; set; }
        [XmlElement(ElementName = "Premio2Funcionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string Premio2Funcionalidade { get; set; }
        [XmlElement(ElementName = "DataExercicioPremio2Funcionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string DataExercicioPremio2Funcionalidade { get; set; }
        [XmlElement(ElementName = "AmortizasemTrocadeDiferencialFuncionalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public string AmortizasemTrocadeDiferencialFuncionalidade { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasComplementaresSwap")]
    public class CaracteristicasComplementaresSwap
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresSwap")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasComplementaresSwapRequest")]
    public class ReceberCaracteristicasComplementaresSwapRequest
    {
        [XmlElement(ElementName = "CaracteristicasComplementaresSwap")]
        public CaracteristicasComplementaresSwap CaracteristicasComplementaresSwap { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}