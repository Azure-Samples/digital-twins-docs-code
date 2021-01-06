# Sample code snippets (C#)

This folder contains the source code for specific samples that are included in the Azure Digital Twins documentation. These are larger, self-contained snippets that combine several SDK calls to accomplish a specific purpose, according to a tutorial or how-to.

## Contents

Below is a list of the samples that are contained in this folder, including mappings between the samples and the documents in which they appear, and descriptions of each.

| Sample file | Used in | Description
| --- | --- | --- |
| adtIngestFunctionSample.cs | [how-to-create-azure-function](https://docs.microsoft.com/azure/digital-twins/how-to-create-azure-function) | The body of a basic [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that is authenticated with Azure Digital Twins |
| adtIoTHub_allocate.cs | [how-to-provision-using-device-provisioning-service](https://docs.microsoft.com/azure/digital-twins/how-to-provision-using-device-provisioning-service) | The body of an HTTP request-triggered [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that [Azure IoT Hub Device Provisioning Service (DPS)](https://docs.microsoft.com/azure/iot-dps/about-iot-dps) uses to provision a new device |
| adtIoTHub_delete.cs | [how-to-provision-using-device-provisioning-service](https://docs.microsoft.com/azure/digital-twins/how-to-provision-using-device-provisioning-service) | The body of an [Azure Event Hubs](https://docs.microsoft.com/azure/event-hubs/event-hubs-about)-triggered [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that [Azure IoT Hub](https://docs.microsoft.com/azure/iot-hub/about-iot-hub) lifecycle events uses to retire an existing device |
| convertRDF.cs | [how-to-integrate-models](https://docs.microsoft.com/azure/digital-twins/how-to-integrate-models) | Shows how to load an RDF model file into a graph and convert it to DTDL |
| fullClientApp_excerpt_model.cs | [tutorial-code](https://docs.microsoft.com/azure/digital-twins/tutorial-code) | Excerpt from a custom client app that is a simple way to upload models |
| fullClientApp.cs | [tutorial-code](https://docs.microsoft.com/azure/digital-twins/tutorial-code) | Code for a custom client app for interacting with Azure Digital Twins |
| getQueryCharges.cs | [concepts-query-units](https://docs.microsoft.com/azure/digital-twins/concepts-query-units) | Code to find the number of query units consumed by a query request |
| IoTHubToTwins.cs | [how-to-ingest-iot-hub-data](https://docs.microsoft.com/azure/digital-twins/how-to-ingest-iot-hub-data) | The body of an [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that processes IoT telemetry events from [Azure IoT Hub](https://docs.microsoft.com/azure/iot-hub/about-iot-hub) into Azure Digital Twins |
| signalRFunction.cs | [how-to-integrate-azure-signalr](https://docs.microsoft.com/azure/digital-twins/how-to-integrate-azure-signalr) | Sets up two [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that connects and broadcasts Azure Digital Twins data to connected client applications |
| updateMaps.cs | [how-to-integrate-maps](https://docs.microsoft.com/azure/digital-twins/how-to-integrate-maps) | The body of an [Azure Event Grid](https://docs.microsoft.com/azure/event-grid/overview)-triggered [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that receives notifications from Azure Digital Twins updates and sends them along to [Azure Maps](https://docs.microsoft.com/azure/azure-maps/about-azure-maps)|
| updateTSI.cs | [how-to-integrate-time-series-insights](https://docs.microsoft.com/azure/digital-twins/how-to-integrate-time-series-insights) | The body of an [Azure Event Hubs](https://docs.microsoft.com/azure/event-hubs/event-hubs-about)-triggered [Azure Function](https://docs.microsoft.com/azure/azure-functions/functions-overview) that converts twin update events from their original JSON Patch form to JSON objects that can be consumed by [Azure Time Series Insights (TSI)](https://docs.microsoft.com/azure/time-series-insights/overview-what-is-tsi) |

## Strategy

Since these are separate samples, each one is kept separately as its own file. Documents can then reference these files by name to pull in the entire sample.

There are also places where one of the larger sample files contains [named tags](https://review.docs.microsoft.com/help/contribute/code-in-docs?branch=master#named-snippet) to identify smaller sections, which are called out in the docs and displayed separately.
