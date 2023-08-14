Feature: CompanyCategoryListQueryHandler

Test for retrieving list of Company Help Categories

@tag1
Scenario: Company Help categories retrieved sucessfully
	Given the command to get the  Company Help categories  
	When the command is handled to get Company Help categories 
	Then Company Help categories list is retrieved
