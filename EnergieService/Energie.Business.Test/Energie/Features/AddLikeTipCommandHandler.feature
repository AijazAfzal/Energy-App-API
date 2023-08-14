Feature: AddLikeTipCommandHandler

Unit test to add like tip command handlet

@tag1
Scenario: Add like tip sucessfully
	Given The command to add like tip
	When The command is handled to add like tip
	Then Like tip added sucessfully

Scenario: RemoveLikeTip when departmentTip is not null
	Given The command to check departmentTip is null or not
	When  The command is handled to check departmentTip is null or not
	Then  Remove Like Tip sucessfully

