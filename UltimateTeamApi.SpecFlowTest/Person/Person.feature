Feature: Person
	Create, Update, Delete and Get a Person

# There are some instructions in the file "PersonSteps.cs"

@mytag
Scenario: 0. Initialize some Person Intances
	When persons required attributes provided to initialize instances
		| Name   | LastName  | UserName | Email					| Birthdate  | LastConnection | ProfilePicture   | AdministratorId |
		| Sam	 | Morales   | ElTioSam | sam@hotmail.com		| 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	 | Revollar  | Lulu     | lulu@gmail.com		| 2003-01-09 | 2020-01-20     | null			 | 1			   |
		| Maria  | Santillan | Maria503 | ma503@yopmail.com		| 2000-07-30 | 2018-09-12     | null			 | 1			   |
		| Lionel | Messi     | ElMeias  | leo@barzabestclub.com | 1212-12-12 | 1219-12-12     | null			 | 1			   |


#1 - Create a person
Scenario: 1. The person wants to register
	When the person complete the form with the required fields and click the Register button
		| Name   | LastName | UserName   | Email             | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Fernan | Floo     | Fernanfloo | fernan@elcrack.es | 1999-05-21 | 2020-03-20     | null           | 1               |


#2 - Update a person
Scenario: 2. The person wants to update their data profile
	When the person with id 5 complete the form with required fields and click the Update button
		| Name	   | LastName | UserName      | Email             | Birthdate           | LastConnection          | ProfilePicture   | AdministratorId |
		| Fernando | Firulais | FernanElCrack | fernan@elcrack.es | 1999-05-21T00:00:00 | 2020-05-20T00:00:00     | null			   | 1               |


#3 - Get all persons
Scenario: 3. The administrator wants to see all persons
	When the administrator goes to Persons Page, person list should return
		| Id | Name     | LastName  | UserName      | Email                 | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| 1  | Sam      | Morales   | ElTioSam      | sam@hotmail.com       | 2002-04-19 | 2020-04-19     | null           | 1               |
		| 2  | Lucia    | Revollar  | Lulu          | lulu@gmail.com        | 2003-01-09 | 2020-01-20     | null           | 1               |
		| 3  | Maria    | Santillan | Maria503      | ma503@yopmail.com     | 2000-07-30 | 2018-09-12     | null           | 1               |
		| 4  | Lionel   | Messi     | ElMeias       | leo@barzabestclub.com | 1212-12-12 | 1219-12-12     | null			 | 1			   |
		| 5  | Fernando | Firulais  | FernanElCrack | fernan@elcrack.es     | 1999-05-21 | 2020-05-20     | null			 | 1               |


#4 - Get person by id
Scenario: 4. The person wants to see his profile data 
	When the person with id 5 goes to Profile Page
	Then person details should be
		| Id | Name     | LastName  | UserName      | Email                 | Birthdate           | LastConnection          | ProfilePicture | AdministratorId |
		| 5  | Fernando | Firulais  | FernanElCrack | fernan@elcrack.es     | 1999-05-21T00:00:00 | 2020-05-20T00:00:00     | null			  | 1               |


#5 - Get person by email
Scenario: 5. The person wants to send a friend request
	When the person send a friend request to email "lulu@gmail.com"
	Then the receiving person details should be 
		| Id | Name     | LastName  | UserName      | Email                 | Birthdate           | LastConnection          | ProfilePicture | AdministratorId |
		| 2  | Lucia    | Revollar  | Lulu          | lulu@gmail.com        | 2003-01-09T00:00:00 | 2020-01-20T00:00:00     | null           | 1               |


#6 - Delete a person
Scenario: 6. The person wants to delete his account
	When the person with id 5 click the Delete Account button
	Then the person with id 5 is removed and removed person details should be
		| Id | Name     | LastName | UserName      | Email             | Birthdate           | LastConnection	         | ProfilePicture  | AdministratorId |
		| 5  | Fernando | Firulais | FernanElCrack | fernan@elcrack.es | 1999-05-21T00:00:00 | 2020-05-20T00:00:00     | null			 | 1               |
