Feature: UserTrendScoreQueryHandler

Writeing unit test for user trend score

@tag1
Scenario: when UserEmail is null
	Given the Query to retrieve energy score for trend
	When the Query is handled to get energy score for trend
	Then energy score is null for trend

Scenario: when UserEmail is not null
	Given the Query to retrieve trend score
	When the Query is handled to get energy score for trend with user Email
	Then trend score is not null for trend

Scenario: when user has only one energy score
	Given the Query to retrieve energy score for one month
	When the Query is handled to get energy score for trend with user Email
	Then energy score is null for trend

Scenario: when user has only two energy score
	Given the Query to retrieve energy score for two month
	When the Query is handled to get energy score for trend with user Email
	Then energy score is null for trend