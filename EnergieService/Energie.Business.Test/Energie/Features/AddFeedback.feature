Feature: AddFeedback

Unit test for Feedback

@tag1
Scenario: Feedback added successfully
	Given The command to add feedback
	When  The command is handled to add feedback successfully
	Then  Feedback added successfully

Scenario: Feedback will not add when rating is already exist
	Given The command to check the rating exist or not
	When  The command is handled to check the rating exist or not
	Then  If exist then give response message rating is already exist

