using CalendarService;
using EDM.Infohub.Integration;
using EDM.Infohub.Integration.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using static CalendarService.CalendarServiceContractClient;

namespace EDM.Infohub.BPO.Services.impl
{
    public class CalendarService : ICalendar
    {
        private readonly ISecureGateway _secureGateway;
        private CalendarServiceContractClient soapClient;
        private readonly IConfiguration _configuration;

        public string COMMON_SERVICE_URI { get; }

        //private readonly ISecurity _Security;
        public CalendarService(IConfiguration configuration, ISecureGateway secureGateway)
        {
            _configuration = configuration;
            COMMON_SERVICE_URI = _configuration["CalendarService"];
            _secureGateway = secureGateway;
        }


        public DateTime GetNextDay(CalendarRequestCoppClark date)
        {
            var soapClient = new CalendarServiceContractClient(EndpointConfiguration.BasicHttpBinding_ICalendarServiceContract, COMMON_SERVICE_URI);
            //soapClient.InnerChannel.OperationTimeout = new TimeSpan(0, 3, 0);
            string tokenToInsert = _secureGateway.GetToken();
            soapClient.ChannelFactory.Endpoint.EndpointBehaviors.Add(new BtgSecurityBehavior(tokenToInsert));

            using (new OperationContextScope(soapClient.InnerChannel))
            {
                //_Security.SetSOAPHeaders(OperationContext.Current);
                try
                {
                    return soapClient.DateAddWorkDaysCoppClark(date);

                }
                catch
                {
                    soapClient.Abort();
                    throw;
                }
                finally
                {
                    soapClient.CloseAsync();
                }
            }
        }
    }
}
