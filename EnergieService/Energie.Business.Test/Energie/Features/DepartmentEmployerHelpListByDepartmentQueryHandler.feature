Feature: DepartmentEmployerHelpListByDepartmentQueryHandler

Test for retreiving department Employer Help List

@tag1
Scenario: Department Employer Help List retrieved sucessfully
	Given the command to get department employer help list
	When  the command is handled to get help list
	Then  the help list is retrieved sucessfully  

Scenario: Translated Department Employer Help List retrieved sucessfully
	Given The command to get translated department employer help list
	When  The command is handled to get translated help list
	Then  The translated help list is retrieved sucessfully  
