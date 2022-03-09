using EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoes;
using EDM.Infohub.BPO.Models.InfoHubXMLObject.AcompanhamentoOperacoesDMZ;

namespace EDM.Infohub.BPO.Mappers
{
    public class AcompOpXmlMapper
    {
        public ReceberAcompanhamentoOperacoesRequest MapDMZtoInfohub(string rawMessage)
        {
            //XmlRootAttribute xRoot = new XmlRootAttribute();
            //xRoot.ElementName = "ReceberAcompanhamentoOperacoesRequest";
            //XmlSerializer xs = new XmlSerializer(typeof(ReceberAcompanhamentoOperacoesRequestDMZ), xRoot);
            //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(rawMessage));
            //var DMZObj = (ReceberAcompanhamentoOperacoesRequestDMZ)xs.Deserialize(ms);

            var DMZObj = Serealization.Deserialize<ReceberAcompanhamentoOperacoesRequestDMZ>(rawMessage, "ReceberAcompanhamentoOperacoesRequest");


            var InfoHubObj = new ReceberAcompanhamentoOperacoesRequest()
            {
                //Acompanhamento = DMZObj.ReceberAcompanhamentoOperacoes.Acompanhamento,
                Acompanhamento = new Models.InfoHubXMLObject.AcompanhamentoOperacoes.Acompanhamento()
                {
                    SisMsg = DMZObj.ReceberAcompanhamentoOperacoes.Acompanhamento.SisMsg,
                    BusMsg = DMZObj.ReceberAcompanhamentoOperacoes.Acompanhamento.BusMsg
                }
            };

            return InfoHubObj;
        }

        public ReceberAcompanhamentoOperacoesRequestDMZ MapInfohubtoDMZ(ReceberAcompanhamentoOperacoesRequest rawMessage)
        {
            //XmlRootAttribute xRoot = new XmlRootAttribute();
            //xRoot.ElementName = "ReceberAcompanhamentoOperacoesRequest";
            //XmlSerializer xs = new XmlSerializer(typeof(ReceberAcompanhamentoOperacoesRequestDMZ), xRoot);
            //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(rawMessage));
            //var DMZObj = (ReceberAcompanhamentoOperacoesRequestDMZ)xs.Deserialize(ms);

            //var DMZObj = Serealization.Deserialize<ReceberAcompanhamentoOperacoesRequestDMZ>(rawMessage, "ReceberAcompanhamentoOperacoesRequest");


            var InfoHubObj = new ReceberAcompanhamentoOperacoesRequestDMZ()
            {
                ReceberAcompanhamentoOperacoes = new ReceberAcompanhamentoOperacoes()
                {
                    //Acompanhamento = rawMessage.Acompanhamento
                    Acompanhamento = new Models.InfoHubXMLObject.AcompanhamentoOperacoesDMZ.Acompanhamento()
                    {
                        SisMsg = rawMessage.Acompanhamento.SisMsg,
                        BusMsg = rawMessage.Acompanhamento.BusMsg,
                        Xmlns = "http://cetip.com.br/IntegracaoAdministradores/receberAcompanhamentoOperacoes"
                    }
                }
            };

            return InfoHubObj;
        }
    }
}
