Feature: GroupMember
	Create, Delete and Get a GroupMember

# There are some instructions in the file "GroupMemberSteps.cs"


@mytag
Scenario: 0. Initialize some GroupMember Intances
	When groups required attributes provided to initialize instances
		| Name           | 
		| TeamUltimate	 |
		| Backyardigans	 |
		| Pollitos FC    |
		| Roomies        |
	When users required attributes provided to initialize instances
		| Name   | LastName  | UserName | Email					| Password		  | Birthdate  | LastConnection | ProfilePicture | AdministratorId |
		| Sam	 | Morales   | ElTioSam | sam@hotmail.com		| TresNodos		  | 2002-04-19 | 2020-04-19     | null			 | 1			   |
		| Lucia	 | Revollar  | Lulu     | lulu@gmail.com		| CrusUpc#3		  | 2003-01-09 | 2020-01-20     | null			 | 1			   |
	Then assign the user with Id 2 on the group with Id 1
		| UserCreator   |
		| True			|
	Then assign the user with Id 2 on the group with Id 2
		| UserCreator   |
		| True			|


#1 - Get all groups by user id
Scenario: 1. The user wants to see the groups list of his profile data
	When the user with id 2 goes to Profile Page and click on the groups list
	Then the groups list of user with Id 2 should be
		| Id   | Name			 |
		| 1	   | TeamUltimate	 |
		| 2	   | Backyardigans	 |

#2 - Assign a group with the user
Scenario: 2. The user wants to create a group
	When the user complete the form with the required fields and click the Create button
		| Name     |
		| Bichotas |
	Then it is assigned to the user with Id 2 on the group with Id 5 and list groups should be
		| Id   | Name			 |
		| 1	   | TeamUltimate	 |
		| 2	   | Backyardigans	 |
		| 5	   | Bichotas		 |



#3 - Unassign a group with the user
Scenario: 3. The user wants to leave his group
	When the user with id 2 click the Leave Group button
	Then the user with id 2 leaves the group and removed group details should be
		| Id | Name			 |
		| 2  | Backyardigans |
