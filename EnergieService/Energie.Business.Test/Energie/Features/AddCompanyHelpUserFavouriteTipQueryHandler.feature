Feature: AddCompanyHelpUserFavouriteTipQueryHandler

Unit test for AddCompanyHelpUserFavouriteTipQueryHandler

@tag1
Scenario: Add Company Help User Favourite Tip sucessfully
	Given Add CompanyHelp UserFavouriteTipQuery request
	When  The request is handled AddCompanyHelpUserFavouriteTipQuery 
	Then Company Help User Favourite Tip added sucessfully

Scenario: Check favourite tip is already exist or not
	Given  Execute UserFavouriteTipQuery Request To Check Tip is Already Exist or Not
	When   If UserFavouriteTip Exist then execute RemoveEmployerFavouriteHelpAsync
	Then   RemoveEmployerFavouriteHelpAsync Execute Successfully If Favourite Tip is Already Exist
