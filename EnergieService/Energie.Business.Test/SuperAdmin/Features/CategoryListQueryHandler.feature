Feature: CategoryListQueryHandler

Test for retreiving Category List

@tag1
Scenario:Category List Retreived sucessfully
	Given the command to get category list
	When  the command is handled to get category list
	Then  the category list is retrieved 

Scenario:Translated Category List Retreived sucessfully
	Given The command to get translated category list
	When  The command is handled to get translated category list
	Then  The translated category list is retrieved 
