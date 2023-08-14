Feature: GetAllEnergyAnalysisQueryHandler

Test to rerieve Energy Analysis Questions

@tag1
Scenario: Energy Analysis Questions retrieved sucessfully
	Given the command to get all energy analysis questions
	When the command is handled to get the list of questions
	Then the list of energy analysis questions is retrieved 

Scenario: Translated Energy Analysis Questions retrieved sucessfully
	Given The command to get all translated energy analysis questions
	When The command is handled to get the translated list of questions
	Then The translated list of energy analysis questions is retrieved 
