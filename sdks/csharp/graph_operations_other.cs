// ------------------ CREATE RELATIONSHIP (Short) ---------------------
// <CreateRelationship_short>
BasicRelationship rel = new BasicRelationship();
rel.TargetId = "myTargetTwin";
rel.Name = "contains"; // a relationship with this name must be defined in the model
// Initialize properties
Dictionary<string, object> props = new Dictionary<string, object>();
props.Add("active", true);
rel.Properties = props;
client.CreateOrReplaceRelationshipAsync("mySourceTwin", "rel001", rel);
// <CreateRelationship_short>

// ------------------ CREATE RELATIONSHIP (Alternate) ---------------------
// <CreateRelationship_3>
// Create Twins, using functions similar to the previous sample
await CreateRoom("Cafe", 70, 66);
await CreateFloor("GroundFloor", averageTemperature=70);
// Create relationships
var relationship = new BasicRelationship
{
    TargetId = "Cafe",
    Name = "contains"
};
try
{
    string relId = $"GroundFloor-contains-Cafe";
    await client.CreateOrReplaceRelationshipAsync<BasicRelationship>("GroundFloor", relId, relationship);
} catch(ErrorResponseException e)
{
    Console.WriteLine($"*** Error creating relationship: {e.Response.StatusCode}");
}
// <CreateRelationship_3>

// ------------------ LIST PROPERTIES OF RELATIONSHIPS ---------------------
// <ListRelationshipProperties>
BasicRelationship res = client.GetRelationship<BasicRelationship>(twin_id, rel_id);
Console.WriteLine($"Relationship Name: {rel.Name}");
foreach (string prop in rel.Contents.Keys)
{
    if (twin.Contents.TryGetValue(prop, out object value))
        Console.WriteLine($"Property '{prop}': {value}");
}
// </ListRelationshipProperties>