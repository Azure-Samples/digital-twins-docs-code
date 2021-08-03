using Azure.DigitalTwins.Core;
using System;

namespace DigitalTwins_Samples
{
    class GraphOperationsOtherSample
    {
        async public void CreateRelationshipShort(DigitalTwinsClient client)
        {
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
        }

        async public void ListRelationshipProperties(DigitalTwinsClient client, string twinId, string relId, BasicDigitalTwin twin)
        {
            // ------------------ LIST PROPERTIES OF RELATIONSHIPS ---------------------
            // <ListRelationshipProperties>
            var res = await client.GetRelationshipAsync<BasicRelationship>(twinId, relId);
            BasicRelationship rel = res.Value;
            Console.WriteLine($"Relationship Name: {rel.Name}");
            foreach (string prop in rel.Properties.Keys)
            {
                if (twin.Contents.TryGetValue(prop, out object value))
                {
                    Console.WriteLine($"Property '{prop}': {value}");
                }
            }
            // </ListRelationshipProperties>
        }
    }
}
