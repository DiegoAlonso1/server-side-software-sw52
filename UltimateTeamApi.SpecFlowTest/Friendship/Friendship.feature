Feature: Friendship
	Assign, Unassign and Get a Friendship

@mytag
Scenario: 1 Initialize some Friendship Intances
	When users required attributes provided to initialize instances
		| Name     | LastName  | UserName      | Email					| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Sam	   | Morales   | ElTioSam      | sam@hotmail.com		| TresNodos		  | 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	   | Revollar  | Lulu          | lulu@gmail.com		    | CrusUpc#3		  | 2003-01-09 | 2020-01-20     | null			 | 1			   |
		| Maria    | Santillan | Maria503      | ma503@yopmail.com		| Password		  | 2000-07-30 | 2018-09-12     | null			 | 1			   |
		| Lionel   | Messi     | ElMesias      | leo@barzabestclub.com  | elMejorDelMundo | 1212-12-12 | 1219-12-12     | null			 | 1			   |
		| Fernando | Firulais  | FernanElCrack | fernan@elcrack.es      | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |
	Then assign the user with Id 1 with the user with Id 2
	Then assign the user with Id 3 with the user with Id 1
	Then assign the user with Id 1 with the user with Id 4


#1 - Get all friendships by userId
Scenario: The user wants to see his friend list
	When the user with Id 1 goes to Friend Lists
	Then the friend list of user with Id 1 should be
		| Id | Name   | LastName  | UserName  | Email                 | Password        | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| 2  | Lucia  | Revollar  | Lulu      | lulu@gmail.com        | CrusUpc#3       | 2003-01-09 | 2020-01-20     | null           | 1               |
		| 3  | Maria  | Santillan | Maria503  | ma503@yopmail.com     | Password        | 2000-07-30 | 2018-09-12     | null           | 1               |
		| 4  | Lionel | Messi     | ElMesias  | leo@barzabestclub.com | elMejorDelMundo | 1212-12-12 | 1219-12-12     | null		   | 1			     |


#2 - Assign a friendship
Scenario: The user wants be friends with another user
	When the user with Id 5 accepts the friend request from the user with Id 1, the user details should be 
		| Id | Name     | LastName  | UserName      | Email                 | Password        | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| 5  | Fernando | Firulais  | FernanElCrack | fernan@elcrack.es     | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |


#3 - Unassign a friendship
Scenario: The user wants to delete a friend from his friend list
	When the user with id 1 click on the Trash can button next to the user with Id 5
	Then the user with id 5 is removed from the friend list of the user with Id 1 and removed user details should be
		| Id | Name     | LastName  | UserName      | Email                 | Password        | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| 5  | Fernando | Firulais  | FernanElCrack | fernan@elcrack.es     | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |
	