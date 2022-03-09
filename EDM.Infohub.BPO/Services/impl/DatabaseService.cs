using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.IO;

namespace EDM.Infohub.BPO.Services.impl
{
    public class DatabaseService : IDataBaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private IConfiguration _configuration;
        private readonly IAmazonSecretsManager _secretManager;

        private NpgsqlConnection Conexao { get; set; }
        public DatabaseService(IConfiguration configuration, IAmazonSecretsManager secretsManager, ILogger<DatabaseService> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _secretManager = secretsManager;
            //StartService();
            //GetSecret();
        }

        private void StartService()
        {
            Conexao = new NpgsqlConnection(GetSecret("Base"));
            try
            {
                Conexao.Open();
            }
            catch (Exception e)
            {
                _logger.LogError("Erro na abertura de conexão", e);
            }

            Conexao.TypeMapper.UseJsonNet();
        }

        public string GetSecret(string connectionName)
        {
            string connectionString = _configuration.GetConnectionString(connectionName);

            string secretName = _configuration["SecretName"];

            string secret = "";

            MemoryStream memoryStream = new MemoryStream();

            //IAmazonSecretsManager client = new AmazonSecretsManagerClient();

            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = secretName;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

            GetSecretValueResponse response = null;

            // In this sample we only handle the specific exceptions for the 'GetSecretValue' API.
            // See https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
            // We rethrow the exception by default.

            try
            {
                response = _secretManager.GetSecretValueAsync(request).Result;
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                // Deal with the exception here, and/or rethrow at your discretion
                throw;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (ResourceNotFoundException e)
            {
                // We can't find the resource that you asked for.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }
            catch (System.AggregateException e)
            {
                // More than one of the above exceptions were triggered.
                // Deal with the exception here, and/or rethrow at your discretion.
                throw;
            }

            // Decrypts secret using the associated KMS CMK.
            // Depending on whether the secret is a string or binary, one of these fields will be populated.
            if (response.SecretString != null)
            {
                secret = response.SecretString;
            }
            else
            {
                memoryStream = response.SecretBinary;
                StreamReader reader = new StreamReader(memoryStream);
                string decodedBinarySecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            }

            var secretObj = JsonConvert.DeserializeObject<Secret>(secret);
            connectionString.Replace("Secret", secretObj.Password);

            return connectionString.Replace("Secret", secretObj.Password); ;
            // Your code goes here.
        }

        public string InsertMessage(ControleMensagem controleMensagem)
        {
            throw new NotImplementedException();
        }

        //    public string InsertMessage(ControleMensagem controleMensagem)
        //    {
        //        try
        //        {
        //            using (var cmd = new NpgsqlCommand("INSERT INTO edm.TB_CONTROLE_MENSAGEM VALUES (DEFAULT, (@data), (@tipoPapel), (@IdLuz), (@mensagem), (@status))", Conexao))
        //            {
        //                cmd.Parameters.AddWithValue("data", controleMensagem.DateTime);
        //                cmd.Parameters.AddWithValue("idLuz", controleMensagem.IdLuz);
        //                cmd.Parameters.AddWithValue("status", (int) controleMensagem.StatusProcessamento);
        //                cmd.Parameters.AddWithValue("tipoPapel", controleMensagem.TipoPapel);
        //                cmd.Parameters.Add(new NpgsqlParameter("mensagem", NpgsqlDbType.Jsonb) { Value = controleMensagem.Mensagem });
        //                cmd.ExecuteNonQuery();
        //            }
        //        }catch (Exception e)
        //        {
        //            _logger.LogError(e,"Erro ao inserir na tabela: " + e.Message);
        //            return e.Message;
        //        }

        //        return "OK";
        //    }
    }

    internal class Secret
    {
        [JsonProperty(PropertyName = "username")]
        private string Username { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public string GetPassword()
        {
            return Password;
        }
    }
}