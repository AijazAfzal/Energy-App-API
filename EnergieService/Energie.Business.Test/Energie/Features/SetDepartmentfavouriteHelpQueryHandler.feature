Feature: SetDepartmentfavouriteHelpQueryHandler

Test for Adding Department Favourite Help

@tag1
Scenario: Favouirite Help added sucessfully
	Given the command to add favourite help
	When the command is handled to add help
	Then help is added sucessfully 

Scenario: FavouriteHelp removed sucessfully
	Given The command to remove favourite help
	When  The command is handled to remove favourite help
	Then  Favourite Help is removed sucessfully

