Feature: UpdateDepartmentTipCommandHandler

Unit Test for updating Department Tip

@tag1
Scenario: When Department Tip Id is not Null
	Given the command to update department tip
	When the command is handled to update department tip
	Then the department tip is updated 
