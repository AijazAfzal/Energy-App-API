Feature: UpdateTipByUserCommandHandler

Test for updating tip 

@tag1
Scenario: Tip updated sucessfully by user
	Given the command to update tip
	When  the command is handled to update tipp
	Then  the tipp is updated sucessfully 
	
Scenario: Check tipupdatebyuser of UpdateTipByUserCommandHandler is null or not if null then return message Something went wrong
	Given The command to check tipupdatebyuser is null or not 
	When  The command is handled to check tipupdatebyuser is null or not
	Then  If tipupdatebyuser is null the return Something went wrong 
