// ------------------ CREATE TWIN: NO HELPER ---------------------
// <CreateTwin_noHelper>
// Define the model type for the twin to be created
Dictionary<string, object> meta = new Dictionary<string, object>()
{
    { "$model", "dtmi:example:Room;1" }
};
// Initialize the twin properties
Dictionary<string, object> initData = new Dictionary<string, object>()
{
    { "$metadata", meta },
    { "Temperature", 25.0},
    { "Humidity", 50.0},
};
//Create the twin
client.CreateOrReplaceDigitalTwinAsync("myTwinID", JsonSerializer.Serialize<Dictionary<string, object>>(initData));
// </CreateTwin_noHelper>

// ------------------ UPDATE TWIN (Longer example than in the runnable sample below)---------------------
// <UpdateTwin>
var updateTwinData = new JsonPatchDocument();
updateTwinData.AppendAddOp("/Temperature", 25.0);
updateTwinData.AppendAddOp("/myComponent/Property", "Hello");
// Un-set a property
updateTwinData.AppendRemoveOp("/Humidity");

client.UpdateDigitalTwin("myTwin", updateTwinData);
// </UpdateTwin>