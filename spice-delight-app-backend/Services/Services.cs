using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;


namespace spice_delight_app_backend.Services
{
    public class SecretsManagerServices
    {

        public async Task<Dictionary<string, string>> GetSecretAsync(string secretName, string region)
        {
            using var client = new AmazonSecretsManagerClient(Amazon.RegionEndpoint.GetBySystemName(region));
            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName
            };
            GetSecretValueResponse response = await client.GetSecretValueAsync(request);

            if (response.SecretString != null)
            {
                return JsonSerializer.Deserialize<Dictionary<string, string>>(response.SecretString);
            }
            else
            {
                throw new Exception("Secrets Manager secret must be a string");
            }
        }

    }

    public class DbConnectionInfo
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool Encrypt { get; set; } = true;
        public bool TrustServerCertificate { get; set; } = true;
    }

}
