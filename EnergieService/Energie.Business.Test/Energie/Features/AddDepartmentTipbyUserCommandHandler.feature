Feature: AddDepartmentTipbyUserCommandHandler

Test for adding department tip by user

@tag1
Scenario:Department Tip added sucessfully 
	Given  the command to add department tip by user
	When   the command is handled to add department tip
	Then   the department tip is added sucessfully 
