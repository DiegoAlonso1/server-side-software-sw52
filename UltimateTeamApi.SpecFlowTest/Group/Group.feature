Feature: Group
	Create, Update, Delete and Get a Group

# There are some instructions in the file "GroupSteps.cs"

@mytag
Scenario: 0. Initialize some Group Intances
	When groups required attributes provided to initialize instances
		| Name           | 
		| TeamUltimate	 |
		| Backyardigans	 |
		| Pollitos FC    |
		| Roomies        |
	When persons required attributes provided to initialize instances
		| Name   | LastName  | UserName | Email					| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Sam	 | Morales   | ElTioSam | sam@hotmail.com		| TresNodos		  | 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	 | Revollar  | Lulu     | lulu@gmail.com		| CrusUpc#3		  | 2003-01-09 | 2020-01-20     | null			 | 1			   |
	Then assign the person with Id 1 on the group with Id 2
		| PersonCreator   |
		| True			|
	Then assign the person with Id 2 on the group with Id 2
		| PersonCreator   |
		| False			|
	

#1 - Create a group
Scenario: 1. The person wants to create a group
	When the person complete the form with the required fields and click the Create button
		| Name     |
		| Bichotas |


#2 - Update a group
Scenario: 2. The person wants to update a group
	When the person complete the form to update the group with Id 5 and click the Update button
		| Name	    |
		| TeamWork  |


#3 - Get all groups
Scenario: 3. The administrator wants to see all groups
	When the administrator goes to Groups Page, group list should return
		| Id | Name          |
		| 1  | TeamUltimate	 |
		| 2  | Backyardigans |
		| 3  | Pollitos FC   |
		| 4  | Roomies       |
		| 5  | TeamWork      |


#4 - Get group by id
Scenario: 4. The person wants to see his group data 
	When the person goes to Group Lists and click on group with id 5
	Then group details should be
		| Id | Name     |
		| 5  | TeamWork |

		
#5 - Get all members by group id
Scenario: 5. The person wants to see the members list of his group
	When the person goes to Group Lists and click on the group with Id 2 and go to Member List Section
	Then the member list of group with Id 2 should be
		| Id   | Name    | LastName  | UserName | Email					| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| 1	   | Sam	 | Morales   | ElTioSam | sam@hotmail.com		| TresNodos		  | 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| 2	   | Lucia	 | Revollar  | Lulu     | lulu@gmail.com		| CrusUpc#3		  | 2003-01-09 | 2020-01-20     | null			 | 1			   |


#6 - Delete a group
Scenario: 6. The person wants to delete his group
	When the person with id 5 click the Delete Group button
	Then the person with id 5 is removed and removed group details should be
		| Id | Name     |
		| 5  | TeamWork |
