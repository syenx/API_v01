using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasAtivos
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "ContadoRegistradorEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ContadoRegistradorEmissor { get; set; }
        [XmlElement(ElementName = "NomeSimplificadoRegistradorEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string NomeSimplificadoRegistradorEmissor { get; set; }
        [XmlElement(ElementName = "ContadoAgentedePagamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ContadoAgentedePagamento { get; set; }
        [XmlElement(ElementName = "NomeSimplificadodoAgentedePagamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string NomeSimplificadodoAgentedePagamento { get; set; }
        [XmlElement(ElementName = "NomeSimplificadoEscriturador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string NomeSimplificadoEscriturador { get; set; }
        [XmlElement(ElementName = "ContaEscriturador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ContaEscriturador { get; set; }
        [XmlElement(ElementName = "DatadeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string DatadeEmissao { get; set; }
        [XmlElement(ElementName = "DatadeVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string DatadeVencimento { get; set; }
        [XmlElement(ElementName = "DatadeRegistro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string DatadeRegistro { get; set; }
        [XmlElement(ElementName = "IniciodeRentabilidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string IniciodeRentabilidade { get; set; }
        [XmlElement(ElementName = "PrazodeEmissaoCreditoDeposito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string PrazodeEmissaoCreditoDeposito { get; set; }
        [XmlElement(ElementName = "QuantidadeEmitida", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string QuantidadeEmitida { get; set; }
        [XmlElement(ElementName = "QuantidadeDepositada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string QuantidadeDepositada { get; set; }
        [XmlElement(ElementName = "QuantidadeResgatada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string QuantidadeResgatada { get; set; }
        [XmlElement(ElementName = "ValorUnitariodeEmissaoCredito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValorUnitariodeEmissaoCredito { get; set; }
        [XmlElement(ElementName = "ValorFinanceirodeEmissaoCredito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValorFinanceirodeEmissaoCredito { get; set; }
        [XmlElement(ElementName = "ValordeOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValordeOriginal { get; set; }
        [XmlElement(ElementName = "ValordeOriginalem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValordeOriginalem { get; set; }
        [XmlElement(ElementName = "ValordeBasedeCalculo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValordeBasedeCalculo { get; set; }
        [XmlElement(ElementName = "ValordeBasedeCalculoem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValordeBasedeCalculoem { get; set; }
        [XmlElement(ElementName = "ValorUnitariodeEmissaoAtualizado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValorUnitariodeEmissaoAtualizado { get; set; }
        [XmlElement(ElementName = "ValorUnitariodeEmissaoAtualizadoem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValorUnitariodeEmissaoAtualizadoem { get; set; }
        [XmlElement(ElementName = "PrecoUnitariodeJuros", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string PrecoUnitariodeJuros { get; set; }
        [XmlElement(ElementName = "PrecoUnitariodeJurosem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string PrecoUnitariodeJurosem { get; set; }
        [XmlElement(ElementName = "PrecoUnitarioAtualizado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string PrecoUnitarioAtualizado { get; set; }
        [XmlElement(ElementName = "PrecoUnitarioAtualizadoem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string PrecoUnitarioAtualizadoem { get; set; }
        [XmlElement(ElementName = "ValorFinanceiroAtualizado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValorFinanceiroAtualizado { get; set; }
        [XmlElement(ElementName = "ValorFinanceiroAtualizadoem", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string ValorFinanceiroAtualizadoem { get; set; }
        [XmlElement(ElementName = "FormadePagamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string FormadePagamento { get; set; }
        [XmlElement(ElementName = "RentabilidadeIndexadorTaxaFlutuante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string RentabilidadeIndexadorTaxaFlutuante { get; set; }
        [XmlElement(ElementName = "DescricaodoindiceVCP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string DescricaodoindiceVCP { get; set; }
        [XmlElement(ElementName = "TipodoIndicadordoindiceVCP", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string TipodoIndicadordoindiceVCP { get; set; }
        [XmlElement(ElementName = "PeriodicidadedeCorrecao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string PeriodicidadedeCorrecao { get; set; }
        [XmlElement(ElementName = "TaxadeJurosSpread", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string TaxadeJurosSpread { get; set; }
        [XmlElement(ElementName = "Criteriodecalculodejuros", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string Criteriodecalculodejuros { get; set; }
        [XmlElement(ElementName = "IncorporaJuros", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string IncorporaJuros { get; set; }
        [XmlElement(ElementName = "PeriodicidadedeJuros", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string PeriodicidadedeJuros { get; set; }
        [XmlElement(ElementName = "TipodeAmortizacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string TipodeAmortizacao { get; set; }
        [XmlElement(ElementName = "DatadoProximoJuros", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string DatadoProximoJuros { get; set; }
        [XmlElement(ElementName = "DatadaProximaAmortizacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string DatadaProximaAmortizacao { get; set; }
        [XmlElement(ElementName = "CodRefBACEN", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CodRefBACEN { get; set; }
        [XmlElement(ElementName = "CodigoISIN", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CodigoISIN { get; set; }
        [XmlElement(ElementName = "Situacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string Situacao { get; set; }
        [XmlElement(ElementName = "IFInadimplente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string IFInadimplente { get; set; }
        [XmlElement(ElementName = "CondicaodeResgateAntecipado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CondicaodeResgateAntecipado { get; set; }
        [XmlElement(ElementName = "DiasuteisFormadeLiquidacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string DiasuteisFormadeLiquidacao { get; set; }
        [XmlElement(ElementName = "DistribuicaoPublica", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string DistribuicaoPublica { get; set; }
        [XmlElement(ElementName = "EsforcoRestrito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string EsforcoRestrito { get; set; }
        [XmlElement(ElementName = "CPFCNPJCliente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string CPFCNPJCliente { get; set; }
        [XmlElement(ElementName = "TipoRegime", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string TipoRegime { get; set; }
        [XmlElement(ElementName = "EventosCursadosCetip", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public string EventosCursadosCetip { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasBasicasAtivos")]
    public class CaracteristicasBasicasAtivos
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasBasicasAtivos")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasBasicasAtivosRequest")]
    public class ReceberCaracteristicasBasicasAtivosRequest
    {
        [XmlElement(ElementName = "CaracteristicasBasicasAtivos")]
        public CaracteristicasBasicasAtivos CaracteristicasBasicasAtivos { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }
}
