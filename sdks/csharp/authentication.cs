// <Azure_Digital_Twins_dependencies>
using Azure.DigitalTwins.Core;
using Azure.Identity;
using System;
// </Azure_Digital_Twins_dependencies>

namespace DigitalTwins_Samples
{
    // ------------------ DefaultAzureCredential with try/catch (Full) ------------------ 
    // <DefaultAzureCredential_full>
    public class DefaultAzureCredentialSample
    {
        // The URL of your instance, starting with the protocol (https://)
        private const string adtInstanceUrl = "https://<your-Azure-Digital-Twins-instance-URL>";

        internal void RunSample()
        {
            //...

            DigitalTwinsClient client;
            try
            {
                var credential = new DefaultAzureCredential();
                client = new DigitalTwinsClient(new Uri(adtInstanceUrl), credential);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Authentication or client creation error: {e.Message}");
                Environment.Exit(0);
            }
        }
    }
    // </DefaultAzureCredential_full>

    // ------------------ DefaultAzureCredential with options  ------------------ 
    // <DefaultAzureCredential_options>
    public class DefaultAzureCredentialOptionsSample
    {
        // The URL of your instance, starting with the protocol (https://)
        private const string adtInstanceUrl = "https://<your-Azure-Digital-Twins-instance-URL>";

        private static DefaultAzureCredentialOptions credentialOptions = new DefaultAzureCredentialOptions()
        {
            ExcludeSharedTokenCacheCredential = true,
            ExcludeVisualStudioCodeCredential = true,
            TenantId = "<your-Azure-Active-Directory-tenant-ID"
        };

        private static DefaultAzureCredential credential = new DefaultAzureCredential(credentialOptions);

        DigitalTwinsClient client = new DigitalTwinsClient(new Uri(adtInstanceUrl), credential);
    }
    // </DefaultAzureCredential_options>

    // ------------------ ManagedIdentityCredential ------------------
    // <ManagedIdentityCredential>
    public class ManagedIdentityCredentialSample
    {
        // The URL of your instance, starting with the protocol (https://)
        private const string adtInstanceUrl = "https://<your-Azure-Digital-Twins-instance-URL>";
        // Your client / app registration ID
        private const string functionAppId = "<client-ID>";

        internal void RunSample()
        {
            DigitalTwinsClient client;
            try
            {
                ManagedIdentityCredential cred = new ManagedIdentityCredential(functionAppId);
                client = new DigitalTwinsClient(new Uri(adtInstanceUrl), cred);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Authentication or client creation error: {e.Message}");
                Environment.Exit(0);
            }
        }
    }
    // </ManagedIdentityCredential>

    // ------------------ InteractiveBrowserCredential ------------------
    // <InteractiveBrowserCredential>
    public class InteractiveBrowserCredentialSample
    {
        // Your client / app registration ID
        private const string clientId = "<your-client-ID>";
        // Your tenant / directory ID
        private const string tenantId = "<your-tenant-ID>";
        // The URL of your instance, starting with the protocol (https://)
        private const string adtInstanceUrl = "https://<your-Azure-Digital-Twins-instance-URL>";

        internal void RunSample()
        {
            //...

            DigitalTwinsClient client;
            try
            {
                var credential = new InteractiveBrowserCredential(tenantId, clientId);
                client = new DigitalTwinsClient(new Uri(adtInstanceUrl), credential);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Authentication or client creation error: {e.Message}");
                Environment.Exit(0);
            }
        }
    }
    // </InteractiveBrowserCredential>
}
