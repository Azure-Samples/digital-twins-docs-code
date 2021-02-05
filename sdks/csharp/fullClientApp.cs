using System;
// <Azure_Digital_Twins_dependencies>
using Azure.DigitalTwins.Core;
using Azure.Identity;
// </Azure_Digital_Twins_dependencies>
// <Model_dependencies>
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Azure;
// </Model_dependencies>
// <Query_dependencies>
using System.Text.Json;
// </Query_dependencies>

namespace minimal
{
    class Program
    {
        // <Async_signature>
        static async Task Main(string[] args)
        {
        // </Async_signature>
            Console.WriteLine("Hello World!");
            // <Authentication_code>
            string adtInstanceUrl = "https://<your-Azure-Digital-Twins-instance-hostName>"; 
            
            var credential = new DefaultAzureCredential();
            var client = new DigitalTwinsClient(new Uri(adtInstanceUrl), credential);
            Console.WriteLine($"Service client created – ready to go");
            // </Authentication_code>

            // <Model_code>
            Console.WriteLine();
            Console.WriteLine("Upload a model");
            string dtdl = File.ReadAllText("SampleModel.json");
            var models = new List<string> { dtdl };

            // Upload the model to the service
            // <Model_try_catch>
            try
            {
                await client.CreateModelsAsync(models);
                Console.WriteLine("Models uploaded to the instance:");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Upload model error: {e.Status}: {e.Message}");
            }
            // </Model_try_catch>

            // <Print_model>
            // Read a list of models back from the service
            AsyncPageable<DigitalTwinsModelData> modelDataList = client.GetModelsAsync();
            await foreach (DigitalTwinsModelData md in modelDataList)
            {
                Console.WriteLine($"Model: {md.Id}");
            }
            // </Print_model>
            // </Model_code>

            // <Initialize_twins>
            var twinData = new BasicDigitalTwin();
            twinData.Metadata.ModelId = "dtmi:example:SampleModel;1";
            twinData.Contents.Add("data", $"Hello World!");
            
            string prefix = "sampleTwin-";
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    twinData.Id = $"{prefix}{i}";
                    await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(twinData.Id, twinData);
                    Console.WriteLine($"Created twin: {twinData.Id}");
                }
                catch(RequestFailedException e)
                {
                    Console.WriteLine($"Create twin error: {e.Status}: {e.Message}");
                }
            }
            // </Initialize_twins>

            // <Use_create_relationship>
            // Connect the twins with relationships
            await CreateRelationshipAsync(client, "sampleTwin-0", "sampleTwin-1");
            await CreateRelationshipAsync(client, "sampleTwin-0", "sampleTwin-2");
            // </Use_create_relationship>

            // <Use_list_relationships>
            //List the relationships
            await ListRelationshipsAsync(client, "sampleTwin-0");
            // </Use_list_relationships>

            // <Query_twins>
            // Run a query for all twins
            string query = "SELECT * FROM digitaltwins";
            AsyncPageable<BasicDigitalTwin> queryResult = client.QueryAsync<BasicDigitalTwin>(query);
            
            await foreach (BasicDigitalTwin twin in queryResult)
            {
                Console.WriteLine(JsonSerializer.Serialize(twin));
                Console.WriteLine("---------------");
            }
            // </Query_twins>
        }

        // <Create_relationship>
        public async static Task CreateRelationshipAsync(DigitalTwinsClient client, string srcId, string targetId)
        {
            var relationship = new BasicRelationship
            {
                TargetId = targetId,
                Name = "contains"
            };
        
            try
            {
                string relId = $"{srcId}-contains->{targetId}";
                await client.CreateOrReplaceRelationshipAsync(srcId, relId, relationship);
                Console.WriteLine("Created relationship successfully");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Create relationship error: {e.Status}: {e.Message}");
            }
        }
        // </Create_relationship>
        
        // <List_relationships>
        public async static Task ListRelationshipsAsync(DigitalTwinsClient client, string srcId)
        {
            try
            {
                AsyncPageable<BasicRelationship> results = client.GetRelationshipsAsync<BasicRelationship>(srcId);
                Console.WriteLine($"Twin {srcId} is connected to:");
                await foreach (BasicRelationship rel in results)
                {
                    Console.WriteLine($" -{rel.Name}->{rel.TargetId}");
                }
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Relationship retrieval error: {e.Status}: {e.Message}");
            }
        }
        // </List_relationships>
    }
}