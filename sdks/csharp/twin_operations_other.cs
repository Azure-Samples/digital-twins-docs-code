// ------------------ CREATE TWIN: NO HELPER ---------------------
// <CreateTwin_noHelper>
// Define the model type for the twin to be created
using System.Net.Mime;

// Initialize the twin properties
var twinInit = new BasicDigitalTwin
{
    Metadata = { ModelId = "dtmi:example:Room;1" },
    Contents =
    {
        { "Temperature", 25.0},
        { "Humidity", 50.0},
    },
};
//Create the twin
client.CreateOrReplaceDigitalTwinAsync(twinID, twinInit);
// </CreateTwin_noHelper>

// ------------------ CREATE TWIN: Error handling---------------------
// <CreateTwin_errorHandling>
try
{
    await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(id, twinInit);
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
