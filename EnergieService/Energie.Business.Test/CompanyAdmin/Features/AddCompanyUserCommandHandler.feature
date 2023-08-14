Feature: AddCompanyUserCommandHandler

 Should be able to add company user

@tag1
Scenario: Company User added sucessfully
	Given the command to add company user
	When  the command is handled to add company user
	Then company user is added sucessfully 

