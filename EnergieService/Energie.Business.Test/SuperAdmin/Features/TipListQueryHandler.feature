Feature: TipListQueryHandler

Test to retrieve Energy Tip List

@tag1
Scenario: Energy Tip List retrieved sucessfully
	Given the command to retreive Energy Tipp List
	When  the command is handled to get Energy Tip List
	Then Energy Tip List is retrieved 
