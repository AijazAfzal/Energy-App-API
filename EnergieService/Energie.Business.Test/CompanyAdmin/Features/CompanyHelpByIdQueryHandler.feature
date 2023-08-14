Feature: CompanyHelpTipByIdQueryHandler

Test for retreiving Company HelpTip by Id

@tag1
Scenario: Company HelpTip is retrieved sucessfully
	Given the command to retreive  Company HelpTip
	When the command is handled to retreive  Company HelpTip
	Then Company HelpTip is retrieved sucessfully 
