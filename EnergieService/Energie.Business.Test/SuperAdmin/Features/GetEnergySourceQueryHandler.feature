Feature: GetEnergySourceQueryHandler

Test for retrieving the EnergyAnalysis List

@tag1
Scenario: EnergyAnalysis List retrieved sucessfully
	Given the command to retrieve the EnergyAnalysis List
	When  the command is handled to get the list
	Then  the list is retrieved 
