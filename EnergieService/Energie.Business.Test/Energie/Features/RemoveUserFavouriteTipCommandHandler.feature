Feature: RemoveUserFavouriteTipCommandHandler

Test for removing User favourite tip

@tag1
Scenario: Removing Super Admin Favourite Tip
	Given the command to remove Super Admin favourite tip
	When the command is handled to remove favaourite tip
	Then the favourite tip is removed 

@tag1
Scenario: Removing Employer Favourite Tip
	Given the command to remove Employer favourite tip
	When the command is handled to remove the favaourite tip
	Then the favourite tip is deleted

@tag1
Scenario: Removing User Favourite Tip
	Given the command to remove User favourite tip
	When the command is handled to remove user favaourite tip
	Then the favourite tip is removed sucessfully 

