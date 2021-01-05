-------------- Used in tutorial-end-to-end
-- <GetAllTwins> (Shows how to query all twins)
SELECT * 
FROM DIGITALTWINS
-- </GetAllTwins>

-------------- Used in how-to-query-graph
-- GetAllTwins from above

-- <QueryByProperty-1> (Shows basic property query)
SELECT  *
FROM DigitalTwins T  
WHERE T.firmwareVersion = '1.1'
AND T.$dtId in ['123', '456']
AND T.Temperature = 70
-- </QueryByProperty-1>

-- <QueryByProperty-2> (Shows property query based on whether property is defined)
SELECT *​ FROM DIGITALTWINS WHERE IS_DEFINED(Location)
-- </QueryByProperty-2>

-- <QueryMarkerTags-1> (Shows property query based on marker tags)
SELECT * FROM digitaltwins WHERE is_defined(tags.red)
-- </QueryMarkerTags-1>

-- <QueryByProperty-3> (Shows property query based on property type)
SELECT * FROM DIGITALTWINS​ T WHERE IS_NUMBER(T.Temperature)
-- </QueryByProperty-3>

-- <QueryByModel-1> (Shows model query with only twinTypeName parameter)
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:example:thing;1')
-- </QueryByModel-1>

-- <QueryByModel-2> (Shows model query with twinCollection and twinTypeName parameters)
SELECT * FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:thing;1')
-- </QueryByModel-2>

-- <QueryByModel-3> (Shows model query with twinTypeName and exact parameters)
SELECT * FROM DIGITALTWINS WHERE IS_OF_MODEL('dtmi:example:thing;1', exact)
-- </QueryByModel-3>

-- <QueryByModel-4> (Shows model query with twinCollection, twinTypeName, and exact parameters)
SELECT ROOM FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:thing;1', exact)
-- </QueryByModel-4>

-- <QueryByRelationship-1> (Shows basic relationship query)
SELECT T, CT
FROM DIGITALTWINS T
JOIN CT RELATED T.contains
WHERE T.$dtId = 'ABC'
-- </QueryByRelationship-1>

-- <QueryByRelationship-2> (Shows querying properties of relationship)
SELECT T, SBT, R
FROM DIGITALTWINS T
JOIN SBT RELATED T.servicedBy R
WHERE T.$dtId = 'ABC'
AND R.reportedCondition = 'clean'
-- </QueryByRelationship-2>

-- </QueryByRelationship-3> (Shows multiple relationship JOINs in a single query)
SELECT LightBulb
FROM DIGITALTWINS Room
JOIN LightPanel RELATED Room.contains
JOIN LightBulb RELATED LightPanel.contains
WHERE IS_OF_MODEL(LightPanel, 'dtmi:contoso:com:lightpanel;1')
AND IS_OF_MODEL(LightBulb, 'dtmi:contoso:com:lightbulb ;1')
AND Room.$dtId IN ['room1', 'room2']
-- </QueryByRelationship-3>

-- <SelectCount-1> (Shows basic use of COUNT)
SELECT COUNT()
FROM DIGITALTWINS
-- </SelectCount-1>

-- <SelectCount-2> (Shows basic use of COUNT with an added WHERE clause)
SELECT COUNT()
FROM DIGITALTWINS
WHERE IS_OF_MODEL('dtmi:sample:Room;1')

SELECT COUNT()
FROM DIGITALTWINS c
WHERE IS_OF_MODEL('dtmi:sample:Room;1') AND c.Capacity > 20
-- </SelectCount-2>

-- <SelectCount-3> (Shows COUNT being used with JOIN)
SELECT COUNT()  
FROM DIGITALTWINS Room  
JOIN LightPanel RELATED Room.contains  
JOIN LightBulb RELATED LightPanel.contains  
WHERE IS_OF_MODEL(LightPanel, 'dtmi:contoso:com:lightpanel;1')  
AND IS_OF_MODEL(LightBulb, 'dtmi:contoso:com:lightbulb;1')  
AND Room.$dtId IN ['room1', 'room2']
-- </SelectCount-3>

