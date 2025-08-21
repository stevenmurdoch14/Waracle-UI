Feature: User Login
  As a user
  I want to login to the online shop
  So that I can access the inventory or see error messages

  @standard
  Scenario: Standard user can login successfully
    Given I navigate to the login page
    When I login with username "standard_user" and password "secret_sauce"
    Then I should be redirected to the inventory page     
    
  @lockedout
  Scenario: Locked out user cannot login
    Given I navigate to the login page
    When I login with username "locked_out_user" and password "secret_sauce"
    Then I should see an error message "Sorry, this user has been locked out."

  @problem
  Scenario: Problem user does not see broken images
    Given I login as "problem_user"
    Then I should see the appropriate image displayed against Sauce Labs BackPack

  @performance
  Scenario: Performance glitch user experiences slow loading
    Given I login as "performance_glitch_user"
    When the inventory page loads successfully
    Then I can navigate to the backpack page without performance delays over 6 seconds
