using System.Xml.Serialization;

namespace EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresImobiliario
{
    [XmlRoot(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
    public class SisMsg
    {
        [XmlElement(ElementName = "CodCanal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CodCanal { get; set; }
        [XmlElement(ElementName = "CodGerador", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CodGerador { get; set; }
        [XmlElement(ElementName = "CodMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CodMsg { get; set; }
        [XmlElement(ElementName = "IdMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string IdMsg { get; set; }
        [XmlElement(ElementName = "CodConta", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CodConta { get; set; }
        [XmlElement(ElementName = "ValDataHoraEvento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ValDataHoraEvento { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
    public class BusMsg
    {
        [XmlElement(ElementName = "ContaMonitoradora", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ContaMonitoradora { get; set; }
        [XmlElement(ElementName = "ContaMonitorada", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ContaMonitorada { get; set; }
        [XmlElement(ElementName = "TipoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string TipoIF { get; set; }
        [XmlElement(ElementName = "CodigoIF", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CodigoIF { get; set; }
        [XmlElement(ElementName = "Emissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Emissao { get; set; }
        [XmlElement(ElementName = "Serie", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Serie { get; set; }
        [XmlElement(ElementName = "TipodaSerie", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string TipodaSerie { get; set; }
        [XmlElement(ElementName = "LocalMunicipioCidadedeEmissao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string LocalMunicipioCidadedeEmissao { get; set; }
        [XmlElement(ElementName = "LocaldeNegociacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string LocaldeNegociacao { get; set; }
        [XmlElement(ElementName = "Desdobramento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Desdobramento { get; set; }
        [XmlElement(ElementName = "DatadeDesdobramento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string DatadeDesdobramento { get; set; }
        [XmlElement(ElementName = "ProporcaodeDesdobramento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ProporcaodeDesdobramento { get; set; }
        [XmlElement(ElementName = "ResgateAntecipadoUnilateral", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ResgateAntecipadoUnilateral { get; set; }
        [XmlElement(ElementName = "AtenderequisitosemissaovalorunitinferioraRS30000000", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string AtenderequisitosemissaovalorunitinferioraRS30000000 { get; set; }
        [XmlElement(ElementName = "RegimeFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string RegimeFiduciario { get; set; }
        [XmlElement(ElementName = "ContadoAgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ContadoAgenteFiduciario { get; set; }
        [XmlElement(ElementName = "RazaoSocialAgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string RazaoSocialAgenteFiduciario { get; set; }
        [XmlElement(ElementName = "NaturezaAgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NaturezaAgenteFiduciario { get; set; }
        [XmlElement(ElementName = "CPFCNPJAgenteFiduciario", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CPFCNPJAgenteFiduciario { get; set; }
        [XmlElement(ElementName = "RegistroCVM", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string RegistroCVM { get; set; }
        [XmlElement(ElementName = "Datadoregistroprovisorio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Datadoregistroprovisorio { get; set; }
        [XmlElement(ElementName = "Datadoregistrodefinitivo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Datadoregistrodefinitivo { get; set; }
        [XmlElement(ElementName = "RegistroDefinitivo", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string RegistroDefinitivo { get; set; }
        [XmlElement(ElementName = "Rating1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Rating1 { get; set; }
        [XmlElement(ElementName = "Rating2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Rating2 { get; set; }
        [XmlElement(ElementName = "ClassificadoradeRisco1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ClassificadoradeRisco1 { get; set; }
        [XmlElement(ElementName = "ClassificadoradeRisco2", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ClassificadoradeRisco2 { get; set; }
        [XmlElement(ElementName = "Coobrigacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Coobrigacao { get; set; }
        [XmlElement(ElementName = "GarantiaFlutuante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string GarantiaFlutuante { get; set; }
        [XmlElement(ElementName = "TipodeGarantia1", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string TipodeGarantia1 { get; set; }
        [XmlElement(ElementName = "NaturezaGarantidor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NaturezaGarantidor { get; set; }
        [XmlElement(ElementName = "TermoSecuritizacaoFormalizado", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string TermoSecuritizacaoFormalizado { get; set; }
        [XmlElement(ElementName = "MunicipiodePagamento", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string MunicipiodePagamento { get; set; }
        [XmlElement(ElementName = "AgencianoBancoLiquidante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string AgencianoBancoLiquidante { get; set; }
        [XmlElement(ElementName = "ContaCorrentenoBancoLiquidante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ContaCorrentenoBancoLiquidante { get; set; }
        [XmlElement(ElementName = "CodigodoBancoLiquidante", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CodigodoBancoLiquidante { get; set; }
        [XmlElement(ElementName = "DatadaConstituicaodoCredito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string DatadaConstituicaodoCredito { get; set; }
        [XmlElement(ElementName = "FracionamentodeCCI", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string FracionamentodeCCI { get; set; }
        [XmlElement(ElementName = "PercentualdoCredito", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string PercentualdoCredito { get; set; }
        [XmlElement(ElementName = "CodigodoCartorio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CodigodoCartorio { get; set; }
        [XmlElement(ElementName = "IdentificacaodoCartorio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string IdentificacaodoCartorio { get; set; }
        [XmlElement(ElementName = "MunicipiodoCartorio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string MunicipiodoCartorio { get; set; }
        [XmlElement(ElementName = "UFdoCartorio", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string UFdoCartorio { get; set; }
        [XmlElement(ElementName = "NomeouRazaoSocialCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NomeouRazaoSocialCredorOriginal { get; set; }
        [XmlElement(ElementName = "CPFCNPJCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CPFCNPJCredorOriginal { get; set; }
        [XmlElement(ElementName = "NaturezaCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NaturezaCredorOriginal { get; set; }
        [XmlElement(ElementName = "LogradouroCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string LogradouroCredorOriginal { get; set; }
        [XmlElement(ElementName = "NumeroCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NumeroCredorOriginal { get; set; }
        [XmlElement(ElementName = "ComplementoEnderecoCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ComplementoEnderecoCredorOriginal { get; set; }
        [XmlElement(ElementName = "BairroCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string BairroCredorOriginal { get; set; }
        [XmlElement(ElementName = "CEPCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CEPCredorOriginal { get; set; }
        [XmlElement(ElementName = "EstadoCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string EstadoCredorOriginal { get; set; }
        [XmlElement(ElementName = "MunicipioCredorOriginal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string MunicipioCredorOriginal { get; set; }
        [XmlElement(ElementName = "Apolice", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Apolice { get; set; }
        [XmlElement(ElementName = "Numeracao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Numeracao { get; set; }
        [XmlElement(ElementName = "NumerodeAverbacao", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NumerodeAverbacao { get; set; }
        [XmlElement(ElementName = "Seguro", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Seguro { get; set; }
        [XmlElement(ElementName = "NomeouRazaoSocialDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NomeouRazaoSocialDevedor { get; set; }
        [XmlElement(ElementName = "NaturezaDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NaturezaDevedor { get; set; }
        [XmlElement(ElementName = "CPFCNPJDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CPFCNPJDevedor { get; set; }
        [XmlElement(ElementName = "BairroDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string BairroDevedor { get; set; }
        [XmlElement(ElementName = "LogradouroDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string LogradouroDevedor { get; set; }
        [XmlElement(ElementName = "NumeroDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NumeroDevedor { get; set; }
        [XmlElement(ElementName = "ComplementoEnderecoDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ComplementoEnderecoDevedor { get; set; }
        [XmlElement(ElementName = "CEPDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CEPDevedor { get; set; }
        [XmlElement(ElementName = "EstadoDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string EstadoDevedor { get; set; }
        [XmlElement(ElementName = "MunicipioDevedor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string MunicipioDevedor { get; set; }
        [XmlElement(ElementName = "NomeouRazaoSocialEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NomeouRazaoSocialEmissor { get; set; }
        [XmlElement(ElementName = "CPFCNPJEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CPFCNPJEmissor { get; set; }
        [XmlElement(ElementName = "NaturezaEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NaturezaEmissor { get; set; }
        [XmlElement(ElementName = "LogradouroEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string LogradouroEmissor { get; set; }
        [XmlElement(ElementName = "NumeroEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NumeroEmissor { get; set; }
        [XmlElement(ElementName = "ComplementoEnderecoEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ComplementoEnderecoEmissor { get; set; }
        [XmlElement(ElementName = "BairroEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string BairroEmissor { get; set; }
        [XmlElement(ElementName = "CEPEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CEPEmissor { get; set; }
        [XmlElement(ElementName = "EstadoEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string EstadoEmissor { get; set; }
        [XmlElement(ElementName = "MunicipioEmissor", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string MunicipioEmissor { get; set; }
        [XmlElement(ElementName = "LogradouroIdentificacaodoImovel", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string LogradouroIdentificacaodoImovel { get; set; }
        [XmlElement(ElementName = "NumeroIdentificacaodoImovel", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string NumeroIdentificacaodoImovel { get; set; }
        [XmlElement(ElementName = "ComplementoEnderecoIdentificacaodoImovel", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string ComplementoEnderecoIdentificacaodoImovel { get; set; }
        [XmlElement(ElementName = "BairroIdentificacaodoImovel", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string BairroIdentificacaodoImovel { get; set; }
        [XmlElement(ElementName = "CEPIdentificacaodoImovel", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string CEPIdentificacaodoImovel { get; set; }
        [XmlElement(ElementName = "EstadoIdentificacaodoImovel", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string EstadoIdentificacaodoImovel { get; set; }
        [XmlElement(ElementName = "MunicipioIdentificacaodoImovel", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string MunicipioIdentificacaodoImovel { get; set; }
        [XmlElement(ElementName = "PaisImovel", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string PaisImovel { get; set; }
        [XmlElement(ElementName = "InscricaoMunicipal", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string InscricaoMunicipal { get; set; }
        [XmlElement(ElementName = "Matricula", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public string Matricula { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "CaracteristicasComplementaresImobiliario")]
    public class CaracteristicasComplementaresImobiliario
    {
        [XmlElement(ElementName = "SisMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public SisMsg SisMsg { get; set; }
        [XmlElement(ElementName = "BusMsg", Namespace = "http://cetip.com.br/IntegracaoAdministradores/receberCaracteristicasComplementaresImobiliario")]
        public BusMsg BusMsg { get; set; }
    }

    [XmlRoot(ElementName = "receberCaracteristicasComplementaresImobiliarioRequest")]
    public class ReceberCaracteristicasComplementaresImobiliarioRequest
    {
        [XmlElement(ElementName = "CaracteristicasComplementaresImobiliario")]
        public CaracteristicasComplementaresImobiliario CaracteristicasComplementaresImobiliario { get; set; }
        //        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsd { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
    }

}