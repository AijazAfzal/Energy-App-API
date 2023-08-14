Feature: UpdateDepartmentTipbyUserCommandHandler

Test for updating department tip by user

@tag1
Scenario: Tip updated sucessfully   
	Given the command to update department tipp 
	When  the command is handled to update tip
	Then  the tip is updated sucessfully 

Scenario: If tiptobeupdated is null the return something failed
	Given The command to check tiptobeupdated is null or not
	When  The command is handled check tiptobeupdated is null or not
	Then  If null then return message something failed
