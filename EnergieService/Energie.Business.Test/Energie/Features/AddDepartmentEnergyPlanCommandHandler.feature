Feature: AddDepartmentEnergyPlanCommandHandler

test for adding Energy Plan for Department

@tag1
Scenario: Super Admin Fav Tip added to Department Energy Plan
	Given the command to add SuperAdmin favtip to Energy Plan
	When  the command is handled to add Energy Plan
	Then  the Energy Plan is added sucessfully 

Scenario: CompanyAdmin fav tip added to plan
	Given the command to add CompanyAdmin favtip to Energy Plan
	When  the command is handled to add Energy Plann
	Then  the Energy Plan is added sucessfullyyy 

Scenario: User Fav tip added to Energy Plan 
	Given the command to add User favtip to Energy Plan
	When  the command is handled to addd Energy Plan
	Then  the Energy Plan iss added sucessfullyyy 

Scenario: Check EnergyPlan for SuperAdmin is exists or not
	Given The command to Check EnergyPlan for SuperAdmin is exists or not
	When  The command is handled to Check EnergyPlan for SuperAdmin is exists or not
	Then  If EnergyPlan is exist for SuperAdmin then response EnergyPlan already exists 

Scenario: Check EnergyPlan for CompanyAdmin is exists or not
	Given The command to Check EnergyPlan for CompanyAdmin is exists or not
	When  The command is handled to Check EnergyPlan for CompanyAdmin is exists or not
	Then  If EnergyPlan is exist for CompanyAdmin then response EnergyPlan already exists 

Scenario: Check EnergyPlan for User is exists or not
	Given The command to Check EnergyPlan for User is exists or not
	When  The command is handled to Check EnergyPlan for User is exists or not
	Then  If EnergyPlan is exist for User then response EnergyPlan already exists 

