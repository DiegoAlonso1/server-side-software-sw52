Feature: Session
	Create, Update, and Get a Session

# There are some instructions in the file "SessionSteps.cs"

@mytag
Scenario: 0. Initialize some Session Intances
	When sessions required attributes provided to initialize instances
		| Name            | SessionTypeId |
		| UltimateTeam    | 1             |
		| Clase de fisica | 2             |
		| Reunión         | 1             |
		| Hola que hace   | 2             |


#1 - Create a session
Scenario: 1. The person wants to create a session
	When the person complete the form with the required fields and click the Create Session button
		| Name            | SessionTypeId |
		| Nueva Sesion    | 2             |


#2 - Update a session
Scenario: 2. The person wants to update their session data
	When the person complete the form with required fields of the session with id 5 and click the Update Session button
		| Name            | SessionTypeId |
		| Session SSS     | 2             |


#3 - Get all sessions
Scenario: 3. The administrator wants to see all sessions
	When the administrator goes to Sessions Page, session list should return
		| Id | Name            | SessionTypeId |
		| 1  | UltimateTeam    | 1             |
		| 2  | Clase de fisica | 2             |
		| 3  | Reunión         | 1             |
		| 4  | Hola que hace   | 2             |
		| 5  | Session SSS     | 2             |


#4 - Get session by id
Scenario: 4. The person wants to see the session data 
	When the person enters to the session with id 5 Page
	Then session details should be
		| Id | Name            | SessionTypeId |
		| 5  | Session SSS     | 2             |


#5 - Get all sessions by name
Scenario: 5. The administrator wants to see all sessions filtered by name
	When the administrator goes to Sessions Page, and filter by "Session SSS" name
	Then the receiving session with name "Session SSS" details should be 
		| Id | Name            | SessionTypeId |
		| 5  | Session SSS     | 2             |