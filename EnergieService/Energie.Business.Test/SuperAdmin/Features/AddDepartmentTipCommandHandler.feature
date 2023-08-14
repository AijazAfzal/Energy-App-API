Feature: AddDepartmentTipCommandHandler

Test for addding Department Tip

@tag1
Scenario: When departmentTipId is not null
	Given the command to add department tip
	When  the command is is handled to add department tip
	Then  department tip is added sucessfully 
