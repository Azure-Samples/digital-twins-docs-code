Console.WriteLine();
Console.WriteLine($"Upload a model");
string dtdl = File.ReadAllText("SampleModel.json");
var models = new List<string>();
models.Add(dtdl);
// Upload the model to the service
await client.CreateModelsAsync(models);