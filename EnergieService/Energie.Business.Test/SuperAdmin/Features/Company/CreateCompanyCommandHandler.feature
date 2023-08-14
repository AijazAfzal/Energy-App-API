Feature: CreateCompanyCommandHandler
  A short summary of the feature

  @tag1
  Scenario: Creating a New Company
    Given a company repository
    And a command handler
    And the command to add company with name "New Company"
    And no company with name "New Company" exists
    When the command is handled to add company
    Then a success response is returned
    And the success message should contain the company ID

  @tag2
  Scenario: Creating an Existing Company
    Given a company repository
    And a command handler
    And the command to add company with name "Existing Company"
    And a company with name "Existing Company" already exists
    When the command is handled to add company
    Then an error response is returned
    And the error message should indicate that the company already exists
