Feature:CompanyHelpTipListQueryHandler

Test for retreieving company help List

@tag1
Scenario:Company Help List retrieved sucessfully
	Given the command to get company help List
	When  the command is handled to get List 
	Then the company help List is retreived
