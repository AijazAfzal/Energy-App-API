Feature: DeleteUserCommandHandler

Test for deleting User 

@tag1
Scenario: When User Id is not Null
	Given the command to delete User
	When  the command is handled to delete User
	Then User deleted sucessfully 
