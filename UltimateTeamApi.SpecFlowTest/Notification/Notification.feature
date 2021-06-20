Feature: Notification
	Get All Notifications

@mytag
Scenario: 0. Initialize some Friendship Intances
	When users required attributes provided to initialize instances
		| Name     | LastName  | UserName      | Email					| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Sam	   | Morales   | ElTioSam      | sam@hotmail.com		| TresNodos		  | 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	   | Revollar  | Lulu          | lulu@gmail.com		    | CrusUpc#3		  | 2003-01-09 | 2020-01-20     | null			 | 1			   |
		| Maria    | Santillan | Maria503      | ma503@yopmail.com		| Password		  | 2000-07-30 | 2018-09-12     | null			 | 1			   |
		| Lionel   | Messi     | ElMesias      | leo@barzabestclub.com  | elMejorDelMundo | 1212-12-12 | 1219-12-12     | null			 | 1			   |
		| Fernando | Firulais  | FernanElCrack | fernan@elcrack.es      | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |


#1 - Get all groups by user id
Scenario: 1. The user wants to see the groups list of his profile data
	When the user with id 2 goes to Profile Page and click on the groups list
	Then the groups list of user with Id 2 should be
		| Id   | Name			 |
		| 1	   | TeamUltimate	 |
		| 2	   | Backyardigans	 |