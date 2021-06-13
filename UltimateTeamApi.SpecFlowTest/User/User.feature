Feature: User
	Create, Update, Delete and Get a User

# There are some instructions in the file "UserSteps.cs"

@mytag
Scenario: 1 Initialize some User Intances
	When users required attributes provided to initialize instances
		| Name   | LastName  | UserName | Email					| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Sam	 | Morales   | ElTioSam | sam@hotmail.com		| TresNodos		  | 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	 | Revollar  | Lulu     | lulu@gmail.com		| CrusUpc#3		  | 2003-01-09 | 2020-01-20     | null			 | 1			   |
		| Maria  | Santillan | Maria503 | ma503@yopmail.com		| Password		  | 2000-07-30 | 2018-09-12     | null			 | 1			   |
		| Lionel | Messi     | ElMeias  | leo@barzabestclub.com | elMejorDelMundo | 1212-12-12 | 1219-12-12     | null			 | 1			   |


#1 - Create an user
Scenario: The user wants to register
	When the user complete the form with the required fields and click the Register button
		| Name   | LastName  | UserName   | Email				| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Fernan | Floo		 | Fernanfloo | fernan@elcrack.es	| Contraseña	  | 1999-05-21 | 2020-03-20     | null			 | 1			   |


#2 - Update an user
Scenario: The user wants to update their data profile
	When the user with id 5 complete the form with required fields and click the Update button
		| Name	   | LastName | UserName      | Email             | Password        | Birthdate  | LastConnection | ProfilePicture   | AdministratorId |
		| Fernando | Firulais | FernanElCrack | fernan@elcrack.es | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |


#3 - Get all users
Scenario: The administrator wants to see all users
	When the administrator goes to Users Page, user list should return
		| Id | Name     | LastName  | UserName      | Email                 | Password        | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| 1  | Sam      | Morales   | ElTioSam      | sam@hotmail.com       | TresNodos       | 2002-04-19 | 2020-04-19     | null           | 1               |
		| 2  | Lucia    | Revollar  | Lulu          | lulu@gmail.com        | CrusUpc#3       | 2003-01-09 | 2020-01-20     | null           | 1               |
		| 3  | Maria    | Santillan | Maria503      | ma503@yopmail.com     | Password        | 2000-07-30 | 2018-09-12     | null           | 1               |
		| 4  | Lionel   | Messi     | ElMeias       | leo@barzabestclub.com | elMejorDelMundo | 1212-12-12 | 1219-12-12     | null			 | 1			   |
		| 5  | Fernando | Firulais  | FernanElCrack | fernan@elcrack.es     | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |


#4 - Get user by id
Scenario: The user wants to see his profile data 
	When the user with id 5 goes to Profile Page
	Then user details should be
	| Id | Name     | LastName  | UserName      | Email                 | Password        | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
	| 5  | Fernando | Firulais  | FernanElCrack | fernan@elcrack.es     | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |


#5 - Get user by email
Scenario: The user wants to send a friend request
	When the user send a friend request to email "lulu@gmail.com"
	Then the receiving user details should be
		| Id | Name     | LastName  | UserName      | Email                 | Password        | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| 2  | Lucia    | Revollar  | Lulu          | lulu@gmail.com        | CrusUpc#3       | 2003-01-09 | 2020-01-20     | null           | 1               |


#6 - Delete a user
Scenario: The user wants to delete his account
	When the user with id 5 click the Delete Account button
	Then the user with id 5 is removed and removed user details should be
		| Id | Name     | LastName | UserName      | Email             | Password        | Birthdate  | LastConnection | ProfilePicture  | AdministratorId |
		| 5  | Fernando | Firulais | FernanElCrack | fernan@elcrack.es | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |
