using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using spice_delight_app_backend.Data;


namespace spice_delight_app_backend.Services
{
    public class SecretsManagerServices
    {

        public void ConfigureServices(IServiceCollection services)
        {
            // Authenticate to Azure using managed identity
            var credential = new DefaultAzureCredential();

            // Create a secret client
            var vaultUrl = "https://spice-delight-key-vault.vault.azure.net/";
            var client = new SecretClient(new Uri(vaultUrl), credential);

            // Retrieve the secret containing your connection string
            KeyVaultSecret secret = client.GetSecret("spice-delight-sql-server");

            // Use the retrieved connection string
            var connectionString = secret.Value;

            services.AddDbContext<SpiceDbContext>(options =>
                options.UseSqlServer(connectionString));

        }
    }

}
