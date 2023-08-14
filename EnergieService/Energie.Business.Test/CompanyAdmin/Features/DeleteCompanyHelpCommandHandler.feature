Feature: DeleteCompanyHelpCommandHandler

Test for deleting Company Help 

@tag1
Scenario: When Company Help is not Null
	Given the command to delete company help 
	When  the command is handled to delete company help 
	Then  company help deleted sucessfully 
