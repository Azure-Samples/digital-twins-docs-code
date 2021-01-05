-------------- Used in tutorial-end-to-end
-- (Shows how to query all twins)
-- <GetAllTwins>
SELECT * 
FROM DIGITALTWINS
-- </GetAllTwins>

-------------- Used in how-to-query-graph
-- GetAllTwins from above

-- (Shows basic property query)
-- <QueryByProperty-1>
SELECT  *
FROM DigitalTwins T  
WHERE T.firmwareVersion = '1.1'
AND T.$dtId in ['123', '456']
AND T.Temperature = 70
-- </QueryByProperty-1>

-- (Shows property query based on whether property is defined)
-- <QueryByProperty-2>
SELECT *​ FROM DIGITALTWINS WHERE IS_DEFINED(Location)
-- </QueryByProperty-2>

-- (Shows property query based on marker tags)
-- <QueryMarkerTags-1>
SELECT * FROM digitaltwins WHERE is_defined(tags.red)
-- </QueryMarkerTags-1>

-- (Shows property query based on property type)
-- <QueryByProperty-3>
SELECT * FROM DIGITALTWINS​ T WHERE IS_NUMBER(T.Temperature)
-- </QueryByProperty-3>

-- (Shows model query with only twinTypeName parameter)
-- <QueryByModel-1>
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:example:thing;1')
-- </QueryByModel-1>

-- (Shows model query with twinCollection and twinTypeName parameters)
-- <QueryByModel-2>
SELECT * FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:thing;1')
-- </QueryByModel-2>

-- (Shows model query with twinTypeName and exact parameters)
-- <QueryByModel-3>
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:example:thing;1', exact)
-- </QueryByModel-3>

-- (Shows model query with twinCollection, twinTypeName, and exact parameters)
-- <QueryByModel-4>
SELECT ROOM FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:thing;1', exact)
-- </QueryByModel-4>

-- (Shows basic relationship query)
-- <QueryByRelationship-1>
SELECT T, CT
FROM DIGITALTWINS T
JOIN CT RELATED T.contains
WHERE T.$dtId = 'ABC'
-- </QueryByRelationship-1>

-- (Shows querying properties of relationship)
-- <QueryByRelationship-2>
SELECT T, SBT, R
FROM DIGITALTWINS T
JOIN SBT RELATED T.servicedBy R
WHERE T.$dtId = 'ABC'
AND R.reportedCondition = 'clean'
-- </QueryByRelationship-2>

-- (Shows multiple relationship JOINs in a single query)
-- </QueryByRelationship-3>
SELECT LightBulb
FROM DIGITALTWINS Room
JOIN LightPanel RELATED Room.contains
JOIN LightBulb RELATED LightPanel.contains
WHERE IS_OF_MODEL(LightPanel, 'dtmi:contoso:com:lightpanel;1')
AND IS_OF_MODEL(LightBulb, 'dtmi:contoso:com:lightbulb ;1')
AND Room.$dtId IN ['room1', 'room2']
-- </QueryByRelationship-3>

-- (Shows basic use of COUNT)
-- <SelectCount-1>
SELECT COUNT()
FROM DIGITALTWINS
-- </SelectCount-1>

-- (Shows basic use of COUNT with an added WHERE clause)
-- <SelectCount-2>
SELECT COUNT()
FROM DIGITALTWINS
WHERE IS_OF_MODEL('dtmi:sample:Room;1')

SELECT COUNT()
FROM DIGITALTWINS c
WHERE IS_OF_MODEL('dtmi:sample:Room;1') AND c.Capacity > 20
-- </SelectCount-2>

-- (Shows COUNT being used with JOIN)
-- <SelectCount-3>
SELECT COUNT()  
FROM DIGITALTWINS Room  
JOIN LightPanel RELATED Room.contains  
JOIN LightBulb RELATED LightPanel.contains  
WHERE IS_OF_MODEL(LightPanel, 'dtmi:contoso:com:lightpanel;1')  
AND IS_OF_MODEL(LightBulb, 'dtmi:contoso:com:lightbulb;1')  
AND Room.$dtId IN ['room1', 'room2']
-- </SelectCount-3>

