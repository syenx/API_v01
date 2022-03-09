using System.Xml.Serialization;


namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.CaracteristicasComplementosCaptacao
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "Escalonamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string Escalonamento { get; set; }
        [XmlElement(ElementName = "MultiplasCurvas", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string MultiplasCurvas { get; set; }
        [XmlElement(ElementName = "RentabilidadeIndexadorTaxaFlutuanteCurva2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string RentabilidadeIndexadorTaxaFlutuanteCurva2 { get; set; }
        [XmlElement(ElementName = "PeriodicidadedeCorrecaoCurva2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string PeriodicidadedeCorrecaoCurva2 { get; set; }
        [XmlElement(ElementName = "ProratadeCorrecaoCurva2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ProratadeCorrecaoCurva2 { get; set; }
        [XmlElement(ElementName = "TipodecorrecaoCurva2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string TipodecorrecaoCurva2 { get; set; }
        [XmlElement(ElementName = "PercdaTaxaFlutuanteCurva2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string PercdaTaxaFlutuanteCurva2 { get; set; }
        [XmlElement(ElementName = "TaxadeJurosSpreadCurva2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string TaxadeJurosSpreadCurva2 { get; set; }
        [XmlElement(ElementName = "RentabilidadeIndexadorTaxaFlutuanteCurva3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string RentabilidadeIndexadorTaxaFlutuanteCurva3 { get; set; }
        [XmlElement(ElementName = "PeriodicidadedeCorrecaoCurva3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string PeriodicidadedeCorrecaoCurva3 { get; set; }
        [XmlElement(ElementName = "ProratadeCorrecaoCurva3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ProratadeCorrecaoCurva3 { get; set; }
        [XmlElement(ElementName = "TipodecorrecaoCurva3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string TipodecorrecaoCurva3 { get; set; }
        [XmlElement(ElementName = "PercdaTaxaFlutuanteCurva3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string PercdaTaxaFlutuanteCurva3 { get; set; }
        [XmlElement(ElementName = "TaxadeJurosSpreadCurva3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string TaxadeJurosSpreadCurva3 { get; set; }
        [XmlElement(ElementName = "CriteriodecalculodejurosCurva3", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CriteriodecalculodejurosCurva3 { get; set; }
        [XmlElement(ElementName = "ClausuladeConversaoExtincao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ClausuladeConversaoExtincao { get; set; }
        [XmlElement(ElementName = "CriteriosparaConversao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CriteriosparaConversao { get; set; }
        [XmlElement(ElementName = "LimiteMaximodeConversibilidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string LimiteMaximodeConversibilidade { get; set; }
        [XmlElement(ElementName = "PossuiOpcoes", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string PossuiOpcoes { get; set; }
        [XmlElement(ElementName = "DatadoRegistroCVM", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string DatadoRegistroCVM { get; set; }
        [XmlElement(ElementName = "DispensaRegistroCVM", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string DispensaRegistroCVM { get; set; }
        [XmlElement(ElementName = "RegistroCVM", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string RegistroCVM { get; set; }
        [XmlElement(ElementName = "EmissorRazaoSocialEmitente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string EmissorRazaoSocialEmitente { get; set; }
        [XmlElement(ElementName = "Serie", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string Serie { get; set; }
        [XmlElement(ElementName = "DestinacaodoRecursoLei12431", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string DestinacaodoRecursoLei12431 { get; set; }
        [XmlElement(ElementName = "ResppeloDepRetConvPerm", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ResppeloDepRetConvPerm { get; set; }
        [XmlElement(ElementName = "StatusdaDebenture", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string StatusdaDebenture { get; set; }
        [XmlElement(ElementName = "PossibilidadedeResgateAntecipado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string PossibilidadedeResgateAntecipado { get; set; }
        [XmlElement(ElementName = "AgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string AgenteFiduciario { get; set; }
        [XmlElement(ElementName = "DataInclusao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string DataInclusao { get; set; }
        [XmlElement(ElementName = "CompetenciadeDeliberacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CompetenciadeDeliberacao { get; set; }
        [XmlElement(ElementName = "DatadaCompetenciadeDeliberacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string DatadaCompetenciadeDeliberacao { get; set; }
        [XmlElement(ElementName = "PadraoSND", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string PadraoSND { get; set; }
        [XmlElement(ElementName = "CalculaCurva", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CalculaCurva { get; set; }
        [XmlElement(ElementName = "CorrigeoValorNominal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string CorrigeoValorNominal { get; set; }
        [XmlElement(ElementName = "IncorporaJuros", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string IncorporaJuros { get; set; }
        [XmlElement(ElementName = "RentabilidadeMultiplicadorPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string RentabilidadeMultiplicadorPerc { get; set; }
        [XmlElement(ElementName = "Taxa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string Taxa { get; set; }
        [XmlElement(ElementName = "ExpressaodaTaxa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ExpressaodaTaxa { get; set; }
        [XmlElement(ElementName = "PeriodicidadeemDias", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string PeriodicidadeemDias { get; set; }
        [XmlElement(ElementName = "TipodeCalculo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string TipodeCalculo { get; set; }
        [XmlElement(ElementName = "TipodePrazo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string TipodePrazo { get; set; }
        [XmlElement(ElementName = "ValorDoPremio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ValorDoPremio { get; set; }
        [XmlElement(ElementName = "ValorAtualRS", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ValorAtualRS { get; set; }
        [XmlElement(ElementName = "ValorAtualizadoEm", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ValorAtualizadoEm { get; set; }
        [XmlElement(ElementName = "ValorNominalUnitarionaEmissaoRS", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ValorNominalUnitarionaEmissaoRS { get; set; }
        [XmlElement(ElementName = "ValorTotaldaEmissaoRS", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string ValorTotaldaEmissaoRS { get; set; }
        [XmlElement(ElementName = "EmitidocomGarantiasaoFGC", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string EmitidocomGarantiasaoFGC { get; set; }
        [XmlElement(ElementName = "Avalista", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string Avalista { get; set; }
        [XmlElement(ElementName = "NumeroRegistro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string NumeroRegistro { get; set; }
        [XmlElement(ElementName = "TipodeEmissaoTipodeNOTA", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public string TipodeEmissaoTipodeNOTA { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasComplementosCaptacao")]
    public class ComplementosCaptacao
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementosCaptacao")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasComplementosCaptacaoRequest")]
    public class ReceberCaracteristicasComplementosCaptacaoRequest
    {
        [XmlElement(ElementName = "CaracteristicasComplementosCaptacao")]
        public ComplementosCaptacao CaracteristicasComplementosCaptacao { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}