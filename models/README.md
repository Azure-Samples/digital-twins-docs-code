# Model code snippets

This folder contains the source code for [models](https://docs.microsoft.com/azure/digital-twins/concepts-models) that are included in the Azure Digital Twins documentation. They are written in a JSON-LD-based language called **Digital Twin Definition Language (DTDL)**. 

Note that these are the model definitions themselves, not the complete SDK calls to submit these models to the service. For SDK call examples for your language of choice, see the folder of [SDK code snippets](/sdks).

## Contents

Below is a list of the model files contained in this folder, including mappings between the models and the documents in which they appear, and descriptions of each.

| JSON model file | Used in | Description
| --- | --- | --- |
| CelestialBody-Planet-Crater.json | [concepts-models](https://docs.microsoft.com/azure/digital-twins/concepts-models) | Illustrates model inheritance |
| coffeeMaker-coffeeMakerInterface-coffeeBar.json | [how-to-parse-models](https://docs.microsoft.com/azure/digital-twins/how-to-parse-models) | General multi-model example. Used as a reference for writing parser code |
| Moon.json | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | General model example, with two properties. Used for querying |
| patch-component-2.json | [how-to-interpret-event-data](https://docs.microsoft.com/azure/digital-twins/how-to-interpret-event-data) | General patch example that replaces a property and a component's property. Used to illustrate corresponding update notification |
| patch-component.json | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | Patch example that replaces a component's property |
| patch-decommission-model.json | [how-to-manage-model](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | Patch example for a model to decommission it |
| patch-model-1.json | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | Patch example that replaces the twin's model, when the twin conforms to new model already |
| patch-model-2.json | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | Patch example that replaces the twin's model, when the twin needs editing to conform to new model |
| patch-object-sub-property-1.json | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | Patch example that replaces a sub-property belonging to an object-type property of a twin -- step 1 |
| patch-object-sub-property-2.json | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | Patch example that replaces a sub-property belonging to an object-type property of a twin -- step 2 |
| patch.json | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | Basic patch example, replaces two properties |
| PatientRoom.json | [how-to-manage-model](https://docs.microsoft.com/azure/digital-twins/how-to-manage-model) | General model example, with three properties and a relationship |
| Planet-Crater-Moon.json | [concepts-models](https://docs.microsoft.com/azure/digital-twins/concepts-models) | Full model example, showing all field types |
| Planet-Moon.json | [how-to-manage-model](https://docs.microsoft.com/azure/digital-twins/how-to-manage-model) | Shows two (minimal) models placed in a JSON array |
| Room.json | [tutorial-command-line-app](https://docs.microsoft.com/azure/digital-twins/tutorial-command-line-app) | General model example, with three properties and a relationship. Used as the basis for a tutorial<br>*If this file is updated, make sure the line-number reference in the corresponding doc stays up-to-date. |
| SampleModel.json | [tutorial-code](https://docs.microsoft.com/azure/digital-twins/tutorial-code) | General model example, with a property and a relationship. Used as the basis for a tutorial |
| tags.json | [how-to-use-tags](https://docs.microsoft.com/azure/digital-twins/how-to-use-tags) | Two models with tags (one marker-type, one value-type).<br>*If this file is updated, make sure the line-number reference in the corresponding doc stays up-to-date. |
| Thermostat.json | [how-to-ingest-iot-hub-data](https://docs.microsoft.com/azure/digital-twins/how-to-ingest-iot-hub-data) | General model example, with one property. Used in a flow for ingesting IoT Hub data |

## Strategy

The preferred strategy for referencing code snippets in docs is by using [named tags](https://review.docs.microsoft.com/help/contribute/code-in-docs?branch=master#named-snippet) to identify the proper section of code from a document.

However, the JSON file format of model documents does not currently appear to be supported by the [DocFX Flavored Markdown's tag representation options](https://dotnet.github.io/docfx/spec/docfx_flavored_markdown.html#tag-name-representation-in-code-snippet-source-file). As a result, each model example in the docs is represented in its own JSON file that can be referenced by name in its entirety.