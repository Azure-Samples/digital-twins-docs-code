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

// ------------------ Paging pattern for query API ---------------------
// <PagedQuery>
try
{
    // Repeat the query while there are pages
    string query = "SELECT * FROM digitaltwins";
    AsyncPageable<BasicDigitalTwin> queryResult = await client.QueryAsync(query);
    int page = 0;
    await foreach (Page<BasicDigitalTwin> page in queryResult.AsPages())
    {
        Console.WriteLine($"== Query results page {++page}:");

        // Extract the query-charge header from the page
        if (QueryChargeHelper.TryGetQueryCharge(page, out float queryCharge))
        {
            Console.WriteLine($"Query charge was: {queryCharge}");
        }

        foreach (BasicDigitalTwin twin in page.Values)
        {
            Console.WriteLine($"  Found {twin.Id}");
        }
    }
}
catch (RequestFailedException ex)
{
    Console.WriteLine($"*** Error in twin query: {ex.Status}, {ex.ErrorCode}, {ex.Message}");
}
// </PagedQuery>