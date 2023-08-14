Feature: AddTipByUserCommandHandler

test for adding tip by user

@tag1
Scenario: Tip added sucessfully
	Given the command to add tip by user
	When  the command is handled to add tip
	Then tip is added sucessfully 
