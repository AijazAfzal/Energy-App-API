Feature: DeleteEnergyTipCommandHandler

Test for testing Delete Energy Tip Functionality

@tag1
Scenario: When Energy Tip is deleted sucessfully
	Given the command to delete Energy Tip 
	When  the command is handled to delete Energy Tip
	Then  the Energy Tip is deleted sucessfully 
