Feature: SessionParticipant
	Assign and Get a Session Participant

# There are some instructions in the file "SessionParticipantSteps.cs"

@mytag
Scenario: 0. Initialize some Session Participant Intances
	When sessions required attributes provided to initialize instances
		| Name            | SessionTypeId |
		| UltimateTeam    | 1             |
		| Clase de fisica | 2             |
	When persons required attributes provided to initialize instances
        | Name   | LastName  | UserName | Email					| Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Sam	 | Morales   | ElTioSam | sam@hotmail.com		| 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	 | Revollar  | Lulu     | lulu@gmail.com		| 2003-01-09 | 2020-01-20     | null			 | 1			   |
		| Maria  | Santillan | Maria503 | ma503@yopmail.com		| 2000-07-30 | 2018-09-12     | null			 | 1			   |
		| Lionel | Messi     | ElMeias  | leo@barzabestclub.com | 1212-12-12 | 1219-12-12     | null			 | 1			   |
	Then assign the person with id 1 with the session with id 1
		| Creator |
		| true    |
	Then assign the person with id 2 with the session with id 1
		| Creator |
		| false    |
	Then assign the person with id 3 with the session with id 2
		| Creator |
		| true    |
	Then assign the person with id 2 with the session with id 2
		| Creator |
		| false   |


#1 - Assign a session participant
Scenario: 1. The person wants to join a session
	When the person with id 4 click the Join Session button
	Then assign the person with id 4 with the session with id 1
		| Creator |
		| false   |


#2 - Get all session participants by person id
Scenario: 2. The person wants to see his sessions history
	When the person with id 2 goes to Sessions History section, session participants list should return
		| PersonId | SessionId | Creator |
		| 2		   | 1         | false   |
		| 2		   | 2         | false   |

#3 - Get all session participants by person creator id
Scenario: 3. The person wants to see his sessions history filter by sessions created
	When the person with id 1 goes to Sessions History section and filter sessions created, session participants list should return
		| PersonId | SessionId | Creator |
		| 1		   | 1         | true    |