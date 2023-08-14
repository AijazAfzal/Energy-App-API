Feature: DepartmentTipListQueryHandler

Test for retrieving Department Tip List

@tag1
Scenario: Department Tip List retreived sucessfully
	Given the command to retrieve Department Tip List 
	When  the command is handled to get the Department Tip List
	Then  the department tip list is retrieved 
