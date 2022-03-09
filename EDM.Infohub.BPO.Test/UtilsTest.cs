using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
namespace EDM.Infohub.BPO.Test
{
    public class UtilsTest
    {
        [Theory]
        [InlineData("2020-01-01 13:13:13.0000")]
        public void GerarCodigoDeRankTest(string date)
        {
            var data = Convert.ToDateTime(date);
            var retorno = Utils.GenerateRank(data);
            Assert.Equal(20200101131313, retorno);
        }

        [Theory]
        [InlineData("PETR30")]
        [InlineData("petr30")]
        [InlineData("peTR30")]
        public void GerarHashTest(string papel)
        {
            byte[] expected = new byte[] { 209, 205, 246, 206, 237, 84, 17, 69, 3, 69, 93, 246, 151, 166, 101, 57 };
            //var data = Convert.ToDateTime(date);
            var retorno = Utils.GenerateHash(papel);
            Assert.Equal(expected, retorno);
        }

        [Theory]
        [InlineData("Base")]
        public void GetSecretTest(string connectionName)
        {
            var expected = "Server=edm-bpo-rf-dev1.cwek932spggu.sa-east-1.rds.amazonaws.com;Database=edm_bpo_rf;Port=5432;User Id=ledm;Password=SENHATESTEMOCK;";
            var configurationMock = new Mock<IConfiguration>();

            //configuration.Setup(a => a.GetSection("TestValueKey")).Returns(configurationSection.Object);
            configurationMock.Setup(x => x.GetSection(It.IsAny<string>())[connectionName]).Returns("Server=edm-bpo-rf-dev1.cwek932spggu.sa-east-1.rds.amazonaws.com;Database=edm_bpo_rf;Port=5432;User Id=ledm;Password=Secret;");

            configurationMock.Setup(x => x["SecretName"]).Returns("ledmdev");

            var secretsMock = new Mock<IAmazonSecretsManager>();

            secretsMock.Setup(x => x.GetSecretValueAsync(It.IsAny<GetSecretValueRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new GetSecretValueResponse() { SecretString = "{\"username\": \"TESTEMOCK\", \"password\": \"SENHATESTEMOCK\"}" }));

            var retorno = Utils.GetSecret(configurationMock.Object, secretsMock.Object, connectionName);

            Assert.Equal(expected, retorno);
        }
    }
}
