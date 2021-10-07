-------------- Used in reference-query-clause-from.md
-- <FromDigitalTwinsSyntax>
--SELECT ...
FROM DIGITALTWINS
-- </FromDigitalTwinsSyntax>

-- <FromDigitalTwinsNamedSyntax>
--SELECT ...
FROM DIGITALTWINS <collection-name>
-- </FromDigitalTwinsNamedSyntax>

-- <FromDigitalTwinsExample>
SELECT *
FROM DIGITALTWINS
-- </FromDigitalTwinsExample>

-- <FromDigitalTwinsNamedExample>
SELECT *
FROM DIGITALTWINS T
-- </FromDigitalTwinsNamedExample>

-- <FromRelationshipsSyntax>
--SELECT ...
FROM RELATIONSHIPS
-- </FromRelationshipsSyntax>

-- <FromRelationshipsNamedSyntax>
--SELECT ...
FROM RELATIONSHIPS <collection-name>
-- </FromRelationshipsNamedSyntax>

-- <FromRelationshipsExample>
SELECT *
FROM RELATIONSHIPS
-- </FromRelationshipsExample>

-- <FromRelationshipsFilteredExample>
SELECT *
FROM RELATIONSHIPS
WHERE $sourceId IN  ['A', 'B', 'C', 'D']
-- </FromRelationshipsFilteredExample>

-- <FromNegativeExample>
SELECT * 
FROM (SELECT * FROM DIGITALTWINS T WHERE ...)
-- </FromNegativeExample>

-------------- Used in reference-query-clause-join.md
-- <JoinSyntax>
--SELECT ...
FROM DIGITALTWINS <twin-collection-name>
JOIN <target-twin-collection-name> RELATED <twin-collection-name>.<relationship-name> <OPTIONAL: relationship-collection-name>
WHERE <twin-collection-name-OR-target-twin-collection-name>.$dtId = '<twin-id>'
-- </JoinSyntax>

-- <JoinExample>
SELECT T, CT
FROM DIGITALTWINS T
JOIN CT RELATED T.contains
WHERE T.$dtId = 'ABC'
-- </JoinExample>

-- <MultiJoinSyntax>
--SELECT ...
FROM DIGITALTWINS <twin-collection-name>
JOIN <relationship-collection-name-1> RELATED <twin-collection-name>.<relationship-type-1>
JOIN <relationship-collection-name-2> RELATED <twin-or-relationship-collection-name>.<relationship-type-2>
-- </MultiJoinSyntax>

-- <MultiJoinExample>
SELECT LightBulb
FROM DIGITALTWINS Room
JOIN LightPanel RELATED Room.contains
JOIN LightBulb RELATED LightPanel.contains
WHERE Room.$dtId IN ['room1', 'room2']
-- </MultiJoinExample>

-- <MaxJoinExample>
SELECT LightBulb
FROM DIGITALTWINS Building
JOIN Floor RELATED Building.contains
JOIN Room RELATED Floor.contains
JOIN LightPanel RELATED Room.contains
JOIN LightBulbRow RELATED LightPanel.contains
JOIN LightBulb RELATED LightBulbRow.contains
WHERE Buliding.$dtId = 'Building1'
-- </MaxJoinExample>

-- <NoOuterJoinExample>
SELECT Building, Floor
FROM DIGITALTWINS Building
JOIN Floor RELATED Building.contains
WHERE Building.$dtId = 'Building1'
-- </NoOuterJoinExample>

-------------- Used in reference-query-clause-match.md
-- <MatchSyntax>
--SELECT ...
-- FROM ...  
MATCH <condition> 
WHERE <twin-collection-name-OR-target-twin-collection-name>.$dtId = '<twin-ID>' 
-- AND ... 
-- </MatchSyntax>

