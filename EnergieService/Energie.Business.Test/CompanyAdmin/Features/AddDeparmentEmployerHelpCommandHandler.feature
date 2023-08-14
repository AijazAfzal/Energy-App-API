Feature: AddDeparmentEmployerHelpCommandHandler

Test for adding department employer help

@tag1
Scenario: Employer help added sucessfully
	Given the command to add employer help
	When the command is handled to add employer help
	Then the employer help is added 
