using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoes;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoesDMZ;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EDM.Infohub.BPO.Test
{
    public class MessageProcessorTest
    {
        [Fact]
        public void AcompOpHideInformationTest()
        {
            ReceberAcompanhamentoOperacoesRequestDMZ expectedMessage = new ReceberAcompanhamentoOperacoesRequestDMZ()
            {
                ReceberAcompanhamentoOperacoes = new ReceberAcompanhamentoOperacoes
                {
                    Acompanhamento = new Models.InfoHubXMLObject.AcompanhamentoOperacoesDMZ.Acompanhamento
                    {
                        SisMsg = new SisMsg { },
                        BusMsg = new BusMsg { }
                    }
                }
            };

            ReceberAcompanhamentoOperacoesRequestDMZ message = new ReceberAcompanhamentoOperacoesRequestDMZ()
            {
                ReceberAcompanhamentoOperacoes = new ReceberAcompanhamentoOperacoes
                {
                    Acompanhamento = new Models.InfoHubXMLObject.AcompanhamentoOperacoesDMZ.Acompanhamento
                    {
                        SisMsg = new SisMsg { },
                        BusMsg = new BusMsg { }
                    }
                }
            };

            expectedMessage = InitMessageContent(expectedMessage);
            var expected = HideInformation(expectedMessage);
            message = InitMessageContent(message);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x[$"Filtro"]).Returns("CRI,CRA,DEB");
            var secretsMock = new Mock<IAmazonSecretsManager>();
            secretsMock.Setup(x => x.GetSecretValueAsync(It.IsAny<GetSecretValueRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new GetSecretValueResponse() { SecretString = "{\"username\": \"TESTEMOCK\", \"password\": \"SENHATESTEMOCK\"}" }));

            var loggerMock = new Mock<ILogger<MessageProcessor>>();
            var luzServiceMock = new Mock<ILuzService>();
            var filterMock = new Mock<Filter>(configurationMock.Object);
            var bdServiceMock = new Mock<ControleMensagemRepository>(configurationMock.Object, secretsMock.Object);

            var messageProcessorTest = new MessageProcessor(filterMock.Object, luzServiceMock.Object, loggerMock.Object, bdServiceMock.Object);
            var retorno = messageProcessorTest.AcompOpHideInformation(message);

            Assert.Equal(expected, retorno);
        }

        [Theory]
        [InlineData("BTG-DMZ")]
        [InlineData("InfoHub")]
        public void CTPACOMOPERTest(string source)
        {
            //mock
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x[$"Filtro"]).Returns("CRI,CRA,DEB");
            var filterMock = new Mock<Filter>(configurationMock.Object);
            var loggerMock = new Mock<ILogger<MessageProcessor>>();
            var luzServiceMock = new Mock<ILuzService>();
            var secretsMock = new Mock<IAmazonSecretsManager>();
            secretsMock.Setup(x => x.GetSecretValueAsync(It.IsAny<GetSecretValueRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new GetSecretValueResponse() { SecretString = "{\"username\": \"TESTEMOCK\", \"password\": \"SENHATESTEMOCK\"}" }));
            var bdServiceMock = new Mock<ControleMensagemRepository>(configurationMock.Object, secretsMock.Object);

            InfohubMessageModel message = new InfohubMessageModel();
            InitInfohubMessage(message, source);

            MessageProcessor messageProcessorTest = new MessageProcessor(filterMock.Object, luzServiceMock.Object, loggerMock.Object, bdServiceMock.Object);
            ProcessaMensagemModel retorno = messageProcessorTest.CTPACOMOPER(message);

            if (source == "BTG-DMZ")
            {
                string expectedMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ReceberAcompanhamentoOperacoesRequest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><ReceberAcompanhamentoOperacoes><Acompanhamento xmlns=\"http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes\"><SisMsg><CodCanal>WSPACTUAL</CodCanal><CodGerador>INFOHUB</CodGerador><CodMsg>ACOMPOPER</CodMsg><IdMsg>2770904</IdMsg><CodConta>00000.00-0</CodConta><ValDataHoraEvento>2016-09-12T19:16:39</ValDataHoraEvento><NumCtrlMsg>0</NumCtrlMsg></SisMsg><BusMsg><NumCtrlCTP>2016091211335523</NumCtrlCTP><ContaMonitoradora>00000.00-0</ContaMonitoradora><ContaMonitorada>00000.00-0</ContaMonitorada><ContaParte>00000.00-0</ContaParte><ContaContraParte>00000.00-0</ContaContraParte><CodOpCTP>14</CodOpCTP><SubTpAtv>CDB</SubTpAtv><CodigoIF>CDB016F7PWL</CodigoIF><PUNegc>1006.84144</PUNegc><VlrFinanc>1006.84144</VlrFinanc><QtdCTP>1</QtdCTP><PapelParte>AAAAAAAAA</PapelParte><PapelContraParte>AAAAAAAAA</PapelContraParte><NumOpPart>000000</NumOpPart><NumOpCTP>0000000000000000</NumOpCTP><ModLiq>6</ModLiq><SitOpCTP>43</SitOpCTP><DataHoraSit>2016-09-12T19:13:01</DataHoraSit><DataMovto>2016-09-12</DataMovto><TpCompraVenda>0</TpCompraVenda><DataLiquidacao>2016-09-12</DataLiquidacao><IndLiqAntecipada>X</IndLiqAntecipada></BusMsg></Acompanhamento></ReceberAcompanhamentoOperacoes></ReceberAcompanhamentoOperacoesRequest>";
                ProcessaMensagemModel expected = new ProcessaMensagemModel { Message = expectedMessage, Filtrar = false };
                Assert.Equal(expected.Filtrar, retorno.Filtrar);
                Assert.Equal(expected.Message, retorno.Message);
            }
            else
            {
                string expectedMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ReceberAcompanhamentoOperacoesRequest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><ReceberAcompanhamentoOperacoes><Acompanhamento xmlns=\"http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes\"><SisMsg><CodCanal>WSPACTUAL</CodCanal><CodGerador>INFOHUB</CodGerador><CodMsg>ACOMPOPER</CodMsg><IdMsg>21968637</IdMsg><CodConta>00000.00-0</CodConta><MotivoEnvio>OPERACAO</MotivoEnvio><ValDataHoraEvento>2020-07-03T22:57:53</ValDataHoraEvento><NumCtrlMsg>0</NumCtrlMsg></SisMsg><BusMsg><NumCtrlCTP>2020070314169501</NumCtrlCTP><ContaMonitoradora>00000.00-0</ContaMonitoradora><ContaMonitorada>00000.00-0</ContaMonitorada><ContaParte>00000.00-0</ContaParte><ContaContraParte>00000.00-0</ContaContraParte><CodOpCTP>60</CodOpCTP><SubTpAtv>LCA</SubTpAtv><CodigoIF>18G00000721</CodigoIF><PUNegc>99.584042</PUNegc><VlrFinanc>99.584042</VlrFinanc><QtdCTP>1</QtdCTP><PapelParte>AAAAAAAAA</PapelParte><PapelContraParte>AAAAAAAAA</PapelContraParte><NumOpPart>000000</NumOpPart><NumOpCTP>0000000000000000</NumOpCTP><ModLiq>6</ModLiq><SitOpCTP>43</SitOpCTP><DataHoraSit>2020-07-03T22:57:53</DataHoraSit><DataMovto>2020-07-03</DataMovto><TpCompraVenda>0</TpCompraVenda><DataLiquidacao>2020-07-06</DataLiquidacao><IndLiqAntecipada>X</IndLiqAntecipada></BusMsg></Acompanhamento></ReceberAcompanhamentoOperacoes></ReceberAcompanhamentoOperacoesRequest>";
                ProcessaMensagemModel expected = new ProcessaMensagemModel { Message = expectedMessage, Filtrar = false };
                Assert.Equal(expected.Filtrar, retorno.Filtrar);
                Assert.Equal(expected.Message, retorno.Message);
            }
        }

        /*[Theory]
        [InlineData("BTG-DMZ")]
        [InlineData("InfoHub")]
        public void OtherTypesTest(string source)
        {
            //mock
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x[$"Filtro"]).Returns("CRI,CRA,DEB");
            var filterMock = new Mock<Filter>(configurationMock.Object);
            var loggerMock = new Mock<ILogger<MessageProcessor>>();
            var luzServiceMock = new Mock<ILuzService>();
            var secretsMock = new Mock<IAmazonSecretsManager>();
            secretsMock.Setup(x => x.GetSecretValueAsync(It.IsAny<GetSecretValueRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new GetSecretValueResponse() { SecretString = "{\"username\": \"TESTEMOCK\", \"password\": \"SENHATESTEMOCK\"}" }));
            var bdServiceMock = new Mock<ControleMensagemRepository>(configurationMock.Object, secretsMock.Object);
            var otherTypesMapperMock = new Mock<InfohubXmlMapper>(filterMock.Object);

            //expected
            InfohubMessageModel expectedMessage = new InfohubMessageModel();
            InitInfohubMessage(expectedMessage, source);
            ProcessaMensagemModel expected;
            if (source == "BTG-DMZ")
                expected = otherTypesMapperMock.Object.InfohubXmlMapperInvoker(expectedMessage.type, expectedMessage.rawMessage, true);
            else
                expected = otherTypesMapperMock.Object.InfohubXmlMapperInvoker(expectedMessage.type, expectedMessage.rawMessage, false);

            //reality
            InfohubMessageModel message = new InfohubMessageModel();
            InitInfohubMessage(message, source);
            
            MessageProcessor messageProcessorTest = new MessageProcessor(filterMock.Object, luzServiceMock.Object, loggerMock.Object, bdServiceMock.Object);
            ProcessaMensagemModel retorno = messageProcessorTest.OtherTypes(message);

            Assert.Equal(expected.Filtrar, retorno.Filtrar);
            Assert.Equal(expected.Message, retorno.Message);
        }*/

        public ReceberAcompanhamentoOperacoesRequestDMZ InitMessageContent(ReceberAcompanhamentoOperacoesRequestDMZ message)
        {
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.CodCanal = "WSPACTUAL";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.CodConta = "72080.00 - 3";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.CodGerador = "INFOHUB";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.CodMsg = "ACOMPOPER";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.IdMsg = "21968637";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.MotivoEnvio = "OPERACAO";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.NumCtrlMsg = "0";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.ValDataHoraEvento = "2020 - 07 - 03T22: 57:53";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.Xmlns = "";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.CodigoIF = "18G00000721";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.CodOpCTP = 60;
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ContaContraParte = "72080.10-6";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ContaMonitorada = "72080.40-5";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ContaMonitoradora = "72080.00-3";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ContaParte = "72080.40-5";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.DataHoraSit = "2020-07-03T22:57:53";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.DataLiquidacao = "2020-07-06";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.DataMovto = "2020-07-03";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.IndLiqAntecipada = "X";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ModLiq = "6";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.NumCtrlCTP = "2020070314169501";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.NumOpCTP = "2020070314169501";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.NumOpPart = "225752949";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.PapelContraParte = "DETENTOR";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.PapelParte = "EMISSOR";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.PUNegc = 99.584042;
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.QtdCTP = "20";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.SitOpCTP = 43;
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.SubTpAtv = "LCA";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.TpCompraVenda = "2";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.VlrFinanc = -1991.68;
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.Xmlns = "";

            return message;
        }
        public string HideInformation(ReceberAcompanhamentoOperacoesRequestDMZ message)
        {
            //message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.IdMsg = "0";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.CodConta = "00000.00-0";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg.NumCtrlMsg = "0";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ContaMonitoradora = "00000.00-0";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ContaMonitorada = "00000.00-0";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ContaParte = "00000.00-0";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.ContaContraParte = "00000.00-0";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.QtdCTP = "1";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.PapelParte = "AAAAAAAAA";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.PapelContraParte = "AAAAAAAAA";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.VlrFinanc = message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.PUNegc;
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.NumOpPart = "000000";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.NumOpCTP = "0000000000000000";
            message.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.TpCompraVenda = "0";

            return Serealization.Serialize(message);
        }
        public InfohubMessageModel InitInfohubMessage(InfohubMessageModel message, string source)
        {
            message.guid = "teste";
            message.infoHubMessageId = 21968637;
            message.infoHubTime = new DateTime(2020, 07, 04, 01, 57, 53);
            message.institutionAccountCode = "72080.00-3";
            message.instrumentCode = "18G00000721";
            message.lastUpdateTime = new DateTime(2020, 07, 03, 22, 57, 53);
            message.monitoredAccountCode = "72080.40-5";
            message.monitoringAccountCode = "72080.00-3";
            message.receivingTime = new DateTime(2020, 07, 06, 07, 12, 57);
            message.Source = source;
            message.type = "CTPACOMOPER";

            if (source == "BTG-DMZ")
                message.rawMessage = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><ReceberAcompanhamentoOperacoesRequest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><ReceberAcompanhamentoOperacoes><Acompanhamento xmlns=\"http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes\"><SisMsg><CodCanal>WSPACTUAL</CodCanal><CodGerador>INFOHUB</CodGerador><CodMsg>ACOMPOPER</CodMsg><IdMsg>2770904</IdMsg><CodConta>72080.10-6</CodConta><ValDataHoraEvento>2016-09-12T19:16:39</ValDataHoraEvento></SisMsg><BusMsg><NumCtrlCTP>2016091211335523</NumCtrlCTP><ContaMonitoradora>72080.00-3</ContaMonitoradora><ContaMonitorada>72080.10-6</ContaMonitorada><CodOpCTP>14</CodOpCTP><SubTpAtv>CDB</SubTpAtv><CodigoIF>CDB016F7PWL</CodigoIF><QtdCTP>135</QtdCTP><PUNegc>1006.84144</PUNegc><PapelParte>DETENTOR</PapelParte><VlrFinanc>135923.59</VlrFinanc><NumOpPart>6198</NumOpPart><NumOpCTP>2016091211335523</NumOpCTP><ModLiq>6</ModLiq><SitOpCTP>43</SitOpCTP><DataHoraSit>2016-09-12T19:13:01</DataHoraSit><DataMovto>2016-09-12</DataMovto><TpCompraVenda>1</TpCompraVenda><DataLiquidacao>2016-09-12</DataLiquidacao><IndLiqAntecipada>X</IndLiqAntecipada></BusMsg></Acompanhamento></ReceberAcompanhamentoOperacoes></ReceberAcompanhamentoOperacoesRequest>";
            else
                message.rawMessage = "<?xml version=\"1.0\" encoding=\"utf-16\"?> <receberAcompanhamentoOperacoesRequest xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> <Acompanhamento> <SisMsg xmlns=\"http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes\"> <CodCanal>WSPACTUAL</CodCanal> <CodGerador>INFOHUB</CodGerador> <CodMsg>ACOMPOPER</CodMsg> <IdMsg>21968637</IdMsg> <CodConta>72080.00-3</CodConta> <MotivoEnvio>OPERACAO</MotivoEnvio> <ValDataHoraEvento>2020-07-03T22:57:53</ValDataHoraEvento> <NumCtrlMsg>0</NumCtrlMsg> </SisMsg> <BusMsg xmlns=\"http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes\"> <NumCtrlCTP>2020070314169501</NumCtrlCTP> <ContaMonitoradora>72080.00-3</ContaMonitoradora> <ContaMonitorada>72080.40-5</ContaMonitorada> <ContaParte>72080.40-5</ContaParte> <ContaContraParte>72080.10-6</ContaContraParte> <CodOpCTP>60</CodOpCTP> <SubTpAtv>LCA</SubTpAtv> <CodigoIF>18G00000721</CodigoIF> <QtdCTP>20</QtdCTP> <PUNegc>99.584042</PUNegc> <PapelParte>EMISSOR</PapelParte> <PapelContraParte>DETENTOR</PapelContraParte> <VlrFinanc>-1991.68</VlrFinanc> <NumOpPart>225752949</NumOpPart> <NumOpCTP>2020070314169501</NumOpCTP> <ModLiq>6</ModLiq> <SitOpCTP>43</SitOpCTP> <DataHoraSit>2020-07-03T22:57:53</DataHoraSit> <DataMovto>2020-07-03</DataMovto> <TpCompraVenda>2</TpCompraVenda> <DataLiquidacao>2020-07-06</DataLiquidacao> <IndLiqAntecipada>X</IndLiqAntecipada> </BusMsg> </Acompanhamento> </receberAcompanhamentoOperacoesRequest>";

            return message;
        }
    }
}
