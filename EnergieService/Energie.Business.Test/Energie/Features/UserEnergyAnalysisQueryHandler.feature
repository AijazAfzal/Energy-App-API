Feature: UserEnergyAnalysisQueryHandler

Test to retrieve User Energy Analyis

@tag1
Scenario: User Energy Analysis list retrieved sucessfully
	Given the command to get user enrgyanalysis list
	When the command is handled to gett the listt
	Then the list is retrieved sucessfully 

Scenario: Translated User Energy Analysis list retrieved sucessfully
	Given The command to get user translated enrgyanalysis list
	When The command is handled to get the translated list
	Then The translated list is retrieved sucessfully 


