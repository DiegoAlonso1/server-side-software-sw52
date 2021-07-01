﻿Feature: Functionality
	Get a Functionality and Get SessionStadistic

# There are some instructions in the file "FunctionalitySteps.cs"

@mytag
Scenario: 0. Initialize some entities instances
	When sessions required attributes provided to initialize instances
		| Name            | SessionTypeId |
		| UltimateTeam    | 1             |
		| Clase de fisica | 2             |
		| Reunión         | 1             |
		| Hola que hace   | 2             |
	Then assign the session with Id 1 with the functionality with Id 4


#1 - Get all functionalities
Scenario: 1. The administrator wants to see all functionalities
	When the administrator goes to Functionality Usage Page, functionalities list should return
		| Id | Name          |
		| 1  | Stream        |
		| 2  | Laser Pointer |
		| 3  | Boards        |
		| 4  | Notes         |
		| 5  | Calendar      |
		| 6  | Alarm         |
		| 7  | ToDo List     |


#2 - Get functionality by id
Scenario: 2. The administrator wants to see a functionality details
	When the administrator select functionality with id 4
	Then functionality details should be
		| Id | Name          |
		| 4  | Notes         |


#3 - Get all session stadistics by functionality id
Scenario: 3. The administrator wants to see a functionality stadistics
	When the administrator select functionality with id 4
	Then functionality stadistics details should be
		| SessionId | FunctionalityId | Count |
		| 1         | 4				  | 1     |