-- <SelectTop> (Shows basic use of SELECT TOP)
SELECT TOP (5)
FROM DIGITALTWINS
WHERE ...
-- </SelectTop>

-- <Projections-1> (Shows basic use of projections)
SELECT Consumer, Factory, Edge
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
-- </Projections-1>

-- <Projections-2> (Shows use of projection to return a twin's property)
SELECT Consumer.name
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Consumer.name)
-- </Projections-2>

-- <Projections-3> (Shows use of projection to return a relationship's property)
SELECT Consumer.name, Edge.prop1, Edge.prop2, Factory.area
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name) AND IS_PRIMITIVE(Edge.prop1) AND IS_PRIMITIVE(Edge.prop2)
-- </Projections-3>

-- <Projections-4> (Same as Projections-3 except adds aliases to result set)
SELECT Consumer.name AS consumerName, Edge.prop1 AS first, Edge.prop2 AS second, Factory.area AS factoryArea
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name) AND IS_PRIMITIVE(Edge.prop1) AND IS_PRIMITIVE(Edge.prop2)
-- </Projections-4>

-- <Projections-5> (Similar to Projections-3 and Projections-4, more specific result set)
SELECT Consumer.name AS consumerName, Factory
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.customer Edge
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area) AND IS_PRIMITIVE(Consumer.name)
-- </Projections-5>

-- <INOperator-without> (Shows a query process that can be simplified using the IN operator)
SELECT Floor
FROM DIGITALTWINS Building
JOIN Floor RELATED Building.contains
WHERE Building.$dtId = @buildingId
-- </INOperator-without>

-- <INOperator-with> (How to get the results of INOperator-without process with the IN operator)
SELECT Room
FROM DIGITALTWINS Floor
JOIN Room RELATED Floor.contains
WHERE Floor.$dtId IN ['floor1','floor2', ..'floorn']
AND Room. Temperature > 72
AND IS_OF_MODEL(Room, 'dtmi:com:contoso:Room;1')
-- </INOperator-with>

-- <OtherExamples-1> (A query example)
SELECT device
FROM DigitalTwins space
JOIN device RELATED space.has
WHERE space.$dtid = 'Room 123'
AND device.$metadata.model = 'dtmi:contoso:com:DigitalTwins:MxChip:3'
AND has.role = 'Operator'
-- </OtherExamples-1> 

-- <OtherExamples-2> (A query example)
SELECT Room
FROM DIGITALTWINS Room
JOIN Thermostat RELATED Room.Contains
WHERE Thermostat.$dtId = 'id1'
-- </OtherExamples-2>

-- <OtherExamples-3> (A query example)
SELECT Room
FROM DIGITALTWINS Floor
JOIN Room RELATED Floor.Contains
WHERE Floor.$dtId = 'floor11'
AND IS_OF_MODEL(Room, 'dtmi:contoso:com:DigitalTwins:Room;1')
-- </OtherExamples-3>

-------------- Used in how-to-use-tags
-- QueryMarkerTags-1 from above

-- <QueryMarkerTags-2> (Shows property query that includes two marker tags)
SELECT * FROM digitaltwins WHERE NOT is_defined(tags.red) AND is_defined(tags.round)
-- </QueryMarkerTags-2>

-- <QueryMarkerValueTags> (Shows property query that includes one marker and one value tag)
SELECT * FROM digitaltwins WHERE NOT is_defined(tags.red) AND tags.size = 'small'
-- </QueryMarkerValueTags>

-------------- Used in tutorial-end-to-end
-- GetAllTwins from above

-------------- Used in quickstart-adt-explorer
-- <TemperatureQuery> (A query example used for the quickstart to observe changes in twin properties)
SELECT * FROM DigitalTwins T WHERE T.Temperature > 75
-- </TemperatureQuery>