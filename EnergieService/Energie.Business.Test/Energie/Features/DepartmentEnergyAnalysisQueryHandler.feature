Feature: DepartmentEnergyAnalysisQueryHandler

Unit test for DepartmentEnergyAnalysisQueryHandler

@tag1
Scenario:Retrieving Department Energy Analysis When activeusers is less than 5
	Given Department EnergyAnalysis Query to retrive DepartmentEnergyAnalysis
	When  Department EnergyAnalysis Query is handled to retrive DepartmentEnergyAnalysis
	Then  Department EnergyAnalysis retrive successfully
