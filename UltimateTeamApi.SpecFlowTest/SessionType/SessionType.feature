Feature: SessionType
	Get a SessionType

# There are some instructions in the file "SessionTypeSteps.cs"

@mytag
#1 - Get all session types
Scenario: 1. The person wants to see all session types options
	When the person goes to Create Session Page, session types options list should return
		| Id | Type             |
		| 1  | Collaborative    |
		| 2  | Laser Individual |


#2 - Get session type by id
Scenario: 2. The person wants to see a session type details
	When the persom select session type with id 1
	Then session type details should be
		| Id | Type             |
		| 1  | Collaborative    |