Feature: Notification
	Get All Notifications

@mytag
Scenario: 1 Initialize some Friendship Intances
	When users required attributes provided to initialize instances
		| Name     | LastName  | UserName      | Email					| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Sam	   | Morales   | ElTioSam      | sam@hotmail.com		| TresNodos		  | 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	   | Revollar  | Lulu          | lulu@gmail.com		    | CrusUpc#3		  | 2003-01-09 | 2020-01-20     | null			 | 1			   |
		| Maria    | Santillan | Maria503      | ma503@yopmail.com		| Password		  | 2000-07-30 | 2018-09-12     | null			 | 1			   |
		| Lionel   | Messi     | ElMesias      | leo@barzabestclub.com  | elMejorDelMundo | 1212-12-12 | 1219-12-12     | null			 | 1			   |
		| Fernando | Firulais  | FernanElCrack | fernan@elcrack.es      | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |