// ------------------ CREATE RELATIONSHIP (Short) ---------------------
// <CreateRelationship_short>
var rel = new BasicRelationship
{
    TargetId = "myTargetTwin",
    Name = "contains", // a relationship with this name must be defined in the model
    // Initialize properties
    Properties =
    {
        { "active", true },
    },
};
await client.CreateOrReplaceRelationshipAsync("mySourceTwin", "rel001", rel);
// </CreateRelationship_short>

// ------------------ LIST PROPERTIES OF RELATIONSHIPS ---------------------
// <ListRelationshipProperties>
var res = await client.GetRelationshipAsync<BasicRelationship>(twinId, relId);
Console.WriteLine($"Relationship Name: {rel.Name}");
foreach (string prop in rel.Contents.Keys)
{
    if (twin.Contents.TryGetValue(prop, out object value))
    {
        Console.WriteLine($"Property '{prop}': {value}");
    }
}
// </ListRelationshipProperties>