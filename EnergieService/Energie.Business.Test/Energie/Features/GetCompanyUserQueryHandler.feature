Feature: GetCompanyUserQueryHandler

Unit test to retrieve company user

@tag1
Scenario: Company User retrieved sucesssfully
	Given the command to get company user
	When  the command is handled to get user
	Then  user is retrieved sucessfully 
