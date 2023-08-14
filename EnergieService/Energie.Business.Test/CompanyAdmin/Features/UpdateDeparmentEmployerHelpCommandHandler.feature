Feature: UpdateDeparmentEmployerHelpCommandHandler

Test fir updating Department Employer Help

@tag1
Scenario: Employer Help updated sucessfully
	Given the command to update employer help
	When  the command is handled to update employer help
	Then  the employer help is updated sucessfully 
