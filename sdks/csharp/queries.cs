using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;

namespace DigitalTwins_Samples
{
    class QueriesSample
    {
        async public void QueryBasic(DigitalTwinsClient client)
        {
            // ------------------ RUN QUERY (Basic) ---------------------
            // <RunQuery>
            // Run a query for all twins   
            string query = "SELECT * FROM DIGITALTWINS";
            AsyncPageable<BasicDigitalTwin> result = client.QueryAsync<BasicDigitalTwin>(query);
            // </RunQuery>
        }
        async public void QueryWithLoop(DigitalTwinsClient client)
        {
            // ------------------ RUN QUERY & LOOP THROUGH PAGEABLE RESULTS WITH TRY/CATCH (Full sample) ---------------------
            // <FullQuerySample>
            AsyncPageable<BasicDigitalTwin> result = client.QueryAsync<BasicDigitalTwin>("Select * From DigitalTwins");
            try
            {
                await foreach (BasicDigitalTwin twin in result)
                {
                    // You can include your own logic to print the result
                    // The logic below prints the twin's ID and contents
                    Console.WriteLine($"Twin ID: {twin.Id} \nTwin data");
                    foreach (KeyValuePair<string, object> kvp in twin.Contents)
                    {
                        Console.WriteLine($"{kvp.Key}  {kvp.Value}");
                    }
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error {ex.Status}, {ex.ErrorCode}, {ex.Message}");
                throw;
            }
            // </FullQuerySample>
        }
    }
}
