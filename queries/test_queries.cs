//Setup for runnable sample
string adtInstanceEndpoint = "https://<your-instance-hostname>";

var credential = new DefaultAzureCredential();
DigitalTwinsClient client = new DigitalTwinsClient(new Uri(adtInstanceEndpoint), credential);

string query = "";
AsyncPageable<BasicDigitalTwin> result = client.QueryAsync<BasicDigitalTwin>(query);

//Set up resources to query
// *****NOT YET WRITTEN********

//Run queries
// *****NOT YET WRITTEN********

    // Logic: For each tag in queries.sql,
    // {
        // string query = tag;
        // result = client.QueryAsync<BasicDigitalTwin>(query);
        // print result
    // }