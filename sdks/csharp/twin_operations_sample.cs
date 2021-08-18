using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Azure;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using System.IO;

namespace DigitalTwins_Samples
{
    class TwinOperationsSample
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Create the Azure Digital Twins client for API calls
            string adtInstanceUrl = "https://<your-instance-hostname>";
            var credentials = new DefaultAzureCredential();
            var client = new DigitalTwinsClient(new Uri(adtInstanceUrl), credentials);
            Console.WriteLine($"Service client created – ready to go");

            // Upload models
            Console.WriteLine($"Upload a model");
            string dtdl = File.ReadAllText("<path-to>/Room.json");
            var models = new List<string> { dtdl };
            // Upload the model to the service
            await client.CreateModelsAsync(models);

            // Create new digital twin
            // <CreateTwin_withHelper>
            string twinId = "myTwinID";
            var initData = new BasicDigitalTwin
            {
                Id = twinId,
                Metadata = { ModelId = "dtmi:example:Room;1" },
                // Initialize properties
                Contents =
                {
                    { "Temperature", 25.0 },
                    { "Humidity", 50.0 },
                },
            };

            // <CreateTwinCall>
            await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(twinId, initData);
            // </CreateTwinCall>
            // </CreateTwin_withHelper>
            Console.WriteLine("Twin created successfully");

            //Print twin
            Console.WriteLine("--- Printing twin details:");
            await CustomMethod_FetchAndPrintTwinAsync(twinId, client);
            Console.WriteLine("--------");

            //Update twin data
            var updateTwinData = new JsonPatchDocument();
            updateTwinData.AppendAdd("/Temperature", 30.0);
            // <UpdateTwinCall>
            await client.UpdateDigitalTwinAsync(twinId, updateTwinData);
            // </UpdateTwinCall>
            Console.WriteLine("Twin properties updated");
            Console.WriteLine();

            //Print twin again
            Console.WriteLine("--- Printing twin details (after update):");
            await CustomMethod_FetchAndPrintTwinAsync(twinId, client);
            Console.WriteLine("--------");
            Console.WriteLine();

            //Delete twin
            await CustomMethod_DeleteTwinAsync(client, twinId);
        }

        private static async Task<BasicDigitalTwin> CustomMethod_FetchAndPrintTwinAsync(string twinId, DigitalTwinsClient client)
        {
            // <GetTwin>
            BasicDigitalTwin twin;
            // <GetTwinCall>
            Response<BasicDigitalTwin> twinResponse = await client.GetDigitalTwinAsync<BasicDigitalTwin>(twinId);
            twin = twinResponse.Value;
            // </GetTwinCall>
            Console.WriteLine($"Model id: {twin.Metadata.ModelId}");
            foreach (string prop in twin.Contents.Keys)
            {
                if (twin.Contents.TryGetValue(prop, out object value))
                    Console.WriteLine($"Property '{prop}': {value}");
            }
            // </GetTwin>

            return twin;
        }

        // <DeleteTwin>
        private static async Task CustomMethod_DeleteTwinAsync(DigitalTwinsClient client, string twinId)
        {
            await CustomMethod_FindAndDeleteOutgoingRelationshipsAsync(client, twinId);
            await CustomMethod_FindAndDeleteIncomingRelationshipsAsync(client, twinId);
            try
            {
                await client.DeleteDigitalTwinAsync(twinId);
                Console.WriteLine("Twin deleted successfully");
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"*** Error:{ex.Message}");
            }
        }

        private static async Task CustomMethod_FindAndDeleteOutgoingRelationshipsAsync(DigitalTwinsClient client, string dtId)
        {
            // Find the relationships for the twin

            try
            {
                // GetRelationshipsAsync will throw an error if a problem occurs
                AsyncPageable<BasicRelationship> rels = client.GetRelationshipsAsync<BasicRelationship>(dtId);

                await foreach (BasicRelationship rel in rels)
                {
                    await client.DeleteRelationshipAsync(dtId, rel.Id).ConfigureAwait(false);
                    Console.WriteLine($"Deleted relationship {rel.Id} from {dtId}");
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"*** Error {ex.Status}/{ex.ErrorCode} retrieving or deleting relationships for {dtId} due to {ex.Message}");
            }
        }

        private static async Task CustomMethod_FindAndDeleteIncomingRelationshipsAsync(DigitalTwinsClient client, string dtId)
        {
            // Find the relationships for the twin

            try
            {
                // GetRelationshipsAsync will throw an error if a problem occurs
                AsyncPageable<IncomingRelationship> incomingRels = client.GetIncomingRelationshipsAsync(dtId);

                await foreach (IncomingRelationship incomingRel in incomingRels)
                {
                    await client.DeleteRelationshipAsync(incomingRel.SourceId, incomingRel.RelationshipId).ConfigureAwait(false);
                    Console.WriteLine($"Deleted incoming relationship {incomingRel.RelationshipId} from {dtId}");
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"*** Error {ex.Status}/{ex.ErrorCode} retrieving or deleting incoming relationships for {dtId} due to {ex.Message}");
            }
        }
        // </DeleteTwin>

    }
}