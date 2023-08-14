Feature: AddUserFavouriteTipQueryHandler

Test to retrieve User favourite tip 

@tag1
Scenario: User favourite tip  retrieved sucessfully
	Given the command to set user favourite tip
	When  the command is handled to set and return a response message
	Then  User favourite tip is added and response message is returned

Scenario: Check User favourite tip is exist or not
	Given The command to check user favourite tip is exist or not
	When  The command is handled to Check user favourite tip is exist or not
	Then  If exist return resopnse favourite Tip already exist