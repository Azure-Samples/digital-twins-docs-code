// ------------------ CREATE MODEL (SINGLE) ---------------------
// <CreateModel>
// 'client' is an instance of DigitalTwinsClient
// Read model file into string (not part of SDK)
StreamReader r = new StreamReader("MyModelFile.json");
string dtdl = r.ReadToEnd(); r.Close();
string[] dtdls = new string[] { dtdl };
client.CreateModels(dtdls);
// </CreateModel>

// ------------------ CREATE MODEL (MULTIPLE) ---------------------
// <CreateModels_multi>
var dtdlFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");

List<string> dtdlStrings = new List<string>();
foreach (string fileName in dtdlFiles)
{
    // Read model file into string (not part of SDK)
    StreamReader r = new StreamReader(fileName);
    string dtdl = r.ReadToEnd(); r.Close();
    dtdlStrings.Add(dtdl);
}
client.CreateModels(dtdlStrings);
// </CreateModels_multi>

// ------------------ GET MODELS ---------------------
// <GetModels>
// 'client' is a valid DigitalTwinsClient object

// Get a single model, metadata and data
DigitalTwinsModelData md1 = client.GetModel(id);

// Get a list of the metadata of all available models; print their display names and IDs
Pageable<DigitalTwinsModelData> pmd2 = client.GetModels();
foreach (DigitalTwinsModelData md in pmd2)
{
    Console.WriteLine($"Type name: {md.DisplayName}: {md.Id}");
}

// Get models and metadata for a model ID, including all dependencies (models that it inherits from, components it references)
Pageable<DigitalTwinsModelData> pmd3 = client.GetModels(new GetModelsOptions { IncludeModelDefinition = true });
// </GetModels>

// ------------------ DECOMISSION MODEL ---------------------
// <DecommissionModel>
// 'client' is a valid DigitalTwinsClient  
client.DecommissionModel(dtmiOfPlanetInterface);
// Write some code that deletes or transitions digital twins
//...
// </DecommissionModel>

// ------------------ DELETE MODEL ---------------------
// <DeleteModel>
// 'client' is a valid DigitalTwinsClient
await client.DeleteModelAsync(IDToDelete);
// </DeleteModel>