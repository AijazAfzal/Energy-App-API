Feature: AddDepartmentCommandhandler

Unit Test for checking Add Department fuctionality

@tag1
Scenario: When Company Id is not null 
	Given the command to add department
	When the command is handled to add department
	Then the department is added sucessfully 
