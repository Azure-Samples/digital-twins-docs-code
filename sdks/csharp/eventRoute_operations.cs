// ------------------ CREATE EVENT ROUTE (Basic) ---------------------
// <CreateEventRoute>
string eventFilter = "$eventType = 'DigitalTwinTelemetryMessages' or $eventType = 'DigitalTwinLifecycleNotification'";
var er = new DigitalTwinsEventRoute("endpointName", eventFilter);
await client.CreateOrReplaceEventRouteAsync("routeId", er);
// </CreateEventRoute>

// ------------------ CREATE, LIST, AND DELETE EVENT ROUTE (Full sample) ---------------------
// <FullEventRouteSample>
private static async Task CreateEventRouteAsync(DigitalTwinsClient client, string routeId, DigitalTwinsEventRoute er)
{
    try
    {
        Console.WriteLine("Create a route: testRoute1");

        // Make a filter that passes everything
        er.Filter = "true";
        await client.CreateOrReplaceEventRouteAsync(routeId, er);
        Console.WriteLine("Create route succeeded.");

        // List routes
        AsyncPageable<DigitalTwinsEventRoute> results = client.GetEventRoutesAsync();
        await foreach (DigitalTwinsEventRoute route in results)
        {
            Console.WriteLine($"Route {route.Id} to endpoint {route.EndpointName} with filter {route.Filter} ");
        }

        // Delete route created earlier
        Console.WriteLine($"Deleting route {routeId}:");
        await client.DeleteEventRouteAsync(routeId);
        }
    }
    catch (RequestFailedException e)
    {
        Console.WriteLine($"*** Error in event route processing ({e.ErrorCode}):\n${e.Message}");
    }
}
// </FullEventRouteSample>