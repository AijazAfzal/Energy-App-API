Feature: DeleteDepartmentTipbyUserCommandHandler

Test for deleting department tip by user

@tag1
Scenario: Department tip sucessfully deleted
	Given the command to delete tip by user
	When  the command is handled to delete  tip
	Then  the tip is deleted sucessfully 
