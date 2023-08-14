Feature: DeleteDepartmentTipCommandHandler

Test for testing Delete Department Tip Functionality

@tag1
Scenario: When department tip is not null
	Given the command to delete department tip
	When  the command is handled to delete department tip
	Then  the department tip is deleted sucessfully
