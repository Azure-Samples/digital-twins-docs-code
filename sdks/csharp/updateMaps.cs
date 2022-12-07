using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Azure.Messaging.EventGrid;

namespace updateMaps
{
    public static class ProcessDTUpdatetoMaps
    {
        // Read maps credentials from application settings on function startup
        private static string statesetID = Environment.GetEnvironmentVariable("statesetID");
        private static string subscriptionKey = Environment.GetEnvironmentVariable("subscription-key");
        private static HttpClient httpClient = new HttpClient();

        [FunctionName("ProcessDTUpdatetoMaps")]
        public static async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            JObject message = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
            log.LogInformation($"Reading event from twinID: {eventGridEvent.Subject}: {eventGridEvent.EventType}: {message["data"]}");

            //Parse updates to "space" twins
            if (message["data"]["modelId"].ToString() == "dtmi:contosocom:DigitalTwins:Space;1")
            {
                // Set the ID of the room to be updated in your map.
                // Replace this line with your logic for retrieving featureID.
                string featureID = "UNIT103";

                // Iterate through the properties that have changed
                foreach (var operation in message["data"]["patch"])
                {
                    if (operation["op"].ToString() == "replace" && operation["path"].ToString() == "/Temperature")
                    {
                        // Update the maps feature stateset
                        var postcontent = new JObject(
                            new JProperty(
                                "States",
                                new JArray(
                                    new JObject(
                                        new JProperty("keyName", "temperature"),
                                        new JProperty("value", operation["value"].ToString()),
                                        new JProperty("eventTimestamp", DateTime.UtcNow.ToString("s"))))));

                        var response = await httpClient.PutAsync(
                            $"https://us.atlas.microsoft.com/featurestatesets/{statesetID}/featureStates/{featureID}?api-version=2.0&subscription-key={subscriptionKey}",
                            new StringContent(postcontent.ToString()));


                        log.LogInformation(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }
    }
}
