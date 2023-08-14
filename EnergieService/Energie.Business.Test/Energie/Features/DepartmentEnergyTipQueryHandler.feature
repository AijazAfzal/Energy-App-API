Feature: DepartmentEnergyTipQueryHandler

Test to retrieve list of   Energy Tip by department

@tag1
Scenario: Energy Tip List retreived sucessfully
	Given the command to get Energy Tip List
	When the command is handled to get Energy Tipp List
	Then the list of Energy Tip is retrieved
	
Scenario: Translated energy Tip List retreived sucessfully
	Given The command to get translated Energy Tip List
	When The command is handled to get translated Energy Tipp List
	Then The list of translated Energy Tip is retrieved
