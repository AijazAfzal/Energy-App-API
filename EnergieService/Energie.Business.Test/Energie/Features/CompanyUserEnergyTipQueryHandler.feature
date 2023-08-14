Feature: CompanyUserEnergyTipQueryHandler

Test to retrieve Compnay Helps by Category Id

@tag1
Scenario: Compnay Helps retrieved sucessfully
	Given the command to get the company helps
	When  the command is handled to get company helps
	Then  the company helps are retrieved
	
Scenario: Translated Compnay Helps retrieved sucessfully
    Given The command to get the translated company helps
    When  The command is handled to get translated company helps
    Then  The translated company helps are retrieved
