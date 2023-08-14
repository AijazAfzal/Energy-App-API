Feature: GetDepartmentListQueryHandler

Test for retreiving Department List

@tag1
Scenario: When Company is not null
	Given the command to get all departments
	When  the command is handled to get all departments
	Then  departments list is retreived 
