Feature: UpdateCompanyHelpCommandHandler

Test for updating Company Help 

@tag1
Scenario: When Company Help  Id is not null
	Given the command to update company help 
	When the command is handled to update company help
	Then company help is updated sucessfully 
