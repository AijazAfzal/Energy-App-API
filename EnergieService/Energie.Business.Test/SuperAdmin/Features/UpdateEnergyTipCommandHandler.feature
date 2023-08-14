Feature: UpdateEnergyTipCommandHandler

Unit Test for updating Energy Tip

@tag1
Scenario: When Tip Id is not Null
	Given the command to update Energy Tip
	When  the command is handled to update Energy Tip
	Then  Energy Tip is updated sucessfully 
