Feature: RemoveDepartmentFavouriteTipCommandHandler

Test for removing department favourite Tip

@tag1
Scenario: If tip added by SuperAdmin
    Given  the command to remove department favourite tip added by superAdmin 
	When  the command is handled to remove favourite tip
	Then  the tip is removed sucessfullyy 

Scenario: If tip added by CompanyAdmin
    Given the command to remove department favourite tip added by CompanyAdmin 
	When  the command is handled to remove favourite tipp
	Then  the tip is removed sucessfullyyy  

	Scenario: If tip added by User
    Given  the command to remove department favourite tip added by User 
	When  the command is handled to remove favourite tippp  
	Then  the tip is deleted sucessfullyy    
	 