using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasOpcoesFlexiveis
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "ContaTitular", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ContaTitular { get; set; }
        [XmlElement(ElementName = "NomedoTitular", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string NomedoTitular { get; set; }
        [XmlElement(ElementName = "ContaLancador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ContaLancador { get; set; }
        [XmlElement(ElementName = "NomedoLancador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string NomedoLancador { get; set; }
        [XmlElement(ElementName = "TipodoContrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TipodoContrato { get; set; }
        [XmlElement(ElementName = "Contrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string Contrato { get; set; }
        [XmlElement(ElementName = "DatadeIniciodeVigencia", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string DatadeIniciodeVigencia { get; set; }
        [XmlElement(ElementName = "DatadeVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string DatadeVencimento { get; set; }
        [XmlElement(ElementName = "CodigoSISBACENdaMoedaBase", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodigoSISBACENdaMoedaBase { get; set; }
        [XmlElement(ElementName = "SimbolodaMoedaBase", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string SimbolodaMoedaBase { get; set; }
        [XmlElement(ElementName = "ValordaAplicacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValordaAplicacao { get; set; }
        [XmlElement(ElementName = "ValorBaseMoedaEstrangeiraQuantidadedaOpcaoEstrategiadeRendaFixa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValorBaseMoedaEstrangeiraQuantidadedaOpcaoEstrategiadeRendaFixa { get; set; }
        [XmlElement(ElementName = "ValorBaseQuantidadeRemanescentedaOpcaoEstrategiadeRendaFixa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValorBaseQuantidadeRemanescentedaOpcaoEstrategiadeRendaFixa { get; set; }
        [XmlElement(ElementName = "ValorResgate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValorResgate { get; set; }
        [XmlElement(ElementName = "DatadeAlteracao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string DatadeAlteracao { get; set; }
        [XmlElement(ElementName = "SituacaodoContrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string SituacaodoContrato { get; set; }
        [XmlElement(ElementName = "TipodeExercicio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TipodeExercicio { get; set; }
        [XmlElement(ElementName = "PrecodeExerciciodeCall", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string PrecodeExerciciodeCall { get; set; }
        [XmlElement(ElementName = "PrecodeExerciciodePut", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string PrecodeExerciciodePut { get; set; }
        [XmlElement(ElementName = "PremioUnitariodeCall", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string PremioUnitariodeCall { get; set; }
        [XmlElement(ElementName = "PremioUnitariodePut", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string PremioUnitariodePut { get; set; }
        [XmlElement(ElementName = "FontedeInformacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string FontedeInformacao { get; set; }
        [XmlElement(ElementName = "Boletim", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string Boletim { get; set; }
        [XmlElement(ElementName = "HorariodoBoletim", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string HorariodoBoletim { get; set; }
        [XmlElement(ElementName = "CotacaoparaVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CotacaoparaVencimento { get; set; }
        [XmlElement(ElementName = "DescricaodeCotacaoparaVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string DescricaodeCotacaoparaVencimento { get; set; }
        [XmlElement(ElementName = "FontedeConsulta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string FontedeConsulta { get; set; }
        [XmlElement(ElementName = "OutrafontedeConsulta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string OutrafontedeConsulta { get; set; }
        [XmlElement(ElementName = "TelaouFuncaodeConsulta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TelaouFuncaodeConsulta { get; set; }
        [XmlElement(ElementName = "PracadeNegociacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string PracadeNegociacao { get; set; }
        [XmlElement(ElementName = "HorariodaConsulta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string HorariodaConsulta { get; set; }
        [XmlElement(ElementName = "CotacaoTaxadeCambioRSUSS", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CotacaoTaxadeCambioRSUSS { get; set; }
        [XmlElement(ElementName = "TipodeParidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TipodeParidade { get; set; }
        [XmlElement(ElementName = "DatadeAvaliacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string DatadeAvaliacao { get; set; }
        [XmlElement(ElementName = "ContaIntermediador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ContaIntermediador { get; set; }
        [XmlElement(ElementName = "NomedoIntermediador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string NomedoIntermediador { get; set; }
        [XmlElement(ElementName = "ComissaoemRSpagapeloTitular", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ComissaoemRSpagapeloTitular { get; set; }
        [XmlElement(ElementName = "ComissaoemRSpagapeloLancador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ComissaoemRSpagapeloLancador { get; set; }
        [XmlElement(ElementName = "ContratoGlobal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ContratoGlobal { get; set; }
        [XmlElement(ElementName = "ValordaAplicacaoValorizado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValordaAplicacaoValorizado { get; set; }
        [XmlElement(ElementName = "ValorBaseOriginalemRS", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValorBaseOriginalemRS { get; set; }
        [XmlElement(ElementName = "ValorBaseRemanescenteemRS", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValorBaseRemanescenteemRS { get; set; }
        [XmlElement(ElementName = "DatadeRegistro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string DatadeRegistro { get; set; }
        [XmlElement(ElementName = "CotacaoBarreira", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CotacaoBarreira { get; set; }
        [XmlElement(ElementName = "CotacaodeAvaliacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CotacaodeAvaliacao { get; set; }
        [XmlElement(ElementName = "TaxadeParidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TaxadeParidade { get; set; }
        [XmlElement(ElementName = "TaxadeAplicacaoPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TaxadeAplicacaoPerc { get; set; }
        [XmlElement(ElementName = "DiasUteisdaAplicacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string DiasUteisdaAplicacao { get; set; }
        [XmlElement(ElementName = "NaturezaEconomicadoTitular", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string NaturezaEconomicadoTitular { get; set; }
        [XmlElement(ElementName = "CodigoSISBACENdaMoedaCotada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodigoSISBACENdaMoedaCotada { get; set; }
        [XmlElement(ElementName = "SimbolodaMoedaCotada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string SimbolodaMoedaCotada { get; set; }
        [XmlElement(ElementName = "Rebate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string Rebate { get; set; }
        [XmlElement(ElementName = "ValordoRebate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ValordoRebate { get; set; }
        [XmlElement(ElementName = "LiquidacaodoRebate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string LiquidacaodoRebate { get; set; }
        [XmlElement(ElementName = "PrecodeExercicioemReais", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string PrecodeExercicioemReais { get; set; }
        [XmlElement(ElementName = "OpcaoQuanto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string OpcaoQuanto { get; set; }
        [XmlElement(ElementName = "CotacaoparaOpcaoQuanto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CotacaoparaOpcaoQuanto { get; set; }
        [XmlElement(ElementName = "DatadeliquidacaodoPremio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string DatadeliquidacaodoPremio { get; set; }
        [XmlElement(ElementName = "AdesaoaoContrato", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string AdesaoaoContrato { get; set; }
        [XmlElement(ElementName = "TipodeBarreira", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TipodeBarreira { get; set; }
        [XmlElement(ElementName = "TriggerIn", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TriggerIn { get; set; }
        [XmlElement(ElementName = "TriggerOut", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TriggerOut { get; set; }
        [XmlElement(ElementName = "MediaOpcaoAsiatica", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string MediaOpcaoAsiatica { get; set; }
        [XmlElement(ElementName = "PrimeiraDatadeVerificacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string PrimeiraDatadeVerificacao { get; set; }
        [XmlElement(ElementName = "NumerodeDatasdeVerificacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string NumerodeDatasdeVerificacao { get; set; }
        [XmlElement(ElementName = "CestadeGarantiasLancador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CestadeGarantiasLancador { get; set; }
        [XmlElement(ElementName = "CodigodaCestadeGarantias", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodigodaCestadeGarantias { get; set; }
        [XmlElement(ElementName = "FormadeVerificacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string FormadeVerificacao { get; set; }
        [XmlElement(ElementName = "CodigodaAcaoIndiceInternacional", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string CodigodaAcaoIndiceInternacional { get; set; }
        [XmlElement(ElementName = "AjustedeProventospelasPartes", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string AjustedeProventospelasPartes { get; set; }
        [XmlElement(ElementName = "ProtecaocontraProventosemDinheiro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string ProtecaocontraProventosemDinheiro { get; set; }
        [XmlElement(ElementName = "EmProventos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string EmProventos { get; set; }
        [XmlElement(ElementName = "TriggerProporcao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TriggerProporcao { get; set; }
        [XmlElement(ElementName = "TriggerFormadeDisparo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TriggerFormadeDisparo { get; set; }
        [XmlElement(ElementName = "TriggerTipodeDisparo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string TriggerTipodeDisparo { get; set; }
        [XmlElement(ElementName = "MercadoriaRIC", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public string MercadoriaRIC { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasOpcoesFlexiveis")]
    public class CaracteristicasOpcoesFlexiveis
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasOpcoesFlexiveis")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasOpcoesFlexiveisRequest")]
    public class ReceberCaracteristicasOpcoesFlexiveisRequest
    {
        [XmlElement(ElementName = "CaracteristicasOpcoesFlexiveis")]
        public CaracteristicasOpcoesFlexiveis CaracteristicasOpcoesFlexiveis { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}