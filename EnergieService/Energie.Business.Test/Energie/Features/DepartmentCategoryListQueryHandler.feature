Feature: DepartmentCategoryListQueryHandler

Test to retreive Department Category List

@tag1
Scenario: Department Category List retrieved sucessfully
	Given the command to retrieve category list
	When  the command is handled to get department Category List
	Then  list is retrived sucessfully 

Scenario: Translated Department Category List retrieved sucessfully
	Given The command to retrieve translated category list
	When  The command is handled to get translated department Category List
	Then  Translated list is retrived sucessfully 
