using VDS.RDF.Ontology;
using VDS.RDF.Parsing;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DigitalTwins_Samples
{
    public class DigitalTwinsConvertRDFSample
    {

        public void Run()
        {

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

                    // An OWL graph can have parent/child classes.
                    // So to understand if an OWL class is a base class or a child class,
                    // we can look for a superclass on any given OWL class.
                    // If found, we convert these to parent + child Interfaces using DTDL extends.
                    IEnumerable<OntologyClass> foundSuperClasses = owlClass.DirectSuperClasses;

                    //...
                }

                // Add interface to the list of interfaces
                _interfaceList.Add(dtdlInterface);
            }

            // Serialize to JSON
            var json = JsonConvert.SerializeObject(_interfaceList);

        }
    }
}