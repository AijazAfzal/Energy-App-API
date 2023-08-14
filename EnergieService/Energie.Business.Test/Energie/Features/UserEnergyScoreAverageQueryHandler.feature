Feature: UserEnergyScoreAverageQueryHandler

Writeing unit test for user energy score average

@tag1
Scenario: When UserEmail is null 
	Given the Query to retrieve all months and userEnergy score
	When the Query is handled to all months and userEnergy score
	Then userEnergy energy score is null

Scenario: when userEmail is not null
	Given the query to get all month and EnergyScore for user
	When the query is handled to all months and energyScore for user
	Then energyScore is not null

Scenario: when user has only One month score
	Given the query to get all month and one month EnergyScore for user
	When the query is handled to all months and energyScore for user
	Then userEnergy energy score is null

Scenario: when user has only two month score
	Given the query to get all month and two month EnergyScore for user
	When the query is handled to all months and energyScore for user
	Then energyScore is not null