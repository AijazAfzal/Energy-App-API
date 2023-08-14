Feature: DepartmentFavouriteTipListQueryHandler

Test to retrieve department favourite tips

@tag1
Scenario: Department Favourite Tip List retreived
	Given the command to get Department Favourite Tip List
	When  the command is handled to get list
	Then  the tip list is retrieved 

Scenario: Translated department Favourite Tip List retreived
	Given The command to get translated Department Favourite Tip List
	When  The command is handled to get translated list
	Then  The translated tip list is retrieved 
