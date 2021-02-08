using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Azure.DigitalTwins.Core;
using Azure.Identity;

namespace creating_twin_graph_from_csv
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var relationshipRecordList = new List<BasicRelationship>();
            var twinList = new List<BasicDigitalTwin>();
            List<List<string>> data = ReadData();
            DigitalTwinsClient client = CreateDtClient();

            // Interpret the CSV file data, by each row
            foreach (List<string> row in data)
            {
                string modelID = row.Count > 0 ? row[0].Trim() : null;
                string srcID = row.Count > 1 ? row[1].Trim() : null;
                string relName = row.Count > 2 ? row[2].Trim() : null;
                string targetID = row.Count > 3 ? row[3].Trim() : null;
                string initProperties = row.Count > 4 ? row[4].Trim() : null;
                Console.WriteLine($"ModelID: {modelID}, TwinID: {srcID}, RelName: {relName}, TargetID: {targetID}, InitData: {initProperties}");
                var props = new Dictionary<string, object>();
                // Parse properties into dictionary (left out for compactness)
                // ...

                // Null check for source and target IDs
                if (!string.IsNullOrWhiteSpace(srcID) && !string.IsNullOrWhiteSpace(targetID) && !string.IsNullOrWhiteSpace(relName))
                {
                    relationshipRecordList.Add(
                        new BasicRelationship
                        {
                            SourceId = srcID,
                            TargetId = targetID,
                            Name = relName,
                        });
                }

                if (!string.IsNullOrWhiteSpace(srcID) && !string.IsNullOrWhiteSpace(modelID))
                twinList.Add(
                    new BasicDigitalTwin
                    {
                        Id = srcID,
                        Metadata = { ModelId = modelID },
                        Contents = props,
                    };
            }

            // Create digital twins
            foreach (BasicDigitalTwin twin in twinList)
            {
                try
                {
                    await client.CreateOrReplaceDigitalTwinAsync<BasicDigitalTwin>(twin.Id, twin);
                    Console.WriteLine("Twin is created");
                }
                catch (RequestFailedException ex)
                {
                    Console.WriteLine($"Error {ex.Status}: {ex.Message}");
                }
            }

            // Create relationships between the twins
            foreach (BasicRelationship rec in relationshipRecordList)
            {
                try
                {
                    string relId = $"{rec.SourceId}-{rec.Name}->{rec.TargetId}";
                    await client.CreateOrReplaceRelationshipAsync<BasicRelationship>(rec.SourceId, relId, rec);
                    Console.WriteLine($"Relationship {relId} is created");
                }
                catch (RequestFailedException ex)
                {
                    Console.WriteLine($"Error creating relationship {relId}. {ex.Status}: {ex.Message}");
                }
            }
        }

        // Method to ingest data from the CSV file
        public static List<List<string>> ReadData()
        {
            string path = "<path-to>/data.csv";
            string[] lines = System.IO.File.ReadAllLines(path);
            var data = new List<List<string>>();
            int count = 0;
            foreach (string line in lines)
            {
                if (count++ == 0)
                    continue;
                var cols = new List<string>();
                string[] columns = line.Split(',');
                foreach (string column in columns)
                {
                    cols.Add(column);
                }
                data.Add(cols);
            }
            return data;
        }

        // Method to create the digital twins client
        private static DigitalTwinsClient CreateDtClient()
        {
            string adtInstanceUrl = "https://<your-instance-hostname>";
            var credentials = new DefaultAzureCredential();
            return new DigitalTwinsClient(new Uri(adtInstanceUrl), credentials);
        }
    }
}