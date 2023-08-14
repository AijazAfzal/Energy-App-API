Feature: GetEnergyAnalysisByIdQueryHandler

Test to retrieve Energy Analysis Questions By Id

@tag1
Scenario: Energy Analysis Questions List retreived sucessfully
	Given the command to get Energy Analysis Questions List by Id
	When the command is handled to retrieve the list 
	Then the Energy Analysis Questions By Id is retreived

Scenario: Translated Energy Analysis Questions List retreived sucessfully
	Given The command to get translated Energy Analysis Questions List by Id
	When The command is handled to retrieve the translated list 
	Then The translated Energy Analysis Questions By Id is retreived
