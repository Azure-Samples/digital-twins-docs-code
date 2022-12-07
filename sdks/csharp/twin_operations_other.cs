using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalTwins_Samples
{
    // ------------------ CREATE TWIN: NO HELPER ---------------------
    // <CreateTwin_noHelper>
    // Define a custom model type for the twin to be created

    internal class CustomDigitalTwin
    {
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinId)]
        public string Id { get; set; }

        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinETag)]
        public string ETag { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity{ get; set; }
    }

    // Initialize properties and create the twin
    public class TwinOperationsCreateTwin
    {
        public async Task CreateTwinAsync(DigitalTwinsClient client)
        {
            // Initialize the twin properties
            var myTwin = new CustomDigitalTwin
            {
                Temperature = 25.0,
                Humidity = 50.0,
            };

            // Create the twin
            const string twinId = "<twin-ID>";
            Response<CustomDigitalTwin> response = await client.CreateOrReplaceDigitalTwinAsync(twinId, myTwin);
        }
    }
    // </CreateTwin_noHelper>

    public class TwinOperationsOther
    { 
        public async Task UpdateTwinAsync(DigitalTwinsClient client)
        {
            // ------------------ UPDATE TWIN (Longer example than in the runnable sample)---------------------
            // <UpdateTwin>
            var updateTwinData = new JsonPatchDocument();
            updateTwinData.AppendAdd("/Temperature", 25.0);
            updateTwinData.AppendAdd("/myComponent/Property", "Hello");
            // Un-set a property
            updateTwinData.AppendRemove("/Humidity");

            await client.UpdateDigitalTwinAsync("myTwin", updateTwinData).ConfigureAwait(false);
            // </UpdateTwin>
        }

        public async Task SetPropertyValuesAsync(DigitalTwinsClient client)
        {
            // ------------------ SET TAG PROPERTY VALUES: CSHARP ---------------------
            // <TagPropertiesCsharp>
            IDictionary<string, bool> tags = new Dictionary<string, bool>
            {
                { "oceanview", true },
                { "VIP", true }
            };
            var twin = new BasicDigitalTwin
            {
                Metadata = { ModelId = "dtmi:example:Room;1" },
                Contents =
                {
                    { "Temperature", 75 },
                    { "tags", tags },
                },
            };
            await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>("myTwinID", twin);
            // </TagPropertiesCsharp>
        }
    }
}
