Feature: DepartmentEmployerCategoryListQueryHandler

Test for retreiving Employer Help Categories

@tag1
Scenario: Employer Help Categories retrieved sucessfully
	Given the command to get employer help list
	When  the command is handled to get thee listt 
	Then  the list is returned 
