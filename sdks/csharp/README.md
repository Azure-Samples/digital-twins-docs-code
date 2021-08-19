# SDK snippets (C#)

This folder contains SDK call examples that are included in the Azure Digital Twins documentation.

## Contents

Below is a list of the SDK code files that are contained in this folder, including mappings between the files and the documents in which they appear, and descriptions of each.

| SDK code file | Used in | Description |
| --- | --- | --- |
| IoTHubToTwins.cs | [how-to-ingest-iot-hub-data](https://docs.microsoft.com/azure/digital-twins/how-to-ingest-iot-hub-data) | The body of an [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that processes IoT telemetry events from [Azure IoT Hub](https://docs.microsoft.com/azure/iot-hub/about-iot-hub) into Azure Digital Twins |
| adtIngestFunctionSample.cs | [how-to-create-azure-function](https://docs.microsoft.com/azure/digital-twins/how-to-create-azure-function) | The body of a basic [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that is authenticated with Azure Digital Twins |
| authentication.cs | [how-to-authenticate-client](https://docs.microsoft.com/azure/digital-twins/how-to-authenticate-client) | Samples of authentication code |
| eventRoute_operations.cs | [concepts-route-events](https://docs.microsoft.com/azure/digital-twins/concepts-route-events)<br><br>[how-to-manage-routes](https://docs.microsoft.com/azure/digital-twins/how-to-manage-routes) | Event route operations, including creating, listing, and deleting |
| fullClientApp.cs | [tutorial-code](https://docs.microsoft.com/azure/digital-twins/tutorial-code) | Code for a custom client app for interacting with Azure Digital Twins |
| fullClientApp_excerpt_model.cs | [tutorial-code](https://docs.microsoft.com/azure/digital-twins/tutorial-code) | Excerpt from a custom client app that is a simple way to upload models |
| getQueryCharges.cs | [concepts-query-units](https://docs.microsoft.com/azure/digital-twins/concepts-query-units) | Code to find the number of query units consumed by a query request |
| graphFromCSV.cs | [how-to-manage-graph](https://docs.microsoft.com/azure/digital-twins/how-to-manage-graph) | Code to create a twin graph out of a CSV file |
| graph_operations_other.cs | [concepts-twins-graph](https://docs.microsoft.com/azure/digital-twins/concepts-twins-graph) | Specific examples of graph operations: Other examples of creating relationships, and listing relationship properties |
| graph_operations_sample.cs | [how-to-manage-graph](https://docs.microsoft.com/azure/digital-twins/how-to-manage-graph) | A full graph sample that creates a twin graph from digital twins and relationships |
| model_operations.cs | [how-to-manage-model](https://docs.microsoft.com/azure/digital-twins/how-to-manage-model) | Model operations, including creating, retrieving, decommissioning, and deleting models |
| parseModels.cs | [how-to-parse-models](https://docs.microsoft.com/azure/digital-twins/how-to-parse-models) | Code that uses the [Microsoft.Azure.DigitalTwins.Parser library](https://nuget.org/packages/Microsoft.Azure.DigitalTwins.Parser/) to parse and validate DTDL models |
| queries.cs | [how-to-query-graph](https://docs.microsoft.com/azure/digital-twins/how-to-query-graph) | Query operations, including the basic query call and a longer example that interprets results |
| signalRFunction.cs | [how-to-integrate-azure-signalr](https://docs.microsoft.com/azure/digital-twins/how-to-integrate-azure-signalr) | Sets up two [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that connects and broadcasts Azure Digital Twins data to connected client applications |
| twin_operations_other.cs | [concepts-twins-graph](https://docs.microsoft.com/azure/digital-twins/concepts-twins-graph)<br><br>[how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin)<br><br>[how-to-use-tags](https://docs.microsoft.com/azure/digital-twins/how-to-use-tags) | Specific examples of twin operations: creating a twin without helper classes, and creating a more complex patch |
| twin_operations_sample.cs | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin) | A full twin sample that creates a twin, update its details, and delete the twin |
| updateMaps.cs | [how-to-integrate-maps](https://docs.microsoft.com/azure/digital-twins/how-to-integrate-maps) | The body of an [Azure Event Grid](https://docs.microsoft.com/azure/event-grid/overview)-triggered [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that receives notifications from Azure Digital Twins updates and sends them along to [Azure Maps](https://docs.microsoft.com/azure/azure-maps/about-azure-maps)|
| updateTSI.cs | [how-to-integrate-time-series-insights](https://docs.microsoft.com/azure/digital-twins/how-to-integrate-time-series-insights) | The body of an [Azure Event Hubs](https://docs.microsoft.com/azure/event-hubs/event-hubs-about)-triggered [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that converts twin update events from their original JSON Patch form to JSON objects that can be consumed by [Azure Time Series Insights (TSI)](https://docs.microsoft.com/azure/time-series-insights/overview-what-is-tsi) |

## Strategy

Some of the files in this folder contain partial snippets that illustrate a particular category of SDK call--models, twins, graphs, queries, eventRoutes, and authentication--and are titled accordingly. These contain [named tags](https://review.docs.microsoft.com/help/contribute/code-in-docs?branch=master#named-snippet) that are used in the docs to reference a particular call or example from the file.

Other files in this folder are larger, self-contained snippets that combine several SDK calls to accomplish a specific purpose, according to a tutorial or how-to. Since these are individual samples, each one is kept separately as its own file. Documents can then reference these files by name to pull in the entire sample.

## Maintenance

The samples in this folder are part of a buildable project, **DigitalTwins Samples.sln**. This means the solution can be downloaded and built to verify code changes.