-- USED IN: tutorial-end-to-end, how-to-use-tags, how-to-query-graph

-- Query to get all twins
SELECT * 
FROM DIGITALTWINS

-- Query marker tags
SELECT * FROM digitaltwins WHERE is_defined(tags.red)
SELECT * FROM digitaltwins WHERE NOT is_defined(tags.red) AND is_defined(tags.round)

-- Query value tags
SELECT * FROM digitaltwins WHERE NOT is_defined(tags.red) AND tags.size = 'small'

-- Query by property
SELECT  *
FROM DigitalTwins T  
WHERE T.firmwareVersion = '1.1'
AND T.$dtId in ['123', '456']
AND T.Temperature = 70

SELECT *​ FROM DIGITALTWINS WHERE IS_DEFINED(Location)
SELECT * FROM DIGITALTWINS​ T WHERE IS_NUMBER(T.Temperature)

-- Query by model
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:example:thing;1')
SELECT * FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:thing;1')
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:example:thing;1', exact)
SELECT ROOM FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:thing;1', exact)

-- Query by relationship
SELECT T, CT
FROM DIGITALTWINS T
JOIN CT RELATED T.contains
WHERE T.$dtId = 'ABC'

SELECT T, SBT, R
FROM DIGITALTWINS T
JOIN SBT RELATED T.servicedBy R
WHERE T.$dtId = 'ABC'
AND R.reportedCondition = 'clean'

SELECT LightBulb
FROM DIGITALTWINS Room
JOIN LightPanel RELATED Room.contains
JOIN LightBulb RELATED LightPanel.contains
WHERE IS_OF_MODEL(LightPanel, 'dtmi:contoso:com:lightpanel;1')
AND IS_OF_MODEL(LightBulb, 'dtmi:contoso:com:lightbulb ;1')
AND Room.$dtId IN ['room1', 'room2']

-- Count items
SELECT COUNT()
FROM DIGITALTWINS

SELECT COUNT()
FROM DIGITALTWINS
WHERE IS_OF_MODEL('dtmi:sample:Room;1')

SELECT COUNT()
FROM DIGITALTWINS c
WHERE IS_OF_MODEL('dtmi:sample:Room;1') AND c.Capacity > 20

SELECT COUNT()  
FROM DIGITALTWINS Room  
JOIN LightPanel RELATED Room.contains  
JOIN LightBulb RELATED LightPanel.contains  
WHERE IS_OF_MODEL(LightPanel, 'dtmi:contoso:com:lightpanel;1')  
AND IS_OF_MODEL(LightBulb, 'dtmi:contoso:com:lightbulb ;1')  
AND Room.$dtId IN ['room1', 'room2']

-- Select top items
SELECT TOP (5)
FROM DIGITALTWINS
WHERE ...

-- Specify return set with projections
SELECT Consumer, Factory, Edge
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'

SELECT Consumer.name
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Consumer.name)

SELECT Consumer.name, Edge.prop1, Edge.prop2, Factory.area
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name) AND IS_PRIMITIVE(Edge.prop1) AND IS_PRIMITIVE(Edge.prop2)

SELECT Consumer.name AS consumerName, Edge.prop1 AS first, Edge.prop2 AS second, Factory.area AS factoryArea
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name) AND IS_PRIMITIVE(Edge.prop1) AND IS_PRIMITIVE(Edge.prop2)

SELECT Consumer.name AS consumerName, Factory
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name)

-- Use the IN operator
SELECT Floor
FROM DIGITALTWINS Building
JOIN Floor RELATED Building.contains
WHERE Building.$dtId = @buildingId

SELECT Room
FROM DIGITALTWINS Floor
JOIN Room RELATED Floor.contains
WHERE Floor.$dtId IN ['floor1','floor2', ..'floorn']
AND Room. Temperature > 72
AND IS_OF_MODEL(Room, 'dtmi:com:contoso:Room;1')

-- Other compound query examples
SELECT device
FROM DigitalTwins space
JOIN device RELATED space.has
WHERE space.$dtid = 'Room 123'
AND device.$metadata.model = 'dtmi:contoso:com:DigitalTwins:MxChip:3'
AND has.role = 'Operator' 

SELECT Room
FROM DIGITALTWINS Room
JOIN Thermostat RELATED Room.Contains
WHERE Thermostat.$dtId = 'id1'

SELECT Room
FROM DIGITALTWINS Floor
JOIN Room RELATED Floor.Contains
WHERE Floor.$dtId = 'floor11'
AND IS_OF_MODEL(Room, 'dtmi:contoso:com:DigitalTwins:Room;1')