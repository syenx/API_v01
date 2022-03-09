using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.CaracteristicasComplementaresCreditoDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.CaracteristicasComplementosCaptacao;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.CaracteristicasComplementosCaptacaoDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresCredito;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresImobiliario;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresImobiliarioDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresSwap;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementaresSwapDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementosAgricolas;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ComplementosAgricolasDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAcompanhamentoOperacoesEspecificacao;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAcompanhamentoOperacoesEspecificacaoDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosAtivo;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosAtivoDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosDerivativos;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberAgendaEventosDerivativosDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasAtivos;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasAtivosDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasCffCfa;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasCffCfaDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.receberCaracteristicasBasicasSwap;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.receberCaracteristicasBasicasSwapDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasTermo;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasBasicasTermoDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasCertificadoOperacoesEstruturadas;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasCertificadoOperacoesEstruturadasDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasOpcoesFlexiveis;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCaracteristicasOpcoesFlexiveisDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCondicaoResgate;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.ReceberCondicaoResgateDMZ;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.receberEscalonamentoAtivo;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.receberEscalonamentoAtivoDMZ;
using EDM.Infohub.BPO.Services.impl;
using System;

namespace EDM.Infohub.BPO.Mappers
{
    public class InfohubXmlMapper
    {

        private Filter _filter;

        public InfohubXmlMapper(Filter filter)
        {
            _filter = filter;
        }

        public ProcessaMensagemModel InfohubXmlMapperInvoker(string methodName, string stringParam, bool ehDMZ)
        {

            var methodData = new object[2];
            methodData[0] = stringParam;
            methodData[1] = ehDMZ;

            Type calledType = GetType();
            //object instance = Activator.CreateInstance(calledType);

            ProcessaMensagemModel response = (ProcessaMensagemModel)calledType.InvokeMember(methodName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic, null, this, methodData);
            return response;
        }

