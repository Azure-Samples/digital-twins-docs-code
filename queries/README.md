# Query code snippets

This folder contains the source code for [queries](https://docs.microsoft.com/azure/digital-twins/concepts-query-language) that are included in the Azure Digital Twins documentation. They are written in a SQL-like query language referred to as the Azure Digital Twins query language.

Note that this is the text of the queries themselves, not the complete SDK calls to submit these queries to the service. For SDK call examples for your language of choice, see the folder of [SDK code snippets](/sdks).

## Contents

Below is a list of the query files contained in this folder, including mappings between the queries and the documents in which they appear, and descriptions of each.

| Query file | Used in | Description
| --- | --- | --- |
| examples.sql | [how-to-manage-twin](https://docs.microsoft.com/azure/digital-twins/how-to-manage-twin)<br><br>[how-to-query-graph](https://docs.microsoft.com/azure/digital-twins/how-to-query-graph)<br><br>[how-to-use-tags](https://docs.microsoft.com/azure/digital-twins/how-to-use-tags)<br><br>[tutorial-end-to-end](https://docs.microsoft.com/azure/digital-twins/tutorial-end-to-end)<br><br>[quickstart-azure-digital-twins-explorer](https://docs.microsoft.com/azure/digital-twins/quickstart-azure-digital-twins-explorer)| This file contains the code snippets for the example queries that are shown in these documents. |
| reference.sql | Query language reference documentation (accessible from [Azure Digital Twins documentation](https://docs.microsoft.com/azure/digital-twins/) TOC | This file contains the code snippets for the queries that are shown in these reference documents. |
| test_queries.cs | *None* | *FOR TESTING PURPOSES*<br>The code in this file runs the queries in **queries.sql** to test whether they work as expected. |

## Strategy

The preferred strategy for referencing code snippets in docs is by using [named tags](https://review.docs.microsoft.com/help/contribute/code-in-docs?branch=master#named-snippet) to identify the proper section of code from a document.

This is possible for SQL documents, so the queries in this folder are given tags that their docs counterparts can use to identify them.
