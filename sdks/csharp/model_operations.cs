using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace DigitalTwins_Samples
{
    class ModelOperationsSample
    {
        async public void CreateModelSingle(DigitalTwinsClient client)
        {
            // ------------------ CREATE MODEL (SINGLE) ---------------------
            // <CreateModel>
            // 'client' is an instance of DigitalTwinsClient
            // Read model file into string (not part of SDK)
            string dtdl = File.ReadAllText("MyModelFile.json");
            await client.CreateModelsAsync(new[] { dtdl });
            // </CreateModel>
        }
        async public void CreateModelMultiple(DigitalTwinsClient client, string sourceDirectory)
        {
            // ------------------ CREATE MODEL (MULTIPLE) ---------------------
            // <CreateModels_multi>
            var dtdlFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");

            var dtdlModels = new List<string>();
            foreach (string fileName in dtdlFiles)
            {
                // Read model file into string (not part of SDK)
                string dtdl = File.ReadAllText(fileName);
                dtdlModels.Add(dtdl);
            }
            await client.CreateModelsAsync(dtdlModels);
            // </CreateModels_multi>
        }
        async public void GetModels(DigitalTwinsClient client)
        {
            // ------------------ GET MODELS ---------------------
            // <GetModels>
            // 'client' is a valid DigitalTwinsClient object

            // Get a single model, metadata and data
            Response<DigitalTwinsModelData> md1 = await client.GetModelAsync("<model-Id>");
            DigitalTwinsModelData model1 = md1.Value;

            // Get a list of the metadata of all available models; print their display names and IDs
            AsyncPageable<DigitalTwinsModelData> md2 = client.GetModelsAsync();
            await foreach (DigitalTwinsModelData md in md2)
            {
                Console.WriteLine($"Type name: {md.DisplayName}: {md.Id}");
            }

            // Get models and metadata for a model ID, including all dependencies (models that it inherits from, components it references)
            AsyncPageable<DigitalTwinsModelData> md3 = client.GetModelsAsync(new GetModelsOptions { IncludeModelDefinition = true });
            // </GetModels>
        }
        async public void DecomissionModel(DigitalTwinsClient client, string dtmiOfPlanetInterface)
        {
            // ------------------ DECOMISSION MODEL ---------------------
            // <DecommissionModel>
            // 'client' is a valid DigitalTwinsClient
            await client.DecommissionModelAsync(dtmiOfPlanetInterface);
            // Write some code that deletes or transitions digital twins
            //...
            // </DecommissionModel>
        }
        async public void DeleteModel(DigitalTwinsClient client, string IDToDelete)
        {
            // ------------------ DELETE MODEL ---------------------
            // <DeleteModel>
            // 'client' is a valid DigitalTwinsClient
            await client.DeleteModelAsync(IDToDelete);
            // </DeleteModel>
        }
    }
}
