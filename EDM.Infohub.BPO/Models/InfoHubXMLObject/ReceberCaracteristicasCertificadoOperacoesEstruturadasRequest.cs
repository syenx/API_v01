using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasCertificadoOperacoesEstruturadas
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ValDataHoraEvento { get; set; }
        [XmlElement(ElementName = "MotivoEnvio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string MotivoEnvio { get; set; }
        [XmlElement(ElementName = "FonteGeradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string FonteGeradora { get; set; }
        [XmlElement(ElementName = "NumCtrlMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string NumCtrlMsg { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "ContaRegistradorEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ContaRegistradorEmissor { get; set; }
        [XmlElement(ElementName = "TipodoCOE", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string TipodoCOE { get; set; }
        [XmlElement(ElementName = "DatadeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string DatadeEmissao { get; set; }
        [XmlElement(ElementName = "DatadeVencimento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string DatadeVencimento { get; set; }
        [XmlElement(ElementName = "PrazodeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string PrazodeEmissao { get; set; }
        [XmlElement(ElementName = "Quantidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Quantidade { get; set; }
        [XmlElement(ElementName = "ValorUnitariodeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ValorUnitariodeEmissao { get; set; }
        [XmlElement(ElementName = "ValorFinanceirodeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ValorFinanceirodeEmissao { get; set; }
        [XmlElement(ElementName = "ContaDetentora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ContaDetentora { get; set; }
        [XmlElement(ElementName = "QuantidadeDepositada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string QuantidadeDepositada { get; set; }
        [XmlElement(ElementName = "EmissaoaTermo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string EmissaoaTermo { get; set; }
        [XmlElement(ElementName = "CodigoISIN", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string CodigoISIN { get; set; }
        [XmlElement(ElementName = "NomeFantasia", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string NomeFantasia { get; set; }
        [XmlElement(ElementName = "Modalidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Modalidade { get; set; }
        [XmlElement(ElementName = "PercdoCapitalGarantido", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string PercdoCapitalGarantido { get; set; }
        [XmlElement(ElementName = "ClassedoAtivoSubjacente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ClassedoAtivoSubjacente { get; set; }
        [XmlElement(ElementName = "AtivoSubjacente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string AtivoSubjacente { get; set; }
        [XmlElement(ElementName = "ValorInicialdoAtivoSubjacente", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ValorInicialdoAtivoSubjacente { get; set; }
        [XmlElement(ElementName = "PercBaseAplicacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string PercBaseAplicacao { get; set; }
        [XmlElement(ElementName = "DistribuicaoPublica", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string DistribuicaoPublica { get; set; }
        [XmlElement(ElementName = "ProtecaocontraProventos", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ProtecaocontraProventos { get; set; }
        [XmlElement(ElementName = "PosicaodoEmissornoDerivativo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string PosicaodoEmissornoDerivativo { get; set; }
        [XmlElement(ElementName = "Remunerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Remunerador { get; set; }
        [XmlElement(ElementName = "DescricaoRemunerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string DescricaoRemunerador { get; set; }
        [XmlElement(ElementName = "PercdeRemuneradorFlutuante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string PercdeRemuneradorFlutuante { get; set; }
        [XmlElement(ElementName = "SpreadCupom", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string SpreadCupom { get; set; }
        [XmlElement(ElementName = "BaseRemunerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BaseRemunerador { get; set; }
        [XmlElement(ElementName = "CondicaoResgate", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string CondicaoResgate { get; set; }
        [XmlElement(ElementName = "VariacaoQuanto", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string VariacaoQuanto { get; set; }
        [XmlElement(ElementName = "Moeda", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Moeda { get; set; }
        [XmlElement(ElementName = "ValorInicialdaParidade", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ValorInicialdaParidade { get; set; }
        [XmlElement(ElementName = "BarreiraPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BarreiraPerc { get; set; }
        [XmlElement(ElementName = "BarreiracenariodealtaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BarreiracenariodealtaPerc { get; set; }
        [XmlElement(ElementName = "BarreiracenariodebaixaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BarreiracenariodebaixaPerc { get; set; }
        [XmlElement(ElementName = "BarreirainferiorPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BarreirainferiorPerc { get; set; }
        [XmlElement(ElementName = "BarreiraKI", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BarreiraKI { get; set; }
        [XmlElement(ElementName = "BarreiraKIKO", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BarreiraKIKO { get; set; }
        [XmlElement(ElementName = "BarreiraKO", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BarreiraKO { get; set; }
        [XmlElement(ElementName = "BarreirasuperiorPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string BarreirasuperiorPerc { get; set; }
        [XmlElement(ElementName = "CupomremuneradoradicionalPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string CupomremuneradoradicionalPerc { get; set; }
        [XmlElement(ElementName = "Datafinalparafixing", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Datafinalparafixing { get; set; }
        [XmlElement(ElementName = "Datafinalparaverificacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Datafinalparaverificacao { get; set; }
        [XmlElement(ElementName = "Datainicialparafixing", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Datainicialparafixing { get; set; }
        [XmlElement(ElementName = "Datainicialparaverificacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Datainicialparaverificacao { get; set; }
        [XmlElement(ElementName = "Dataparafixing", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Dataparafixing { get; set; }
        [XmlElement(ElementName = "Direcaobarreira", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Direcaobarreira { get; set; }
        [XmlElement(ElementName = "Fatorbarreiramovelinferior", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Fatorbarreiramovelinferior { get; set; }
        [XmlElement(ElementName = "Fatorbarreiramovelsuperior", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Fatorbarreiramovelsuperior { get; set; }
        [XmlElement(ElementName = "LimitadorcenariodealtaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string LimitadorcenariodealtaPerc { get; set; }
        [XmlElement(ElementName = "LimitadorcenariodebaixaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string LimitadorcenariodebaixaPerc { get; set; }
        [XmlElement(ElementName = "ParticipacaocenariodealtaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ParticipacaocenariodealtaPerc { get; set; }
        [XmlElement(ElementName = "Participacaocenariodealta2Perc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Participacaocenariodealta2Perc { get; set; }
        [XmlElement(ElementName = "ParticipacaocenariodebaixaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ParticipacaocenariodebaixaPerc { get; set; }
        [XmlElement(ElementName = "Participacaocenariodebaixa2Perc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Participacaocenariodebaixa2Perc { get; set; }
        [XmlElement(ElementName = "Periododecapturadoativosubjacenteparaliquidacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Periododecapturadoativosubjacenteparaliquidacao { get; set; }
        [XmlElement(ElementName = "Periododepagamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Periododepagamento { get; set; }
        [XmlElement(ElementName = "Periododeverificacaodebarreiras", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Periododeverificacaodebarreiras { get; set; }
        [XmlElement(ElementName = "RebateKI", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RebateKI { get; set; }
        [XmlElement(ElementName = "RebateKO", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RebateKO { get; set; }
        [XmlElement(ElementName = "RebatenoCenariodeAlta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RebatenoCenariodeAlta { get; set; }
        [XmlElement(ElementName = "RebatenoCenariodeBaixa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RebatenoCenariodeBaixa { get; set; }
        [XmlElement(ElementName = "RemuneracaoAbaixo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RemuneracaoAbaixo { get; set; }
        [XmlElement(ElementName = "RemuneracaoAcima", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RemuneracaoAcima { get; set; }
        [XmlElement(ElementName = "Remuneracaoadicional", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Remuneracaoadicional { get; set; }
        [XmlElement(ElementName = "RemuneracaodentroPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RemuneracaodentroPerc { get; set; }
        [XmlElement(ElementName = "RemuneracaoforaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RemuneracaoforaPerc { get; set; }
        [XmlElement(ElementName = "Strike1Perc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Strike1Perc { get; set; }
        [XmlElement(ElementName = "Strike2Perc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Strike2Perc { get; set; }
        [XmlElement(ElementName = "TipodeCotacaoparaLiquidacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string TipodeCotacaoparaLiquidacao { get; set; }
        [XmlElement(ElementName = "TipodeCotacaoparaVerificacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string TipodeCotacaoparaVerificacao { get; set; }
        [XmlElement(ElementName = "Tipodecotacaoparaverificacaodebarreiracenariodealta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Tipodecotacaoparaverificacaodebarreiracenariodealta { get; set; }
        [XmlElement(ElementName = "Tipodecotacaoparaverificacaodebarreiracenariodebaixa", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string Tipodecotacaoparaverificacaodebarreiracenariodebaixa { get; set; }
        [XmlElement(ElementName = "VerticedeAltaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string VerticedeAltaPerc { get; set; }
        [XmlElement(ElementName = "VerticedeBaixaPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string VerticedeBaixaPerc { get; set; }
        [XmlElement(ElementName = "ContaEscriturador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string ContaEscriturador { get; set; }
        [XmlElement(ElementName = "RemuneradorflutuanteadicionalPerc", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string RemuneradorflutuanteadicionalPerc { get; set; }
        [XmlElement(ElementName = "TipoRegime", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string TipoRegime { get; set; }
        [XmlElement(ElementName = "EventosCursadosCetip", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public string EventosCursadosCetip { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicaCertificadoOperaocesEstruturadas")]
    public class CaracteristicaCertificadoOperaocesEstruturadas
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasCertificadoOperacoesEstruturadas")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasCertificadoOperacoesEstruturadasRequest")]
    public class ReceberCaracteristicasCertificadoOperacoesEstruturadasRequest
    {
        [XmlElement(ElementName = "CaracteristicaCertificadoOperaocesEstruturadas")]
        public CaracteristicaCertificadoOperaocesEstruturadas CaracteristicaCertificadoOperaocesEstruturadas { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}
