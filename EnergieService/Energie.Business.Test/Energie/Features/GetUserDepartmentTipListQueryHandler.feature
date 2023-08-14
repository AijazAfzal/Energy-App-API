Feature: GetUserDepartmentTipListQueryHandler

Test to retrieve Department Tip List of User

@tag1
Scenario: Department Tip List retrievd sucessfully
	Given the command to get the department tip list
	When the command is handled to get ther department tip list 
	Then tip list is retrieved 
