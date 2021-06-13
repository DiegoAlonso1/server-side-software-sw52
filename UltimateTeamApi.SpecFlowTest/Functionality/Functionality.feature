Feature: Functionality
	Get a Functionality and Get SessionStadistic

# There are some instructions in the file "FunctionalitySteps.cs"

@mytag
#1 - Get all functionalities
Scenario: The administrator wants to see all functionalities
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
Scenario: The administrator wants to see a functionality details
	When the administrator select functionality with id 4
	Then functionality details should be
		| Id | Name          |
		| 4  | Notes         |


#3 - Get all session stadistics by functionality id
Scenario: The administrator wants to see a functionality stadistics
	When the administrator select functionality with id 4
	Then functionality stadistics details should be
		| SessionId | FunctionalityId | Count |
		| 1         | 4				  | 8     |