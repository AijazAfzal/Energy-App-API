Feature: AddDepartmentFavouriteTipCommandHandler

Writing unit test for AddDepartmentFavouriteTipCommandHandler

Scenario: When userId and UserEmail is not null 
	Given AddDepartmentFavouriteTipCommand request 
	When  the command is handled to AddUserFavouriteDepartmentTipAsync 
	Then  the favourites tips added sucessfully

Scenario: Removed Department favourite tip when existuserDepartmentTip is not null 
	Given Department Favourite Tip Command request 
	When  The command is handled to Removed Department favourite tip when existuserDepartmentTip is not null
	Then  Department Favourite Tip Removed Successfully