Feature: RemoveUserAccountCommandHandler

Unit Test for removing Energy App USer

@tag1
Scenario: Energy App User removed sucessfully
	Given the command to delete Energy App User
	When  the command is handled to delete account 
	Then  the user account is deleted sucessfully 
