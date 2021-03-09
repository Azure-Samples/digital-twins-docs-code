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

// Initialize the twin properties
var myTwin = new MyCustomDigitalTwin
{
    Metadata = { ModelId = "dtmi:example:Room;1" },
    Temperature = 25.0,
    Humidity = 50.0,
};
// Create the twin
Response<BasicDigitalTwin> response = await client.CreateOrReplaceDigitalTwinAsync(twinId, myTwin);
Console.WriteLine($"Temperature last updated on {response.Value.Metadata.Temperature.LastUpdatedOn}")
// </CreateTwin_noHelper>

// ------------------ CREATE TWIN: Error handling---------------------
// <CreateTwin_errorHandling>
try
{
    await client.CreateOrReplaceDigitalTwinAsync<MyCustomDigitalTwin>(id, myTwin);
    Console.WriteLine($"Created a twin successfully: {id}");
}
catch (ErrorResponseException e)
{
    Console.WriteLine($"*** Error creating twin {id}: {e.Response.StatusCode}");
}
// </CreateTwin_errorHandling>

// ------------------ UPDATE TWIN (Longer example than in the runnable sample)---------------------
// <UpdateTwin>
var updateTwinData = new JsonPatchDocument();
updateTwinData.AppendAdd("/Temperature", 25.0);
updateTwinData.AppendAdd("/myComponent/Property", "Hello");
// Un-set a property
updateTwinData.AppendRemove("/Humidity");

await client.UpdateDigitalTwinAsync("myTwin", updateTwinData);
// </UpdateTwin>

// ------------------ SET TAG PROPERTY VALUES: MARKER ---------------------
// <TagPropertiesMarker>
entity-01: "tags": { "red": true, "round": true }
entity-02: "tags": { "blue": true, "round": true }
entity-03: "tags": { "red": true, "large": true }
// </TagPropertiesMarker>

// ------------------ SET TAG PROPERTY VALUES: VALUE ---------------------
// <TagPropertiesValue>
entity-01: "tags": { "red": "", "size": "large" }
entity-02: "tags": { "purple": "", "size": "small" }
entity-03: "tags": { "red": "", "size": "small" }
// </TagPropertiesValue>
