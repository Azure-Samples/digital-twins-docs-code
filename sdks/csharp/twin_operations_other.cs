using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Text.Json.Serialization;


namespace DigitalTwins_Samples
{
    // ------------------ CREATE TWIN: NO HELPER ---------------------
    // <CreateTwin_noHelper>
    // Define a custom model type for the twin to be created

    class CustomDigitalTwin
    {
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinId)]
        public string Id { get; set; }

        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinETag)]
        public string ETag { get; set; }

        [JsonPropertyName(DigitalTwinsJsonPropertyNames.DigitalTwinMetadata)]
        public MyCustomDigitalTwinMetadata Metadata { get; set; } = new MyCustomDigitalTwinMetadata();

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity{ get; set; }
    }

    class MyCustomDigitalTwinMetadata
    {
        [JsonPropertyName(DigitalTwinsJsonPropertyNames.MetadataModel)]
        public string ModelId { get; set; }

        [JsonPropertyName("temperature")]
        public DigitalTwinPropertyMetadata Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public DigitalTwinPropertyMetadata Humidity { get; set; }
    }

    class TwinOperationSample
    {
        public async void Run(DigitalTwinsClient client)
        {
            // Initialize the twin properties
            var myTwin = new CustomDigitalTwin
            {
                Metadata = { ModelId = "dtmi:example:Room;1" },
                Temperature = 25.0,
                Humidity = 50.0,
            };
            // Create the twin
            const string twinId = "<twin-ID>";
            Response<BasicDigitalTwin> response = await client.CreateOrReplaceDigitalTwinAsync(twinId, myTwin);
            Console.WriteLine($"Temperature last updated on {response.Value.Metadata.Temperature.LastUpdatedOn}");
            // </CreateTwin_noHelper>
        }
        public async void UpdateTwin(DigitalTwinsClient client)
        {
            // ------------------ UPDATE TWIN (Longer example than in the runnable sample)---------------------
            // <UpdateTwin>
            var updateTwinData = new JsonPatchDocument();
            updateTwinData.AppendAdd("/Temperature", 25.0);
            updateTwinData.AppendAdd("/myComponent/Property", "Hello");
            // Un-set a property
            updateTwinData.AppendRemove("/Humidity");

            await client.UpdateDigitalTwinAsync("myTwin", updateTwinData);
            // </UpdateTwin>
        }
    }


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
