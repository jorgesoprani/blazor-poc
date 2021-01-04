using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration(builder =>
                //{
                //    var root = builder.Build();
                //    var vaultName = root["KeyVault:Name"];
                //    var clientId = root["KeyVault:ClientId"];
                //    var thumbprint = root["KeyVault:Thumbprint"];
                //    var certificate = GetCertificate(thumbprint.ToLower());

                //    var secretManager = new PrefixKeyVaultSecretManager("BlazorPoC");

                //    builder.AddAzureKeyVault($"https://{vaultName}.vault.azure.net", clientId, certificate, secretManager);
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



        private static X509Certificate2 GetCertificate(string thumbprint)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                var certificateCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                if (certificateCollection.Count > 0)
                    return certificateCollection[0];

                else throw new Exception("Certificate not found");
            }
            finally
            {
                store.Close();
            }
        }
    }



    public class PrefixKeyVaultSecretManager : IKeyVaultSecretManager
    {
        private readonly string _prefix;
        public PrefixKeyVaultSecretManager(string prefix)
        {
            _prefix = $"{prefix}-";
        }

        public string GetKey(SecretBundle secret)
        {
            return secret.SecretIdentifier.Name.Substring(_prefix.Length)
                .Replace("--", ConfigurationPath.KeyDelimiter);
        }

        public bool Load(SecretItem secret)
        {
            return secret.Identifier.Name.StartsWith(_prefix);
        }
    }
}
