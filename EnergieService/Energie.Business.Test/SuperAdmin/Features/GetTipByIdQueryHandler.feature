Feature: GetTipByIdQueryHandler

Test to retrieve Energy Tip By Id

@tag1
Scenario: Energy Tip retrieved sucessfully 
	Given the command to get energy tip by Id 
	When the command is handled to get Energy Tip
	Then the Energy Tip is retireved sucessfully
