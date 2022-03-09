using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;

namespace EDM.Infohub.BPO.DataAccess
{
    public class ControleMensagemRepository : SqlRepository<ControleMensagemDAO>
    {
        public ControleMensagemRepository(IConfiguration configuration, IAmazonSecretsManager secret)
        {
            this._config = configuration;
            this._secret = secret;
        }
    }
}
