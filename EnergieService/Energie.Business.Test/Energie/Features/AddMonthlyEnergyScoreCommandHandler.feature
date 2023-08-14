Feature: AddMonthlyEnergyScoreCommandHandler

Test for adding monthly Energy Score

@tag1
Scenario: Monthly Energy Score added sucessfully
	Given the command to add energy score
	When  the command is handled to add energy score
	Then  the energy score is added sucessfully 

@tag1
 Scenario: Monthly Energy Score add when existEnergyScore is not null 
	Given  The AddMonthlyEnergyScoreCommand to add energy when existEnergyScore is not null
	When   The AddMonthlyEnergyScoreCommand is handled to add energy score when existEnergyScore is not null
	Then   The MonthlyEnergyScore is added sucessfully when existEnergyScore is not null

@tag1
 Scenario: Befor add Monthly Energy Score Check Energy score already exist or Not for the month
	Given  The AddMonthlyEnergyScoreCommand to Check Energy score already exist or Not for the month
	When   The AddMonthlyEnergyScoreCommand is handled to Check Energy score already exist or Not
	Then   If exist return Energy score already exist or Not for the month


Scenario: Don't add monthly energy score when something went wrong
	Given The AddMonthlyEnergyScoreCommand input to Check everything is correct or not 
	When  The AddMonthlyEnergyScoreCommand is handled to Check given input is correct or not 
	Then  If something is wrong with the input then return message Something went wrong


