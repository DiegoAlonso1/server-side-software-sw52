Feature: SessionStadistic
	Simple calculator for adding two numbers

#There are some instructions in the file "SessionStadistic.feature"

@mytag
Scenario: 0. Initialize some instances
	When session required attributes provided to initialize instance
		| Name            | SessionTypeId |
		| UltimateTeam    | 1             |
		| Clase de fisica | 2             |
	Then assign the session with Id 1 with the functionality with Id 1
	Then assign the session with Id 1 with the functionality with Id 2
	Then assign the session with Id 1 with the functionality with Id 3
	Then assign the session with Id 2 with the functionality with Id 2
	Then assign the session with Id 2 with the functionality with Id 4


#1 - Get all session stadistics by session id
Scenario: 1. The administrator wants see the session stadistics of a session
	When the administrator goes to Functionality Usage Page on session with id 1, the session stadistics list should return
		| SessionId | FunctionalityId | Count |
		| 1         | 1               | 1     |
		| 1         | 2               | 1     |
		| 1         | 3               | 1     |


#2 - Assign session stadistic
Scenario: 2. The administrator wants to see session stadistics
	When the person uses the functionality with id 5 in the session with id 1, session stadistics details should be
		| SessionId | FunctionalityId | Count |
		| 1         | 5               | 1     |
