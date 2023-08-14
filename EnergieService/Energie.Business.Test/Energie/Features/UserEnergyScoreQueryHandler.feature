Feature: UserEnergyScoreQueryHandler

Writeing unit test for user energy score

@tag1
Scenario: When UserEmail is null 
	Given the Query to retrieve energy score
	When the Query is handled to get energy score
	Then energy score is null 

Scenario: When UserEmail is not null 
	Given the Query to retrieve months and user energy score
	When the Query is handled to get months and user energy score
	Then energy score is not null 