using Azure;
using Azure.DigitalTwins.Core;
using Microsoft.Azure.DigitalTwins.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTwins_Samples
{
    class ParseModelsSample
    {
        async Task ParseDemoAsync(DigitalTwinsClient client)
        {
            try
            {
                AsyncPageable<DigitalTwinsModelData> mdata = client.GetModelsAsync(new GetModelsOptions { IncludeModelDefinition = true });
                var models = new List<string>();
                await foreach (DigitalTwinsModelData md in mdata)
                    models.Add(md.DtdlModel);
                var parser = new ModelParser();
                IReadOnlyDictionary<Dtmi, DTEntityInfo> dtdlOM = await parser.ParseAsync(models);

                var interfaces = new List<DTInterfaceInfo>();
                IEnumerable<DTInterfaceInfo> ifenum =
                    from entity in dtdlOM.Values
                    where entity.EntityKind == DTEntityKind.Interface
                    select entity as DTInterfaceInfo;
                interfaces.AddRange(ifenum);
                foreach (DTInterfaceInfo dtif in interfaces)
                {
                    PrintInterfaceContent(dtif, dtdlOM);
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Failed due to {ex}");
            }
        }

        void PrintInterfaceContent(DTInterfaceInfo dtif, IReadOnlyDictionary<Dtmi, DTEntityInfo> dtdlOM, int indent = 0)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < indent; i++) sb.Append("  ");
            Console.WriteLine($"{sb}Interface: {dtif.Id} | {dtif.DisplayName}");
            Dictionary<string, DTContentInfo> contents = dtif.Contents;

            foreach (DTContentInfo item in contents.Values)
            {
                switch (item.EntityKind)
                {
                    case DTEntityKind.Property:
                        DTPropertyInfo pi = item as DTPropertyInfo;
                        Console.WriteLine($"{sb}--Property: {pi.Name} with schema {pi.Schema}");
                        break;
                    case DTEntityKind.Relationship:
                        DTRelationshipInfo ri = item as DTRelationshipInfo;
                        Console.WriteLine($"{sb}--Relationship: {ri.Name} with target {ri.Target}");
                        break;
                    case DTEntityKind.Telemetry:
                        DTTelemetryInfo ti = item as DTTelemetryInfo;
                        Console.WriteLine($"{sb}--Telemetry: {ti.Name} with schema {ti.Schema}");
                        break;
                    case DTEntityKind.Component:
                        DTComponentInfo ci = item as DTComponentInfo;
                        Console.WriteLine($"{sb}--Component: {ci.Id} | {ci.Name}");
                        DTInterfaceInfo component = ci.Schema;
                        PrintInterfaceContent(component, dtdlOM, indent + 1);
                        break;                
                }
            }
        }
    }
}
