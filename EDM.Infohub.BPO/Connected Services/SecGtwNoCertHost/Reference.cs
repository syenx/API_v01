﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SecureGatewayService
{
    using Microsoft.Extensions.Configuration;
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AuthRespDataContract", Namespace = "http://btgpactual.com/SecureGateway")]
    public partial class AuthRespDataContract : object
    {

        private SecureGatewayService.Environments EnvironmentField;

        private string IdentityField;

        private bool IsAuthenticatedField;

        private string MessageResultField;

        private int SystemCodeField;

        private string SystemNameField;

        private string TokenField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public SecureGatewayService.Environments Environment
        {
            get
            {
                return this.EnvironmentField;
            }
            set
            {
                this.EnvironmentField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Identity
        {
            get
            {
                return this.IdentityField;
            }
            set
            {
                this.IdentityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsAuthenticated
        {
            get
            {
                return this.IsAuthenticatedField;
            }
            set
            {
                this.IsAuthenticatedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MessageResult
        {
            get
            {
                return this.MessageResultField;
            }
            set
            {
                this.MessageResultField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SystemCode
        {
            get
            {
                return this.SystemCodeField;
            }
            set
            {
                this.SystemCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SystemName
        {
            get
            {
                return this.SystemNameField;
            }
            set
            {
                this.SystemNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token
        {
            get
            {
                return this.TokenField;
            }
            set
            {
                this.TokenField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Environments", Namespace = "http://btgpactual.com/SecureGateway")]
    public enum Environments : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Develop = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Homolog = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Production = 2,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AuthUserRespDataContract", Namespace = "http://btgpactual.com/SecureGateway")]
    public partial class AuthUserRespDataContract : object
    {

        private string EmailField;

        private SecureGatewayService.Environments EnvironmentField;

        private string IdentityField;

        private bool IsAuthenticatedField;

        private string MessageResultField;

        private int SystemCodeField;

        private string SystemNameField;

        private string TokenField;

        private int UserCGEField;

        private string UserLoginField;

        private string UserNameField;

        private string[] UserTransactionsField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public SecureGatewayService.Environments Environment
        {
            get
            {
                return this.EnvironmentField;
            }
            set
            {
                this.EnvironmentField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Identity
        {
            get
            {
                return this.IdentityField;
            }
            set
            {
                this.IdentityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsAuthenticated
        {
            get
            {
                return this.IsAuthenticatedField;
            }
            set
            {
                this.IsAuthenticatedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MessageResult
        {
            get
            {
                return this.MessageResultField;
            }
            set
            {
                this.MessageResultField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SystemCode
        {
            get
            {
                return this.SystemCodeField;
            }
            set
            {
                this.SystemCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SystemName
        {
            get
            {
                return this.SystemNameField;
            }
            set
            {
                this.SystemNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token
        {
            get
            {
                return this.TokenField;
            }
            set
            {
                this.TokenField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserCGE
        {
            get
            {
                return this.UserCGEField;
            }
            set
            {
                this.UserCGEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserLogin
        {
            get
            {
                return this.UserLoginField;
            }
            set
            {
                this.UserLoginField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName
        {
            get
            {
                return this.UserNameField;
            }
            set
            {
                this.UserNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] UserTransactions
        {
            get
            {
                return this.UserTransactionsField;
            }
            set
            {
                this.UserTransactionsField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AuthComponentRespDataContract", Namespace = "http://btgpactual.com/SecureGateway")]
    public partial class AuthComponentRespDataContract : object
    {

        private int ComponentCodeField;

        private string ComponentNameField;

        private SecureGatewayService.Environments EnvironmentField;

        private string IdentityField;

        private bool IsAuthenticatedField;

        private string MessageResultField;

        private int TokenSystemCodeField;

        private string TokenSystemNameField;

        private string TokenUserLoginField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ComponentCode
        {
            get
            {
                return this.ComponentCodeField;
            }
            set
            {
                this.ComponentCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ComponentName
        {
            get
            {
                return this.ComponentNameField;
            }
            set
            {
                this.ComponentNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public SecureGatewayService.Environments Environment
        {
            get
            {
                return this.EnvironmentField;
            }
            set
            {
                this.EnvironmentField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Identity
        {
            get
            {
                return this.IdentityField;
            }
            set
            {
                this.IdentityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsAuthenticated
        {
            get
            {
                return this.IsAuthenticatedField;
            }
            set
            {
                this.IsAuthenticatedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MessageResult
        {
            get
            {
                return this.MessageResultField;
            }
            set
            {
                this.MessageResultField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TokenSystemCode
        {
            get
            {
                return this.TokenSystemCodeField;
            }
            set
            {
                this.TokenSystemCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TokenSystemName
        {
            get
            {
                return this.TokenSystemNameField;
            }
            set
            {
                this.TokenSystemNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TokenUserLogin
        {
            get
            {
                return this.TokenUserLoginField;
            }
            set
            {
                this.TokenUserLoginField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://btgpactual.com/SecureGateway", ConfigurationName = "SecureGatewayService.ISecGtwNoCertContract")]
    public interface ISecGtwNoCertContract
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/AuthenticateByKey", ReplyAction = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/AuthenticateByKeyRespon" +
            "se")]
        System.Threading.Tasks.Task<SecureGatewayService.AuthRespDataContract> AuthenticateByKeyAsync(string key);

        [System.ServiceModel.OperationContractAttribute(Action = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/AuthenticateUser", ReplyAction = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/AuthenticateUserRespons" +
            "e")]
        System.Threading.Tasks.Task<SecureGatewayService.AuthUserRespDataContract> AuthenticateUserAsync(string key, string login, string password);

        [System.ServiceModel.OperationContractAttribute(Action = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/AuthenticateUserByToken" +
            "", ReplyAction = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/AuthenticateUserByToken" +
            "Response")]
        System.Threading.Tasks.Task<SecureGatewayService.AuthUserRespDataContract> AuthenticateUserByTokenAsync(string key, string token);

        [System.ServiceModel.OperationContractAttribute(Action = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/AuthenticateComponent", ReplyAction = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/AuthenticateComponentRe" +
            "sponse")]
        System.Threading.Tasks.Task<SecureGatewayService.AuthComponentRespDataContract> AuthenticateComponentAsync(string key, string token);

        [System.ServiceModel.OperationContractAttribute(Action = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/ValidadeToken", ReplyAction = "http://btgpactual.com/SecureGateway/ISecGtwNoCertContract/ValidadeTokenResponse")]
        System.Threading.Tasks.Task<bool> ValidadeTokenAsync(string token);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface ISecGtwNoCertContractChannel : SecureGatewayService.ISecGtwNoCertContract, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class SecGtwNoCertContractClient : System.ServiceModel.ClientBase<SecureGatewayService.ISecGtwNoCertContract>, SecureGatewayService.ISecGtwNoCertContract
    {

        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);

        public SecGtwNoCertContractClient(IConfiguration endpointConfiguration) :
                base(SecGtwNoCertContractClient.GetBindingForEndpoint(endpointConfiguration), SecGtwNoCertContractClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public SecGtwNoCertContractClient(IConfiguration endpointConfiguration, string remoteAddress) :
                base(SecGtwNoCertContractClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public SecGtwNoCertContractClient(IConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) :
                base(SecGtwNoCertContractClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public SecGtwNoCertContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public System.Threading.Tasks.Task<SecureGatewayService.AuthRespDataContract> AuthenticateByKeyAsync(string key)
        {
            return base.Channel.AuthenticateByKeyAsync(key);
        }

        public System.Threading.Tasks.Task<SecureGatewayService.AuthUserRespDataContract> AuthenticateUserAsync(string key, string login, string password)
        {
            return base.Channel.AuthenticateUserAsync(key, login, password);
        }

        public System.Threading.Tasks.Task<SecureGatewayService.AuthUserRespDataContract> AuthenticateUserByTokenAsync(string key, string token)
        {
            return base.Channel.AuthenticateUserByTokenAsync(key, token);
        }

        public System.Threading.Tasks.Task<SecureGatewayService.AuthComponentRespDataContract> AuthenticateComponentAsync(string key, string token)
        {
            return base.Channel.AuthenticateComponentAsync(key, token);
        }

        public System.Threading.Tasks.Task<bool> ValidadeTokenAsync(string token)
        {
            return base.Channel.ValidadeTokenAsync(token);
        }

        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }

        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }

        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(IConfiguration endpointConfiguration)
        {
            System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.AllowCookies = true;
            return result;
        }

        private static System.ServiceModel.EndpointAddress GetEndpointAddress(IConfiguration endpointConfiguration)
        {
            var url = "";
            try
            {
                url = endpointConfiguration.GetSection("SecureGateway")["ServiceUrl"].ToString();
            }
            catch
            {
                throw new KeyNotFoundException("ServiceUrl for SecureGateway not found in appSettings: SecureGateway.ServiceUrl");
            }
            return new System.ServiceModel.EndpointAddress(endpointConfiguration.GetSection("SecureGateway")["ServiceUrl"].ToString());
        }

        public enum EndpointConfiguration
        {

            CustomBinding_ISecGtwNoCertContract
        }
    }
}
