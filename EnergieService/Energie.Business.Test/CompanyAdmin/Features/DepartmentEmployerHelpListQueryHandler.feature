Feature: DepartmentEmployerHelpListQueryHandler

Test to retrieve Department Employer Help List

@tag1
Scenario: Employer Help List retrieved sucessfully
	Given the command to retrieve employer help list
	When  the command is handled to get the listt 
	Then  the list is retirevd sucesssfully 
