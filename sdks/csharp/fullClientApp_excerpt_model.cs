using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace DigitalTwins_Samples
{
    class ClientAppExcerptModelsSample
    {
        async public void Run(DigitalTwinsClient client)
        {
            // <ClientExcerptModel>
            Console.WriteLine();
            Console.WriteLine($"Upload a model");
            string dtdl = File.ReadAllText("SampleModel.json");
            var models = new List<string> { dtdl };
            // Upload the model to the service
            await client.CreateModelsAsync(models);
            // </ClientExcerptModel>
        }
    }
}