        private ProcessaMensagemModel CTPCOMPLAGRO(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasComplementosAgricolasRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasComplementosAgricolasRequestDMZ>(rawMessage, "ReceberCaracteristicasComplementosAgricolasRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasComplementosAgricolasRequest>(rawMessage, "receberCaracteristicasComplementosAgricolasRequest");
                var auxObj = new ReceberCaracteristicasComplementosAgricolasDMZ()
                {
                    CaracteristicasComplementosAgricolas = infohubObj.CaracteristicasComplementosAgricolas
                };
                obj = new ReceberCaracteristicasComplementosAgricolasRequestDMZ()
                {
                    ReceberCaracteristicasComplementosAgricolas = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasComplementosAgricolas.CaracteristicasComplementosAgricolas.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }

        private ProcessaMensagemModel CTPCOMPLCAPT(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasComplementosCaptacaoRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasComplementosCaptacaoRequestDMZ>(rawMessage, "ReceberCaracteristicasComplementosCaptacaoRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasComplementosCaptacaoRequest>(rawMessage, "receberCaracteristicasComplementosCaptacaoRequest");
                var auxObj = new ReceberCaracteristicasComplementosCaptacaoDMZ()
                {
                    CaracteristicasComplementosCaptacao = infohubObj.CaracteristicasComplementosCaptacao
                };
                obj = new ReceberCaracteristicasComplementosCaptacaoRequestDMZ()
                {
                    ReceberCaracteristicasComplementosCaptacao = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasComplementosCaptacao.CaracteristicasComplementosCaptacao.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }
        private ProcessaMensagemModel CTPCOMPLCRED(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasComplementaresCreditoRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasComplementaresCreditoRequestDMZ>(rawMessage, "ReceberCaracteristicasComplementaresCreditoRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasComplementaresCreditoRequest>(rawMessage, "receberCaracteristicasComplementaresCreditoRequest");
                var auxObj = new ReceberCaracteristicasComplementaresCreditoDMZ()
                {
                    CaracteristicasComplementaresCredito = infohubObj.CaracteristicasComplementaresCredito
                };
                obj = new ReceberCaracteristicasComplementaresCreditoRequestDMZ()
                {
                    ReceberCaracteristicasComplementaresCredito = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasComplementaresCredito.CaracteristicasComplementaresCredito.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };
        }
        private ProcessaMensagemModel CTPCOMPLIMOB(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasComplementaresImobiliarioRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasComplementaresImobiliarioRequestDMZ>(rawMessage, "ReceberCaracteristicasComplementaresImobiliarioRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasComplementaresImobiliarioRequest>(rawMessage, "receberCaracteristicasComplementaresImobiliarioRequest");
                var auxObj = new ReceberCaracteristicasComplementaresImobiliarioDMZ()
                {
                    CaracteristicasComplementaresImobiliario = infohubObj.CaracteristicasComplementaresImobiliario
                };
                obj = new ReceberCaracteristicasComplementaresImobiliarioRequestDMZ()
                {
                    ReceberCaracteristicasComplementaresImobiliario = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasComplementaresImobiliario.CaracteristicasComplementaresImobiliario.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };
        }
        private ProcessaMensagemModel CTPCOMPLSWAP(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasComplementaresSwapRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasComplementaresSwapRequestDMZ>(rawMessage, "ReceberCaracteristicasComplementaresSwapRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasComplementaresSwapRequest>(rawMessage, "receberCaracteristicasComplementaresSwapRequest");

                var auxObj = new ReceberCaracteristicasComplementaresSwapDMZ()
                {
                    CaracteristicasComplementaresSwap = infohubObj.CaracteristicasComplementaresSwap
                };
                obj = new ReceberCaracteristicasComplementaresSwapRequestDMZ()
                {
                    ReceberCaracteristicasComplementaresSwap = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasComplementaresSwap.CaracteristicasComplementaresSwap.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }
        private ProcessaMensagemModel CTPCONDRESG(string rawMessage, bool ehDMZ)
        {
            ReceberCondicaoResgateRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCondicaoResgateRequestDMZ>(rawMessage, "ReceberCondicaoResgateRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCondicaoResgateRequest>(rawMessage, "receberCondicaoResgateRequest");

                var auxObj = new ReceberCondicaoResgateDMZ()
                {
                    CondicaoResgate = infohubObj.CondicaoResgate
                };
                obj = new ReceberCondicaoResgateRequestDMZ()
                {
                    ReceberCondicaoResgate = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCondicaoResgate.CondicaoResgate.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }
        private ProcessaMensagemModel CTPDADOSATIVOS(string rawMessage, bool ehDMZ)
        {
            var obj = new ReceberCaracteristicasBasicasAtivosRequestDMZ();
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasBasicasAtivosRequestDMZ>(rawMessage, "ReceberCaracteristicasBasicasAtivosRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasBasicasAtivosRequest>(rawMessage, "receberCaracteristicasBasicasAtivosRequest");
                var auxObj = new ReceberCaracteristicasBasicasAtivosDMZ()
                {
                    CaracteristicasBasicasAtivos = infohubObj.CaracteristicasBasicasAtivos
                };
                obj = new ReceberCaracteristicasBasicasAtivosRequestDMZ()
                {
                    ReceberCaracteristicasBasicasAtivos = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasBasicasAtivos.CaracteristicasBasicasAtivos.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

            //var infohubObj = Serealization.Deserialize<ReceberCaracteristicasBasicasAtivosRequest>(rawMessage, "receberCaracteristicasBasicasAtivosRequest");

            //var auxObj = new ReceberCaracteristicasBasicasAtivosDMZ()
            //{
            //    CaracteristicasBasicasAtivos = infohubObj.CaracteristicasBasicasAtivos
            //};
            //var dmzObj = new ReceberCaracteristicasBasicasAtivosRequestDMZ()
            //{
            //    ReceberCaracteristicasBasicasAtivos = auxObj
            //};
            //var message = Serealization.Serialize(dmzObj);

            //return message;
        }
        private ProcessaMensagemModel CTPDADOSCOE(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasCertificadoOperacoesEstruturadasRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasCertificadoOperacoesEstruturadasRequestDMZ>(rawMessage, "ReceberCaracteristicasCertificadoOperacoesEstruturadasRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasCertificadoOperacoesEstruturadasRequest>(rawMessage, "receberCaracteristicasCertificadoOperacoesEstruturadasRequest");

                var auxObj = new ReceberCaracteristicasCertificadoOperacoesEstruturadasDMZ()
                {
                    CaracteristicaCertificadoOperaocesEstruturadas = infohubObj.CaracteristicaCertificadoOperaocesEstruturadas
                };
                obj = new ReceberCaracteristicasCertificadoOperacoesEstruturadasRequestDMZ()
                {
                    ReceberCaracteristicasCertificadoOperacoesEstruturadas = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasCertificadoOperacoesEstruturadas.CaracteristicaCertificadoOperaocesEstruturadas.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }
        private ProcessaMensagemModel CTPDADOSESCAL(string rawMessage, bool ehDMZ)
        {
            ReceberEscalonamentoAtivoRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberEscalonamentoAtivoRequestDMZ>(rawMessage, "ReceberEscalonamentoAtivoRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberEscalonamentoAtivoRequest>(rawMessage, "receberEscalonamentoAtivoRequest");

                var auxObj = new ReceberEscalonamentoAtivoDMZ()
                {
                    EscalonamentoAtivo = infohubObj.EscalonamentoAtivo
                };
                obj = new ReceberEscalonamentoAtivoRequestDMZ()
                {
                    ReceberEscalonamentoAtivo = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberEscalonamentoAtivo.EscalonamentoAtivo.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }
        private ProcessaMensagemModel CTPDADOSFUNDOS(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasBasicasCffCfaRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasBasicasCffCfaRequestDMZ>(rawMessage, "ReceberCaracteristicasBasicasCffCfaRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasBasicasCffCfaRequest>(rawMessage, "receberCaracteristicasBasicasCffCfaRequest");

                var auxObj = new ReceberCaracteristicasBasicasCffCfaDMZ()
                {
                    CaracteristicasBasicasCffCfa = infohubObj.CaracteristicasBasicasCffCfa
                };
                obj = new ReceberCaracteristicasBasicasCffCfaRequestDMZ()
                {
                    ReceberCaracteristicasBasicasCffCfa = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasBasicasCffCfa.CaracteristicasBasicasCffCfa.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };
        }
        private ProcessaMensagemModel CTPDADOSOPCAO(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasOpcoesFlexiveisRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasOpcoesFlexiveisRequestDMZ>(rawMessage, "ReceberCaracteristicasOpcoesFlexiveisRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasOpcoesFlexiveisRequest>(rawMessage, "receberCaracteristicasOpcoesFlexiveisRequest");

                var auxObj = new ReceberCaracteristicasOpcoesFlexiveisDMZ()
                {
                    CaracteristicasOpcoesFlexiveis = infohubObj.CaracteristicasOpcoesFlexiveis
                };
                obj = new ReceberCaracteristicasOpcoesFlexiveisRequestDMZ()
                {
                    ReceberCaracteristicasOpcoesFlexiveis = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasOpcoesFlexiveis.CaracteristicasOpcoesFlexiveis.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };
        }
        private ProcessaMensagemModel CTPDADOSSWAP(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasBasicasSwapRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasBasicasSwapRequestDMZ>(rawMessage, "ReceberCaracteristicasBasicasSwapRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasBasicasSwapRequest>(rawMessage, "receberCaracteristicasBasicasSwapRequest");

                var auxObj = new ReceberCaracteristicasBasicasSwapDMZ()
                {
                    CaracteristicasBasicasSwap = infohubObj.CaracteristicasBasicasSwap
                };
                obj = new ReceberCaracteristicasBasicasSwapRequestDMZ()
                {
                    ReceberCaracteristicasBasicasSwap = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasBasicasSwap.CaracteristicasBasicasSwap.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };
        }
        private ProcessaMensagemModel CTPDADOSTERMO(string rawMessage, bool ehDMZ)
        {
            ReceberCaracteristicasBasicasTermoRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberCaracteristicasBasicasTermoRequestDMZ>(rawMessage, "ReceberCaracteristicasBasicasTermoRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberCaracteristicasBasicasTermoRequest>(rawMessage, "receberCaracteristicasBasicasTermoRequest");

                var auxObj = new ReceberCaracteristicasBasicasTermoDMZ()
                {
                    CaracteristicasBasicasTermo = infohubObj.CaracteristicasBasicasTermo
                };
                obj = new ReceberCaracteristicasBasicasTermoRequestDMZ()
                {
                    ReceberCaracteristicasBasicasTermo = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberCaracteristicasBasicasTermo.CaracteristicasBasicasTermo.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }
        private ProcessaMensagemModel CTPEVENATIVOS(string rawMessage, bool ehDMZ)
        {
            ReceberAgendaEventosAtivoRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberAgendaEventosAtivoRequestDMZ>(rawMessage, "ReceberAgendaEventosAtivoRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberAgendaEventosAtivoRequest>(rawMessage, "receberAgendaEventosAtivoRequest");

                var auxObj = new ReceberAgendaEventosAtivoDMZ()
                {
                    AgendaEventosAtivo = infohubObj.AgendaEventosAtivo
                };
                obj = new ReceberAgendaEventosAtivoRequestDMZ()
                {
                    ReceberAgendaEventosAtivo = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberAgendaEventosAtivo.AgendaEventosAtivo.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }
        private ProcessaMensagemModel CTPEVENDERIVAT(string rawMessage, bool ehDMZ)
        {
            ReceberAgendaEventosDerivativosRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberAgendaEventosDerivativosRequestDMZ>(rawMessage, "ReceberAgendaEventosDerivativosRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberAgendaEventosDerivativosRequest>(rawMessage, "receberAgendaEventosDerivativosRequest");

                var auxObj = new ReceberAgendaEventosDerivativosDMZ()
                {
                    AgendaEventosDerivativos = infohubObj.AgendaEventosDerivativos
                };
                obj = new ReceberAgendaEventosDerivativosRequestDMZ()
                {
                    ReceberAgendaEventosDerivativos = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberAgendaEventosDerivativos.AgendaEventosDerivativos.BusMsg.TipoIF);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };

        }
        private ProcessaMensagemModel CTPOPERESP(string rawMessage, bool ehDMZ)
        {
            ReceberAcompanhamentoOperacoesEspecificacaoRequestDMZ obj;
            if (ehDMZ)
            {
                obj = Serealization.Deserialize<ReceberAcompanhamentoOperacoesEspecificacaoRequestDMZ>(rawMessage, "ReceberAcompanhamentoOperacoesEspecificacaoRequest");
            }
            else
            {
                var infohubObj = Serealization.Deserialize<ReceberAcompanhamentoOperacoesEspecificacaoRequest>(rawMessage, "receberAcompanhamentoOperacoesEspecificacaoRequest");

                var auxObj = new ReceberAcompanhamentoOperacoesEspecificacaoDMZ()
                {
                    CtpOperEsp = infohubObj.CtpOperEsp
                };
                obj = new ReceberAcompanhamentoOperacoesEspecificacaoRequestDMZ()
                {
                    ReceberAcompanhamentoOperacoesEspecificacao = auxObj
                };

            }

            var filtrar = _filter.FiltrarMensagem(obj.ReceberAcompanhamentoOperacoesEspecificacao.CtpOperEsp.BusMsg.SubTpAtv);
            var message = Serealization.Serialize(obj);

            return new ProcessaMensagemModel { Message = message, Filtrar = filtrar };
        }

    }
}
