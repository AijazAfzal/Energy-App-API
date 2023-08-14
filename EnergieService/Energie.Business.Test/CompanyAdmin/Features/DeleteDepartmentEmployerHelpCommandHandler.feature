Feature: DeleteDepartmentEmployerHelpCommandHandler
Test for deleting employer help
@tag1
Scenario: Department Employer Help deleted sucessfully
	Given the command to delete employer help
	When the command is handled to delete help
	Then employer help is deleted sucessfully 