-- <MatchExample1>
SELECT building, sensor FROM DIGITALTWINS MATCH (building-[]-> (sensor) 
WHERE building.$tdid= 'Building21' AND T.temp > 50  
-- <MatchExample1>
 
-- <MatchExample2>
SELECT t, c, r FROM DIGITALTWINS   
MATCH (t)-[r:contains|isAssociatedWith]-(c)  
WHERE t. $dtid = 'ABC' AND c.humidity > 70 AND r.length = 10 
-- </MatchExample2>

-- <MatchWithRelationshipDirectionExampleLR>
SELECT T, C FROM DIGITALTWINS MATCH (T-[]-> (C) 
WHERE T.temp > 50 AND C.$dtid = 'ABC'  
-- </MatchWithRelationshipDirectionExampleLR>

-- <MatchWithRelationshipDirectionExampleND>
SELECT t, c FROM DIGITALTWINS MATCH (t)-[]-(c) 
WHERE t.$tdid ='ABC'  AND c.humidity > 70 
-- </MatchWithRelationshipDirectionExampleND>

-- <MatchWithRelationshipDirectionExampleRL>
SELECT T, C FROM DIGITALTWINS MATCH (T<-[]- (C) 
WHERE C.$dtid = 'ABC' AND T.temp > 50  
-- </MatchWithRelationshipDirectionExampleRL>

-- <MatchWithRelationshipPropertiesExample>
SELECT t, c, r FROM DIGITALTWINS   
MATCH (t)-[r:contains|isAssociatedWith]-(c)  
WHERE t.$dtId = ‘thermostat-15’ AND c.humidity > 70 AND r.length = 10 
-- </MatchWithRelationshipPropertiesExample>

-- <BidirectionalMatchExample>
SELECT t1, t2, c FROM DIGITALTWINS    
MATCH (t1)-[contains|isAssociatedWith*3..5]->(c)<-[has|includes*2..3]-(t2)  
WHERE t.$dtId = ‘thermostat-15’  AND c.humidity > 70 AND t2.temp = 55  
-- </BidirectionalMatchExample>

-------------- Used in reference-query-clause-select.md
-- <SelectSyntax>
SELECT *
--FROM ...
-- </SelectSyntax>

-- <SelectExample>
SELECT *
FROM DIGITALTWINS
-- </SelectExample>

-- <SelectProjectCollectionSyntax>
SELECT <twin-or-relationship-collection>
-- </SelectProjectCollectionSyntax>

-- <SelectProjectPropertySyntax>
SELECT <twin-or-relationship-collection>.<property-name>
-- </SelectProjectPropertySyntax>

-- <SelectProjectCollectionExample>
SELECT T
FROM DIGITALTWINS T
-- </SelectProjectCollectionExample>

-- <SelectProjectJoinExample>
SELECT Consumer, Factory, Relationship
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.consumerRelationship Relationship
WHERE Factory.$dtId = 'FactoryA'
-- </SelectProjectJoinExample>

-- <SelectProjectPropertyExample>
SELECT Consumer.name, Relationship.managedBy
FROM DIGITALTWINS Factory
JOIN Consumer RELATED Factory.consumerRelationship Relationship
WHERE Factory.$dtId = 'FactoryA'
-- </SelectProjectPropertyExample>

-- <SelectProjectPropertyNotPresentExample>
SELECT name, age 
FROM DIGITALTWINS
-- </SelectProjectPropertyNotPresentExample>

-- <SelectCountSyntax>
SELECT COUNT()
-- </SelectCountSyntax>

-- <SelectCountExample>
SELECT COUNT()
FROM DIGITALTWINS
-- </SelectCountExample>

-- <SelectCountRelationshipsExample>
SELECT COUNT()
FROM RELATIONSHIPS
-- </SelectCountRelationshipsExample>

-- <SelectTopSyntax>
SELECT TOP(<number-of-return-items>)
-- </SelectTopSyntax>

-- <SelectTopExample>
SELECT TOP(5)
FROM DIGITALTWINS
-- </SelectTopExample>

-------------- Used in reference-query-clause-where.md
-- <WhereSyntax>
--SELECT ...
--FROM ...
WHERE <twin-or-relationship-collection>.<property> <operator> <value-to-compare>
-- </WhereSyntax>

-- <WhereFunctionSyntax>
--SELECT ...
--FROM ...
WHERE <function-with-Boolean-result>
-- </WhereFunctionSyntax>

-- <WhereExample>
SELECT *
FROM DIGITALTWINS T
WHERE T.$dtId = 'Room1'
-- </WhereExample>

-- <WhereFunctionExample>
SELECT *
FROM DIGITALTWINS
WHERE IS_OF_MODEL('dtmi:sample:Room;1')
-- </WhereFunctionExample>

-------------- Used in reference-query-functions.md
-- <EndsWithSyntax>
ENDSWITH(<string-to-check>,<ending-string>)
-- </EndsWithSyntax>

-- <EndsWithExample>
SELECT *
FROM DIGITALTWINS T
WHERE ENDSWITH(T.$dtId, '-small')
-- </EndsWithExample>

-- <IsDefinedSyntax>
IS_DEFINED(<property>)
-- </IsDefinedSyntax>

-- <IsDefinedExample>
SELECT *
FROM DIGITALTWINS
WHERE IS_DEFINED(Location)
-- </IsDefinedExample>

-- <IsOfModelSyntax>
IS_OF_MODEL(<twin-collection>,'<model-ID>', exact)
-- </IsOfModelSyntax>

-- <IsOfModelExample>
SELECT ROOM FROM DIGITALTWINS DT WHERE IS_OF_MODEL(DT, 'dtmi:example:room;1', exact)
-- </IsOfModelExample>

-- <IsBoolSyntax>
IS_BOOL(<expression>)
-- </IsBoolSyntax>

-- <IsBoolExample>
SELECT *
FROM DIGITALTWINS T
WHERE IS_BOOL( HasTemperature )
-- </IsBoolExample>

-- <IsBoolNotFalseExample>
SELECT *
FROM DIGITALTWINS T
WHERE IS_BOOL( HasTemperature ) AND HasTemperature != false
-- </IsBoolNotFalseExample>

-- <IsNumberSyntax>
IS_NUMBER(<expression>)
-- </IsNumberSyntax>

-- <IsNumberExample>
SELECT * 
FROM DIGITALTWINS 
WHERE IS_NUMBER( Capacity ) AND Capacity != 0
-- </IsNumberExample>

-- <IsStringSyntax>
IS_STRING(<expression>)
-- </IsStringSyntax>

-- <IsStringExample>
SELECT * 
FROM DIGITIALTWINS 
WHERE IS_STRING( Status ) AND Status != 'Completed'
-- </IsStringExample>

-- <IsNullSyntax>
IS_NULL(<expression>)
-- </IsNullSyntax>

-- <IsNullExample>
SELECT *
FROM DIGITALTWINS T
WHERE NOT IS_NULL(T.Temperature)
-- </IsNullExample>

-- <IsPrimitiveSyntax>
IS_PRIMITIVE(<expression>)
-- </IsPrimitiveSyntax>

-- <IsPrimitiveExample>
SELECT Factory.area
FROM DIGITALTWINS Factory
WHERE Factory.$dtId = 'ABC'
AND IS_PRIMITIVE(Factory.area)
-- </IsPrimitiveExample>

-- <IsObjectSyntax>
IS_OBJECT<expression>)
-- </IsObjectSyntax>

-- <IsObjectExample>
SELECT * 
FROM DIGITALTWINS 
WHERE IS_OBJECT( MapObject ) AND NOT IS_DEFINED ( MapObject.TemperatureReading )
-- </IsObjectExample>

-- <StartsWithSyntax>
STARTSWITH(<string-to-check>,<beginning-string>)
-- </StartsWithSyntax>

-- <StartsWithExample>
SELECT *
FROM DIGITALTWINS T
WHERE STARTSWITH(T.$dtId, 'area1-')
-- </StartsWithExample>

-------------- Used in reference-query-operators.md
-- <EqualityExample>
SELECT * 
FROM DIGITALTWINS DT
WHERE DT.Temperature = 80
-- </EqualityExample>

-- <ComparisonExample>
SELECT * 
FROM DIGITALTWINS DT
WHERE DT.Temperature < 80
-- </ComparisonExample>

-- <OrderedComparisonExample>
SELECT * 
FROM DIGITALTWINS DT
WHERE NOT DT.Temperature <= 80
-- </OrderedComparisonExample>

-- <InExample>
SELECT * 
FROM DIGITALTWINS DT
WHERE DT.owner IN ['John', 'Anil', 'Bailey', 'Alex']
-- </InExample>

-- <AndExample>
SELECT * 
FROM DIGITALTWINS DT
WHERE DT.Temperature < 80 AND DT.Humidity < 50
-- </AndExample>

-- <OrExample>
SELECT * 
FROM DIGITALTWINS DT
WHERE DT.Temperature < 80 OR DT.Humidity < 50
-- </OrExample>

-- <NotExample>
SELECT * 
FROM DIGITALTWINS DT
WHERE NOT DT.Temperature < 80
-- </NotExample>

-------------- Used in reference-query-reserved.md
-- <ReservedKeywordExample>
SELECT * FROM DIGITALTWINS WHERE [[Group]] = 'BravoTeam'
-- </ReservedKeywordExample>
