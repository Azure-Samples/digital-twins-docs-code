// ------------------ RUN QUERY (Basic) ---------------------
// <RunQuery>
    // Run a query for all twins   
    string query = "SELECT * FROM DIGITALTWINS";
    AsyncPageable<BasicDigitalTwin> result = client.QueryAsync<BasicDigitalTwin>(query);
// </RunQuery>

// ------------------ RUN QUERY & LOOP THROUGH PAGEABLE RESULTS WITH TRY/CATCH (Full sample) ---------------------
// <FullQuerySample>
AsyncPageable<string> result = client.QueryAsync("Select * From DigitalTwins");
try
{
    await foreach(BasicDigitalTwin twin in result)
        {
            // You can include your own logic to print the result
            // The logic below prints the twin's ID and contents
            Console.WriteLine($"Twin ID: {twin.Id} \nTwin data");
            IDictionary<string, object> contents = twin.Contents;
            foreach (KeyValuePair<string, object> kvp in contents)
            {
                Console.WriteLine($"{kvp.Key}  {kvp.Value}");
            }
        }
}
catch (RequestFailedException e)
{
    Console.WriteLine($"Error {e.Status}: {e.Message}");
    throw;
}
// </FullQuerySample>