// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
//<Function_dependencies>
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
//</Function_dependencies>

namespace DigitalTwins_Samples
{
    public class DigitalTwinsIngestFunctionSample
    {
        // Your Digital Twin URL is stored in an application setting in Azure Functions
        // <ADT_service_URL>
        private static readonly string adtInstanceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");
        // </ADT_service_URL>
        // <HTTP_client>
        private static readonly HttpClient singletonHttpClientInstance = new HttpClient();
        // </HTTP_client>

        [FunctionName("TwinsFunction")]
        public void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.Data.ToString());
            if (adtInstanceUrl == null) log.LogError("Application setting \"ADT_SERVICE_URL\" not set");
            try
            {
                // Authenticate with Digital Twins
                // <ManagedIdentityCredential>
                // To use the function app's system-assigned identity:
                var cred = new ManagedIdentityCredential();
                // To use a user-assigned identity for the function app:
                //var cred = new ManagedIdentityCredential("<uai-client-ID>");
                // </ManagedIdentityCredential>
                // <DigitalTwinsClient>
                var client = new DigitalTwinsClient(
                    new Uri(adtInstanceUrl),
                    cred,
                    new DigitalTwinsClientOptions
                    {
                        Transport = new HttpClientTransport(singletonHttpClientInstance)
                    });
                // </DigitalTwinsClient>
                log.LogInformation($"ADT service client connection created.");

                // Add your business logic here.
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
            }
        }
    }
}
