Feature: Notification
	Get All Notifications by User Id, Create a Notification and Recieve a Notification

@mytag
Scenario: 0. Initialize some Friendship Intances
	When users required attributes provided to initialize instances
		| Name     | LastName  | UserName      | Email					| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Sam	   | Morales   | ElTioSam      | sam@hotmail.com		| TresNodos		  | 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	   | Revollar  | Lulu          | lulu@gmail.com		    | CrusUpc#3		  | 2003-01-09 | 2020-01-20     | null			 | 1			   |
		| Maria    | Santillan | Maria503      | ma503@yopmail.com		| Password		  | 2000-07-30 | 2018-09-12     | null			 | 1			   |
		| Lionel   | Messi     | ElMesias      | leo@barzabestclub.com  | elMejorDelMundo | 1212-12-12 | 1219-12-12     | null			 | 1			   |
		| Fernando | Firulais  | FernanElCrack | fernan@elcrack.es      | NuevaContraseña | 1999-05-21 | 2020-05-20     | null			 | 1               |

#1 Create a Notification
Scenario: 1. The user wants to send a friend request
	When the user with Id 5 sends a notification to the user with Id 1, the notifications details should be 
		| Id | SenderId | RemitendId | DateTime   | Description                           |
		| 1  |    5     |     1      | 2021-06-22 | Acepta mi solicitud de amistad        |
		| 2  |    5     |     1      | 2021-06-20 | Acepta la solicitud para hacer amigos |
		

#2 Get a Notification
Scenario: 2. The user recieve a notification
	When the user with Id 1 recieves a notification of the user with Id 5, the notifications details should be 
		| Id | SenderId | RemitendId | DateTime   | Description                           |
		| 1  |    5     |     1      | 2021-06-22 | Acepta mi solicitud de amistad        |
		| 2  |    5     |     1      | 2021-06-20 | Acepta la solicitud para hacer amigos |

#3 Get all Notifications by User Id
Scenario: 3. The user wants to see his notification list
	When the user with Id 5 wants to see his list of notifications
	Then the notification list of the user with Id 5 should be
		| Id | SenderId | DateTime   | Description                    |
		| 1  |   5      | 2021-06-22 | Acepta mi solicitud de amistad |
		| 2  |   4      | 2021-06-20 | Acepta para hacer amigos       |