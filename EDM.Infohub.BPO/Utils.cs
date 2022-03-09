using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using EDM.Infohub.BPO.Services.impl;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EDM.Infohub.BPO
{
    public static class Utils
    {
        //public static string GetSecret(IConfiguration _configuration, IAmazonSecretsManager _secretsManager, string connectionName)
        //{
        //    return _configuration.GetSection("connectionStrings")[connectionName];
        //}

        public static string GetSecret(IConfiguration _configuration, IAmazonSecretsManager _secretsManager, string connectionName)
        {
            string connectionString = _configuration.GetSection("connectionStrings")[connectionName];

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
                response = _secretsManager.GetSecretValueAsync(request).Result;
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

            return connectionString.Replace("Secret", secretObj.Password);
            // Your code goes here.
        }

        public static byte[] GenerateHash(string message)
        {
            var md5 = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(message.Trim().ToUpper());
            return md5.ComputeHash(bytes);
        }

        public static byte[] GenerateByteArrayId()
        {
            Random rnd = new Random();
            byte[] b = new byte[10];
            rnd.NextBytes(b);
            return b;

        }

        public static Int64 GenerateRank(DateTime dateTime)
        {
            //return Int64.Parse(dateTime.ToString("yyyyMMddHHmmss"));
            return Int64.Parse(dateTime.ToString("yyyyMMddHHmmss"));
        }

        public static TimeSpan FilterTimeParam(string timeParam)
        {
            DateTime dTime;
            if (!DateTime.TryParseExact(timeParam, "HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dTime))
            {
                return TimeSpan.Parse("00:00");
            }
            else
            {
                return dTime.TimeOfDay;
            }
        }
        public static DateTime FilterDateParam(DateTime data)
        {
            if (data.Equals(new DateTime(0001, 01, 01)))
            {
                return DateTime.Now;
            }
            else
            {
                return data;
            }
        }
    }
}
