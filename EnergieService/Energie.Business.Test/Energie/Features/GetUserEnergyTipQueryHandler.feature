Feature: GetUserEnergyTipQueryHandler

Test to retrieve list of Energy Tips

@tag1
Scenario: Energy Tip List retrieved sucessfully
	Given the command to get Energy Tip Listt
	When  the command us handled to get the tip List
	Then  Energy Tip Listt is retrieved

Scenario: Translated Energy Tip List retrieved sucessfully
	Given The command to get translated Energy Tip Listt
	When  The command is handled to get the translated tip List
	Then  Translated Energy Tip Listt is retrieved
