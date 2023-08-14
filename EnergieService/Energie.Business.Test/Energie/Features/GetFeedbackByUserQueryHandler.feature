Feature: GetFeedbackByUserQueryHandler

Unit test for get feedback given by user

@tag1
Scenario: Get feedback rating given by user
	Given The query  to get user rating 
	When  The query handler to handel get rating by user
	Then  The rating given by user get successfully
