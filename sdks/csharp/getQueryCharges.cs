AsyncPageable<BasicDigitalTwin> asyncPageableResponseWithCharge = client.QueryAsync<BasicDigitalTwin>("SELECT * FROM digitaltwins");
int pageNum = 0;

// The "await" keyword here is required, as a call is made when fetching a new page.

await foreach (Page<BasicDigitalTwin> page in asyncPageableResponseWithCharge.AsPages())
{
    Console.WriteLine($"Page {++pageNum} results:");

    // Extract the query-charge header from the page

    if (QueryChargeHelper.TryGetQueryCharge(page, out float queryCharge))
    {
        Console.WriteLine($"Query charge was: {queryCharge}");
    }

    // Iterate over the twin instances.

    // The "await" keyword is not required here, as the paged response is local.

    foreach (BasicDigitalTwin twin in page.Values)
    {
        Console.WriteLine($"Found digital twin '{twin.Id}'");
    }
}