-- (Shows basic use of SELECT TOP)
-- <SelectTop>
SELECT TOP (5)
FROM DIGITALTWINS
WHERE ...
-- </SelectTop>

-- (Shows basic use of projections)
-- <Projections-1>
SELECT Consumer, Factory, Edge
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
-- </Projections-1>

-- (Shows use of projection to return a twin's property)
-- <Projections-2>
SELECT Consumer.name
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Consumer.name)
-- </Projections-2>

-- (Shows use of projection to return a relationship's property)
-- <Projections-3>
SELECT Consumer.name, Edge.prop1, Edge.prop2, Factory.area
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name) AND IS_PRIMITIVE(Edge.prop1) AND IS_PRIMITIVE(Edge.prop2)
-- </Projections-3>

-- (Same as Projections-3 except adds aliases to result set)
-- <Projections-4>
SELECT Consumer.name AS consumerName, Edge.prop1 AS first, Edge.prop2 AS second, Factory.area AS factoryArea
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name) AND IS_PRIMITIVE(Edge.prop1) AND IS_PRIMITIVE(Edge.prop2)
-- </Projections-4>

-- (Similar to Projections-3 and Projections-4, more specific result set)
-- <Projections-5>
SELECT Consumer.name AS consumerName, Factory
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name)
-- </Projections-5>

-- (Shows a query process that can be simplified using the IN operator)
-- <INOperator-without>
SELECT Floor
FROM DIGITALTWINS Building
JOIN Floor RELATED Building.contains
WHERE Building.$dtId = @buildingId
-- </INOperator-without>

-- (How to get the results of INOperator-without process with the IN operator)
-- <INOperator-with>
SELECT Room
FROM DIGITALTWINS Floor
JOIN Room RELATED Floor.contains
WHERE Floor.$dtId IN ['floor1','floor2', ..'floorn']
AND Room. Temperature > 72
AND IS_OF_MODEL(Room, 'dtmi:com:contoso:Room;1')
-- </INOperator-with>

-- (A query example)
-- <OtherExamples-1>
SELECT device
FROM DigitalTwins space
JOIN device RELATED space.has
WHERE space.$dtid = 'Room 123'
AND device.$metadata.model = 'dtmi:contoso:com:DigitalTwins:MxChip:3'
AND has.role = 'Operator'
-- </OtherExamples-1>

-- (A query example)
-- <OtherExamples-2>
SELECT Room
FROM DIGITALTWINS Room
JOIN Thermostat RELATED Room.Contains
WHERE Thermostat.$dtId = 'id1'
-- </OtherExamples-2>

-- (A query example)
-- <OtherExamples-3>
SELECT Room
FROM DIGITALTWINS Floor
JOIN Room RELATED Floor.Contains
WHERE Floor.$dtId = 'floor11'
AND IS_OF_MODEL(Room, 'dtmi:contoso:com:DigitalTwins:Room;1')
-- </OtherExamples-3>

-------------- Used in how-to-use-tags
-- QueryMarkerTags-1 from above

-- (Shows property query that includes two marker tags)
-- <QueryMarkerTags-2>
SELECT * FROM digitaltwins WHERE NOT is_defined(tags.red) AND is_defined(tags.round)
-- </QueryMarkerTags-2>

-- (Shows property query that includes one marker and one value tag)
-- <QueryMarkerValueTags>
SELECT * FROM digitaltwins WHERE NOT is_defined(tags.red) AND tags.size = 'small'
-- </QueryMarkerValueTags>

-------------- Used in tutorial-end-to-end
-- GetAllTwins from above

-------------- Used in quickstart-adt-explorer
-- (A query example used for the quickstart to observe changes in twin properties)
-- <TemperatureQuery>
SELECT * FROM DigitalTwins T WHERE T.Temperature > 75
-- </TemperatureQuery>