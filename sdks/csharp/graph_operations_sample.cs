using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Azure;
using Azure.DigitalTwins.Core;
using Azure.Identity;

namespace minimal
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Create the Azure Digital Twins client for API calls
            DigitalTwinsClient client = createDtClient();
            Console.WriteLine($"Service client created – ready to go");
            Console.WriteLine();

            // Upload models
            Console.WriteLine($"Upload models");
            Console.WriteLine();
            string dtdl = File.ReadAllText("<path-to>/Room.json");
            string dtdl1 = File.ReadAllText("<path-to>/Floor.json");
            var models = new List<string>
            {
                dtdl,
                dtdl1,
            };
            // Upload the models to the service
            await client.CreateModelsAsync(models);

            // Create new (Floor) digital twin
            var floorTwin = new BasicDigitalTwin();
            string srcId = "myFloorID";
            floorTwin.Metadata.ModelId = "dtmi:example:Floor;1";
            // Floor twins have no properties, so nothing to initialize
            // Create the twin
            await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(srcId, floorTwin);
            Console.WriteLine("Twin created successfully");

            // Create second (Room) digital twin
            var roomTwin = new BasicDigitalTwin();
            string targetId = "myRoomID";
            roomTwin.Metadata.ModelId = "dtmi:example:Room;1";
            // Initialize properties
            roomTwin.Contents.Add("Temperature", 35.0);
            roomTwin.Contents.Add("Humidity", 55.0);
            // Create the twin
            await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(targetId, roomTwin);
            
            // Create relationship between them
            // <UseCreateRelationship>
            await CustomMethod_CreateRelationshipAsync(client, srcId, targetId, "contains");
            // </UseCreateRelationship>
            Console.WriteLine();

            //Print twins and their relationships
            Console.WriteLine("--- Printing details:");
            Console.WriteLine("Outgoing relationships from source twin:");
            // <UseFetchAndPrint>
            await CustomMethod_FetchAndPrintTwinAsync(srcId, client);
            // </UseFetchAndPrint>
            Console.WriteLine();
            Console.WriteLine("Incoming relationships to target twin:");
            await CustomMethod_FetchAndPrintTwinAsync(targetId, client);
            Console.WriteLine("--------");
            Console.WriteLine();

            // Delete the relationship
            Console.WriteLine("Deleting the relationship");
            // <UseDeleteRelationship>
            await CustomMethod_DeleteRelationshipAsync(client, srcId, $"{srcId}-contains->{targetId}");
            // </UseDeleteRelationship>
            Console.WriteLine();

            // Print twins and their relationships again
            Console.WriteLine("--- Printing details:");
            Console.WriteLine("Outgoing relationships from source twin:");
            await CustomMethod_FetchAndPrintTwinAsync(srcId, client);
            Console.WriteLine();
            Console.WriteLine("Incoming relationships to target twin:");
            await CustomMethod_FetchAndPrintTwinAsync(targetId, client);
            Console.WriteLine("--------");
            Console.WriteLine();
        }

        private static DigitalTwinsClient createDtClient()
        {
            string adtInstanceUrl = "https://<your-instance-hostname>";
            var credentials = new DefaultAzureCredential();
            var client = new DigitalTwinsClient(new Uri(adtInstanceUrl), credentials);
            return client;
        }

        // <CreateRelationshipMethod>
        private async static Task CustomMethod_CreateRelationshipAsync(DigitalTwinsClient client, string srcId, string targetId, string relName)
        {
            var relationship = new BasicRelationship
            {
                TargetId = targetId,
                Name = relName
            };

            try
            {
                string relId = $"{srcId}-{relName}->{targetId}";
                await client.CreateOrReplaceRelationshipAsync<BasicRelationship>(srcId, relId, relationship);
                Console.WriteLine($"Created {relName} relationship successfully");
            }
            catch (RequestFailedException rex)
            {
                Console.WriteLine($"Create relationship error: {rex.Status}:{rex.Message}");
            }

        }
        // </CreateRelationshipMethod>

        // <FetchAndPrintMethod>
        private static async Task CustomMethod_FetchAndPrintTwinAsync(string twin_Id, DigitalTwinsClient client)
        {
            Response<BasicDigitalTwin> res = await client.GetDigitalTwinAsync<BasicDigitalTwin>(twin_Id);
            // <UseFindOutgoingRelationships>
            await CustomMethod_FindOutgoingRelationshipsAsync(client, twin_Id);
            // </UseFindOutgoingRelationships>
            // <UseFindIncomingRelationships>
            await CustomMethod_FindIncomingRelationshipsAsync(client, twin_Id);
            // </UseFindIncomingRelationships>

            return;
        }
        // </FetchAndPrintMethod>

        // <FindOutgoingRelationshipsMethod>
        private static async Task<List<BasicRelationship>> CustomMethod_FindOutgoingRelationshipsAsync(DigitalTwinsClient client, string dtId)
        {
            // Find the relationships for the twin
            
            try
            {
                // GetRelationshipsAsync will throw if an error occurs
                // <GetRelationshipsCall>
                AsyncPageable<BasicRelationship> rels = client.GetRelationshipsAsync<BasicRelationship>(dtId);
                // </GetRelationshipsCall>
                var results = new List<BasicRelationship>();
                await foreach (BasicRelationship rel in rels)
                {
                    results.Add(rel);
                    Console.WriteLine($"Found relationship-{rel.Name}->{rel.TargetId}");
                }

                return results;
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"*** Error {ex.Status}/{ex.ErrorCode} retrieving relationships for {dtId} due to {ex.Message}");
                return null;
            }
        }
        // </FindOutgoingRelationshipsMethod>

        // <FindIncomingRelationshipsMethod>
        private static async Task<List<IncomingRelationship>> CustomMethod_FindIncomingRelationshipsAsync(DigitalTwinsClient client, string dtId)
        {
            // Find the relationships for the twin
            
            try
            {
                // GetRelationshipsAsync will throw an error if a problem occurs
                AsyncPageable<IncomingRelationship> incomingRels = client.GetIncomingRelationshipsAsync(dtId);

                var results = new List<IncomingRelationship>();
                await foreach (IncomingRelationship incomingRel in incomingRels)
                {
                    results.Add(incomingRel);
                    Console.WriteLine($"Found incoming relationship-{incomingRel.RelationshipId}");
                }
                return results;
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"*** Error {ex.Status}/{ex.ErrorCode} retrieving incoming relationships for {dtId} due to {ex.Message}");
                return null;
            }
        }
        // </FindIncomingRelationshipsMethod>

        // <DeleteRelationshipMethod>
        private static async Task CustomMethod_DeleteRelationshipAsync(DigitalTwinsClient client, string srcId, string relId)
        {
            try
            {
                Response response = await client.DeleteRelationshipAsync(srcId, relId);
                await CustomMethod_FetchAndPrintTwinAsync(srcId, client);
                Console.WriteLine("Deleted relationship successfully");
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine($"Error {e.ErrorCode}");
            }
        }
        // </DeleteRelationshipMethod>
    }
}