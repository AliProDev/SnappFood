Feature: Login

@Login
Scenario: Check the different parts of the login module
	Given launch the application
	And Check Enter invalid Mobile and valid Verification Code
	| MobileNumber | VerificationCode |
	| 09378008209  | 123456           |
	When Check Enter valid Mobile and invalid Verification Code
	| MobileNumber | VerificationCode |
	| 09301941972  | 54321            |
	Then Enter valid Mobile and valid Verification Code
	| MobileNumber | VerificationCode |
	| 09301941972  | 12345            |