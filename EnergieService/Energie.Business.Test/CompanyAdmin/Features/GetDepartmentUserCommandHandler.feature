Feature: GetDepartmentUserCommandHandler

Tests for getting Department Users

@tag1
Scenario: Returns list of Users 
	Given  the command to get all users
	When   the command is handled to get all users
	Then   list of users are retrieved 
