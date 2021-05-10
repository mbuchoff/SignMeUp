using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace SafemarkGoAdminTool
{
    public class AzureKeyVaultService
    {
        public AzureKeyVaultService(IConfiguration configuration)
        {
            var client = new SecretClient(vaultUri: new Uri(uriString: configuration["KeyVaultUrl"]), new DefaultAzureCredential(options: new DefaultAzureCredentialOptions
            {
                ExcludeSharedTokenCacheCredential = true,
                ExcludeVisualStudioCredential = true
            }));
            
            Task<string> FindSecret(SecretClient client, string secretName) => Task.Run(() => client.GetSecretAsync(secretName).Result.Value.Value);

            DbConnectionString = FindSecret(client, "DbConnectionString");
        }

        public Task<string> DbConnectionString { get; private set; }
    }
}