Feature: CompanyListQueryHandler

Test for retreiving Company List

@tag1
Scenario: Company List is retrieved sucessfully
	Given the command to retreive company list
	When  the command is handled to get company list
	Then  the company list is retrieved 
