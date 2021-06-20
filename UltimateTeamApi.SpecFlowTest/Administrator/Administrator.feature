Feature: Administrator
	Create, Update, Delete and Get an Administrator

@mytag
Scenario: 0. Initialize some Administrator Intances
	When administrators required attributes provided to initialize instances
		| Name        | Password  | Area	  |
		| Crsitiano7  | Madrid13  | null      |
		| Paolo9      | Peru2020  | null      |
		| MarioG      | Gam264    | null      |
		| Isabel_2    | pasword   | null      |


#1 - Create an administrator
Scenario: 1. The administrator wants to register
	When the administrator complete the form with the required fields and click the Register button
		| Name        | Password  | Area	  |
		| Isak54      | RS0815    | null      |



#2 - Update an administrator
Scenario: 2. The administrator wants to update their data profile
	When the administrator with id 5 complete the form with required fields and click the Update button
		| Name        | Password  | Area	  |
		| ZlatanGod   | ACM05     | null      |



#3 - Get all administrators
Scenario: 3. The administrator wants to see all the administrators
	When the administrator goes to see all the administrators, administrator list should return
		| Id | Name        | Password  | Area	  |
		| 1	 | Crsitiano7  | Madrid13  | null     |
		| 2  | Paolo9      | Peru2020  | null     |
		| 3	 | MarioG      | Gam264    | null     |
		| 4  | Isabel_2    | pasword   | null     |
		| 5	 | ZlatanGod   | ACM05     | null     |


#4 - Get user by id
Scenario: 4. The administrator wants to see his profile data 
	When the administrator with id 5 goes to Profile Page
	Then administrator details should be
		| Id | Name        | Password  | Area	  |
		| 5	 | ZlatanGod   | ACM05     | null     |


#5 - Delete a user
Scenario: 6. The administrator wants to delete his account
	When the administrator with id 5 click the Delete Account button
	Then the administrator with id 5 is removed
		| Id | Name        | Password  | Area	  |
		| 5	 | ZlatanGod   | ACM05     | null     |