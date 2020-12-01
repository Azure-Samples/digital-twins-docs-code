//USED IN: how-to-query-graph, how-to-use-tags
//Also corresponds to queries.sql and can be used to test these queries present in both files

//Setup for runnable sample
string adtInstanceEndpoint = "https://<your-instance-hostname>";

var credential = new DefaultAzureCredential();
DigitalTwinsClient client = new DigitalTwinsClient(new Uri(adtInstanceEndpoint), credential);

// Query to get all twins 
string query = "SELECT * FROM DIGITALTWINS";
AsyncPageable<BasicDigitalTwin> result = client.QueryAsync<BasicDigitalTwin>(query);

// Query marker tags
query = "SELECT * FROM digitaltwins WHERE is_defined(tags.red)";
result = client.QueryAsync<BasicDigitalTwin>(query);
query = "SELECT * FROM digitaltwins WHERE NOT is_defined(tags.red) AND is_defined(tags.round)";
result = client.QueryAsync<BasicDigitalTwin>(query);

// Query value tags
query = "SELECT * FROM digitaltwins WHERE NOT is_defined(tags.red) AND tags.size = 'small'";
result = client.QueryAsync<BasicDigitalTwin>(query);

//Query by property
query = "SELECT  * FROM DigitalTwins T WHERE T.firmwareVersion = '1.1' AND T.$dtId in ['123', '456'] AND T.Temperature = 70";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT *​ FROM DIGITALTWINS WHERE IS_DEFINED(Location)";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT * FROM DIGITALTWINS​ T WHERE IS_NUMBER(T.Temperature)";
result = client.QueryAsync<BasicDigitalTwin>(query);

// Query by model
query = "SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:example:thing;1')";
result = client.QueryAsync<BasicDigitalTwin>(query);
query = "SELECT * FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:thing;1')";
result = client.QueryAsync<BasicDigitalTwin>(query);
query = "SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:example:thing;1', exact)";
result = client.QueryAsync<BasicDigitalTwin>(query);
query = "SELECT ROOM FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:thing;1', exact)";
result = client.QueryAsync<BasicDigitalTwin>(query);

// Query by relationship
query = "SELECT T, CT FROM DIGITALTWINS T JOIN CT RELATED T.contains WHERE T.$dtId = 'ABC'";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT T, SBT, R FROM DIGITALTWINS T JOIN SBT RELATED T.servicedBy R WHERE T.$dtId = 'ABC' AND R.reportedCondition = 'clean'";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT LightBulb FROM DIGITALTWINS Room JOIN LightPanel RELATED Room.contains JOIN LightBulb RELATED LightPanel.contains WHERE IS_OF_MODEL(LightPanel, 'dtmi:contoso:com:lightpanel;1') AND IS_OF_MODEL(LightBulb, 'dtmi:contoso:com:lightbulb ;1') AND Room.$dtId IN ['room1', 'room2']";
result = client.QueryAsync<BasicDigitalTwin>(query);

// Count items
query = "SELECT COUNT() FROM DIGITALTWINS";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT COUNT() FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:sample:Room;1')";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT COUNT() FROM DIGITALTWINS c WHERE IS_OF_MODEL('dtmi:sample:Room;1') AND c.Capacity > 20";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT COUNT() FROM DIGITALTWINS Room JOIN LightPanel RELATED Room.contains JOIN LightBulb RELATED LightPanel.contains WHERE IS_OF_MODEL(LightPanel, 'dtmi:contoso:com:lightpanel;1') AND IS_OF_MODEL(LightBulb, 'dtmi:contoso:com:lightbulb ;1') AND Room.$dtId IN ['room1', 'room2']";
result = client.QueryAsync<BasicDigitalTwin>(query);

// Select top items
query = "SELECT TOP (5) FROM DIGITALTWINS";
result = client.QueryAsync<BasicDigitalTwin>(query);

// Specify return set with projections
query = "SELECT Consumer, Factory, Edge FROM DIGITALTWINS Factory JOIN Consumer RELATED Factory.customer Edge WHERE Factory.$dtId = 'ABC'";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT Consumer.name FROM DIGITALTWINS Factory JOIN Consumer RELATED Factory.customer Edge WHERE Factory.$dtId = 'ABC' AND IS_PRIMITIVE(Consumer.name)";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT Consumer.name, Edge.prop1, Edge.prop2, Factory.area FROM DIGITALTWINS Factory JOIN Consumer RELATED Factory.customer Edge WHERE Factory.$dtId = 'ABC' AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name) AND IS_PRIMITIVE(Edge.prop1) AND IS_PRIMITIVE(Edge.prop2)";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT Consumer.name AS consumerName, Edge.prop1 AS first, Edge.prop2 AS second, Factory.area AS factoryArea FROM DIGITALTWINS Factory JOIN Consumer RELATED Factory.customer Edge WHERE Factory.$dtId = 'ABC' AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name) AND IS_PRIMITIVE(Edge.prop1) AND IS_PRIMITIVE(Edge.prop2)";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT Consumer.name AS consumerName, Factory FROM DIGITALTWINS Factory JOIN Consumer RELATED Factory.customer Edge WHERE Factory.$dtId = 'ABC' AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name)";
result = client.QueryAsync<BasicDigitalTwin>(query);

// Use the IN operator
query = "SELECT Floor FROM DIGITALTWINS Building JOIN Floor RELATED Building.contains WHERE Building.$dtId = @buildingId";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT Room FROM DIGITALTWINS Floor JOIN Room RELATED Floor.contains WHERE Floor.$dtId IN ['floor1','floor2'] AND Room. Temperature > 72 AND IS_OF_MODEL(Room, 'dtmi:com:contoso:Room;1')";
result = client.QueryAsync<BasicDigitalTwin>(query);

// Other compound query examples
query = "SELECT device FROM DigitalTwins space JOIN device RELATED space.has WHERE space.$dtid = 'Room 123' AND device.$metadata.model = 'dtmi:contoso:com:DigitalTwins:MxChip:3' AND has.role = 'Operator'";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT Room FROM DIGITALTWINS Room JOIN Thermostat RELATED Room.Contains WHERE Thermostat.$dtId = 'id1'";
result = client.QueryAsync<BasicDigitalTwin>(query);

query = "SELECT Room FROM DIGITALTWINS Floor JOIN Room RELATED Floor.Contains WHERE Floor.$dtId = 'floor11' AND IS_OF_MODEL(Room, 'dtmi:contoso:com:DigitalTwins:Room;1')";
result = client.QueryAsync<BasicDigitalTwin>(query);