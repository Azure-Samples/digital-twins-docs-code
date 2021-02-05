// ------------------ CREATE MODEL (SINGLE) ---------------------
// <CreateModel>
// 'client' is an instance of DigitalTwinsClient
// Read model file into string (not part of SDK)
using var modelStreamReader = new StreamReader("MyModelFile.json");
string dtdl = modelStreamReader.ReadToEnd();
string[] dtdls = new string[] { dtdl };
client.CreateModels(dtdls);
// </CreateModel>

// ------------------ CREATE MODEL (MULTIPLE) ---------------------
// <CreateModels_multi>
var dtdlFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");

var dtdlModels = new List<string>();
foreach (string fileName in dtdlFiles)
{
    // Read model file into string (not part of SDK)
    using var modelStreamReader = new StreamReader(fileName);
    string dtdl = modelStreamReader.ReadToEnd();
    dtdlModels.Add(dtdl);
}
client.CreateModels(dtdlModels);
// </CreateModels_multi>

// ------------------ GET MODELS ---------------------
// <GetModels>
// 'client' is a valid DigitalTwinsClient object

// Get a single model, metadata and data
DigitalTwinsModelData md1 = client.GetModel(id);

// Get a list of the metadata of all available models; print their display names and IDs
AsyncPageable<DigitalTwinsModelData> pmd2 = client.GetModelsAsync();
await foreach (DigitalTwinsModelData md in pmd2)
{
    Console.WriteLine($"Type name: {md.DisplayName}: {md.Id}");
}

// Get models and metadata for a model ID, including all dependencies (models that it inherits from, components it references)
Pageable<DigitalTwinsModelData> pmd3 = client.GetModels(new GetModelsOptions { IncludeModelDefinition = true });
// </GetModels>

// ------------------ DECOMISSION MODEL ---------------------
// <DecommissionModel>
// 'client' is a valid DigitalTwinsClient
await client.DecommissionModelAsync(dtmiOfPlanetInterface);
// Write some code that deletes or transitions digital twins
//...
// </DecommissionModel>

// ------------------ DELETE MODEL ---------------------
// <DeleteModel>
// 'client' is a valid DigitalTwinsClient
await client.DeleteModelAsync(IDToDelete);
// </DeleteModel>