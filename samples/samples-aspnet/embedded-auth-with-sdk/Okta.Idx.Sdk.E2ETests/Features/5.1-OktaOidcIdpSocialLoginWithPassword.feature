﻿Feature: 5.1: Direct Auth Social Login with 1 Social IDP

 Background:
	Given a SPA, WEB APP or MOBILE Policy that defines Password as the only factor required for authentication
	And a configured IDP object for Okta
	And an IDP routing rule defined to allow users in the Sample App to use the IDP
	And a user named Mary does not have an account in the org but has an Okta OIDC IDP account
  
  @Ignore
  Scenario: 5.1.1: Mary Logs in with Social IDP
	Given Mary navigates to the Login View
	When she clicks the Login with Okta button
	And logs in to Okta
	Then she is redirected to the Root View
    And she sees a table with her profile info
    And the cell for the value of email is shown and contains her email