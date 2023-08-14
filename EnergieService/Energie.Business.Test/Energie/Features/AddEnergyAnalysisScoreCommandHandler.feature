Feature: AddEnergyAnalysisScoreCommandHandler

Test for adding Energy Analysis Score

@tag1
Scenario:  Energy Analysis Score added sucessfully
	Given  the command to add  Energy Analysis Score
	When   the command is handled to add  Energy Analysis Score
	Then   the Energy Analysis Score is added sucessfully 
