Feature: SessionType
	Get a SessionType

# There are some instructions in the file "SessionTypeSteps.cs"

@mytag
#1 - Get all session types
Scenario: 1. The user wants to see all session types options
	When the user goes to Create Session Page, session types options list should return
		| Id | Type             |
		| 1  | Collaborative    |
		| 2  | Laser Individual |


#2 - Get session type by id
Scenario: 2. The user wants to see a session type details
	When the user select session type with id 1
	Then session type details should be
		| Id | Type             |
		| 1  | Collaborative    |