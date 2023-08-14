Feature: GetLanguagesListQueryHandler

Unit test for retrieving list of languages

@tag1
Scenario: List of languages retrieved sucessfully
	Given the command to get list of languages
	When  the command is handled 
	Then  languages are retrieved sucessfully 
