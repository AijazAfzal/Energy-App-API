Feature: RemoveEnergyPlanCommandHandler

Test to remove Energy Plan

@tag1
Scenario: Energy Plan removed sucessfully
	Given the command to delete Energy Plan
	When  the command is handeld to remove Plan
	Then  the Plan is removed sucessfully 
