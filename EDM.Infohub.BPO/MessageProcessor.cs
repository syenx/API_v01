using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Mappers;
using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoes;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoesDMZ;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EDM.Infohub.BPO.Test")]
namespace EDM.Infohub.BPO
{
    public class MessageProcessor
    {
        private ILogger<MessageProcessor> _logger;
        private ILuzService _luzService;
        private Filter _filter;
        private ControleMensagemRepository _bdService;
        private RetryPolicy _retryPolicy;

        private InfohubXmlMapper otherTypesMapper;

        public MessageProcessor(Filter filter, ILuzService luzService, ILogger<MessageProcessor> logger, ControleMensagemRepository bdService)
        {
            _logger = logger;
            _luzService = luzService;
            _filter = filter;
            _bdService = bdService;
            otherTypesMapper = new InfohubXmlMapper(_filter);
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(5, retryAttempt =>
                {
                    var timeToWait = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    _logger.LogWarning($"Mensagem nao enviada, esperando {timeToWait.TotalSeconds} segundos");
                    return timeToWait;
                }
                );
        }
        public void Process(InfohubMessageModel message)
        {

            if (message.type == "CTPACOMOPER")
            {

                var mensagemTratada = CTPACOMOPER(message);
                EnviarMensagem(mensagemTratada, message);

            }
            else
            {
                var mensagemTratada = OtherTypes(message);
                EnviarMensagem(mensagemTratada, message);
            }
        }

        private void EnviarMensagem(ProcessaMensagemModel mensagemTratada, InfohubMessageModel message)
        {
            ProcessaMensagemModel m = (ProcessaMensagemModel)mensagemTratada;
            if (m.Filtrar)
            {
                var response = _retryPolicy.Execute(() => _luzService.EnviarMensagem(m.Message).Result);
                message.rawMessage = mensagemTratada.Message;

                try
                {
                    _bdService.Insert(new ControleMensagemDAO()
                    {
                        dh_processamento = DateTime.Now,
                        cd_id_processamento = response.Id,
                        tp_mensagem = message.type,
                        tx_mensagem = JObject.Parse(JsonConvert.SerializeObject(message)),
                        es_processamento = (StatusClearingEnum)Enum.Parse(typeof(StatusClearingEnum), response.Status.Replace(" ", "").Trim()),
                        es_processamento_btg = "Esperando Retorno"
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError("Erro ao tentar inserir objeto ControleMensagem no banco de dados: " + e.Message);
                    throw e;
                }

                //_dataBaseService.InsertMessage(new ControleMensagem()
                //{
                //    DH_Processamento = DateTime.Now,
                //    CD_ID_Processamento = response.Id,
                //    TP_Mensagem = message.type,
                //    TX_Mensagem = message,
                //    ES_Processamento = (StatusClearingEnum) Enum.Parse(typeof(StatusClearingEnum), response.Status.Replace(" ", "").Trim()),
                //    ES_Processamento_BTG = "Esperando Retorno"
                //});
            }
            else
            {
                _logger.LogInformation("Mensagem nao eh do tipo esperado " + m.Message);
            }
        }

        internal ProcessaMensagemModel OtherTypes(InfohubMessageModel message)
        {

            if (message.Source == "BTG-DMZ")
            {
                return otherTypesMapper.InfohubXmlMapperInvoker(message.type, message.rawMessage, true);
            }
            else
            {

                return otherTypesMapper.InfohubXmlMapperInvoker(message.type, message.rawMessage, false);
            }
        }

        internal ProcessaMensagemModel CTPACOMOPER(InfohubMessageModel message)
        {
            ReceberAcompanhamentoOperacoesRequestDMZ messageDeserialized = null;
            if (message.Source == "BTG-DMZ")
            {
                messageDeserialized = Serealization.Deserialize<ReceberAcompanhamentoOperacoesRequestDMZ>(message.rawMessage, "ReceberAcompanhamentoOperacoesRequest");

                //return AcompOpHideInformation(messageDeserialized);

            }
            else
            {
                AcompOpXmlMapper acompOpMapper = new AcompOpXmlMapper();
                messageDeserialized = acompOpMapper.MapInfohubtoDMZ(Serealization.Deserialize<ReceberAcompanhamentoOperacoesRequest>(message.rawMessage, "receberAcompanhamentoOperacoesRequest"));

                //return AcompOpHideInformation(messageDeserialized);
            }

            return new ProcessaMensagemModel { Message = AcompOpHideInformation(messageDeserialized), Filtrar = _filter.FiltrarMensagem(messageDeserialized.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg.SubTpAtv) };
        }

        internal string AcompOpHideInformation(ReceberAcompanhamentoOperacoesRequestDMZ message)
        {
            //mudança pedida pelo Leandro da Luz, ele usa esse Id como chave da mensagem
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
    }
}
