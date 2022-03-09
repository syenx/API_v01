using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.Assinatura;
using EDM.Infohub.BPO.Models.DadosAtuais;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EDM.Infohub.BPO.Services
{
    public interface ILuzService
    {
        Task<string> AutenticationAsync();
        #region Enviar_mensagens_infohub
        Task<EnviarArquivoResponse> EnviarMensagem(string message);
        #endregion

        #region Assinaturas
        Task<List<string>> PapeisAssinados();
        Task<string> AssinarPapel(string papel);
        Task<List<AssinaturaLuzResponse>> AssinarPapelLote(List<AssinaturaLuzRequest> listaPapeis);
        Task<string> RemoverAssinatura(string papel);
        Task<List<AssinaturaLuzResponse>> RemoverPapelLote(List<AssinaturaLuzRequest> listaPapeis);
        #endregion

        #region Precos
        Task<(List<DadosPrecoLuz>, HttpResponseHeaders)> RelatorioPreco(DateTime date, string page);
        Task<List<DadosPrecoLuz>> RelatorioPrecoPapel(DateTime date, string papel);
        #endregion

        #region Precos Historicos
        Task<(List<DadosPrecoLuz>, HttpResponseHeaders)> RelatorioPrecoHistorico(string papel, string page);
        #endregion

        #region Pu de Eventos
        Task<(List<PuDeEventos>, HttpResponseHeaders)> RelatorioPagamento(DateTime date, string page);
        Task<List<PuDeEventos>> RelatorioPagamentoPapel(DateTime date, string papel);
        #endregion

        #region Fluxos
        Task<(List<Fluxos>, HttpResponseHeaders)> RelatorioFluxo(DateTime date, string papel);
        Task<(List<Fluxos>, HttpResponseHeaders)> FluxoPapel(DateTime date, string papel, string page);
        #endregion

        #region Cadastro
        Task<List<DadosCaracteristicos>> RelatorioCaracteristica(DateTime date);
        Task<List<DadosCaracteristicos>> RelatorioCaracteristicaPapel(DateTime date, string papel);
        Task<(List<DadosCaracteristicos>, HttpResponseHeaders)> RelatorioCaracteristica(DateTime data, string hasNext);
        #endregion

    }
}
