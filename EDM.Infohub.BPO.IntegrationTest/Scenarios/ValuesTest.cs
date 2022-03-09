using System.Net;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;


namespace EDM.Infohub.BPO.IntegrationTest.Scenarios
{
    public class ValuesTest
    {
        private readonly IntegrationTestFixture _testContext;
        public ValuesTest()
        {
            _testContext = new IntegrationTestFixture();
        }

        [Fact]
        public async Task Values_Get_ReturnsOkResponse()
        {
            var response = await _testContext.Client.GetAsync("/InfohubLuz/autenticacao");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
