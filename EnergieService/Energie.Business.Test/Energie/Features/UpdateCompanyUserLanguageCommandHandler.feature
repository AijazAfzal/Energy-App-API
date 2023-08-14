Feature: UpdateCompanyUserLanguageCommandHandler

Unit test for updating user language

@tag1
Scenario: User language updated sucessfully
	Given the command to update user language
	When  the command is handled to update user language
	Then  the user language is updated sucessfully 
