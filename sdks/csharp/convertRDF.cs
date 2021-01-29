using VDS.RDF.Ontology;
using VDS.RDF.Parsing;
using Microsoft.Azure.DigitalTwins.Parser;

//...

Console.WriteLine("Reading file...");

FileLoader.Load(_ontologyGraph, rdfFile.FullName);

// Start looping through for each owl:Class
foreach (OntologyClass owlClass in _ontologyGraph.OwlClasses)
{

    // Generate a DTMI for the owl:Class
    string Id = GenerateDTMI(owlClass);

    if (!String.IsNullOrEmpty(Id))
    {

        Console.WriteLine($"{owlClass.ToString()} -> {Id}");

        // Create Interface
        var dtdlInterface = new DtdlInterface
        {
            Id = Id,
            Type = "Interface",
            DisplayName = GetInterfaceDisplayName(owlClass),
            Comment = GetInterfaceComment(owlClass),
            Contents = new List<DtdlContents>(),
        };

        // Use DTDL 'extends' for super classes 
        IEnumerable<OntologyClass> foundSuperClasses = owlClass.DirectSuperClasses;

        //...
    }

    // Add interface to the list of interfaces
    _interfaceList.Add(dtdlInterface);
}

// Serialize to JSON
var json = JsonConvert.SerializeObject(_interfaceList);

//...