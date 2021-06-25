# Server Side Software - Ultimate Team

## Link Api
```
https://localhost:44345/api
```

## Api Endpoints
* [Calendar](#calendar)
	* Get All Calendar Events
	* Get All Calendars
	* Get Calendar By Id

* [Drive](#drive)
	* Login Drive Account
	* Logout Drive Account
	* Get All Drive Files
	* Save Drive File
	* Get Drive File By Id
	* Update Drive File
	* Delete Drive File
	* Get All Drive Files By Name

* [Administrators](#admins)
	* Get All Admins
	* Save Admin
	* Get All Admins By Area
	* Get Admin By Id
	* Update Admin
	* Delete Admin

* [Administrator Users](#adminusers)
	* Get All Users By Administrator Id
	
* [Friendships](#friendships)
	* Get All Friends By User Id
	* Assign Friendship
	* Unassign Friendship

* [Functionalities](#functionalities)
	* Get All Functionalities
	* Get Functionality By Id
	* Get All SessionStadistics By Functionality Id

* [Group Members](#groupmembers)
	*  Get  All Groups By User Id
	* Save GroupMember
	* Delete GroupMember

* [Groups](#groups)
	* Get All Groups
	* Save Group
	* Get Group By Id
	* Update Group
	* Delete Group
	* Get All Members By Group Id

* [Notifications](#notifications)
	* Get All Notifications

* [Session Stadistics](#session-stadistics)
	* Get All SessionStadistics By Session Id
	* Assign SessionStadistic

* [Subscription Bills](#subsbills)
	* Get All Subscription Bills
	* Save Subscription Bill
	* Get Subscription Bill By Id
	* Update Subscription Bill
	* Delete Subscription Bill

* [Subscription Types](#substypes)
	* Get All Subscription Types
	* Save Subscription Type
	* Get Subscription Type By Id
	* Update Subscription Type
	* Delete Subscription Type

* [Subscription Type Subscription Bills](#substypebills)
	* Get All Sub Bills By Type Id

* [Trello](#trello)
	* Login Trello Account
	* Logout Trello Account
	* Get Trello Member By Id
	* Get Trello Boards By Member Id
	* Get Trello Board By Id
	* Put Trello Board
	* Post Trello Board

* [User Notifications](#user-notifications)
	* Get All Notifications Sent By User Id
	* Get All Notifications Received By User Id
	* Get Notification By Id And User Id
	* Delete Notification
	* Save Notification

* [Users](#users)
	* Get All Users
	* Save User
	* Get User By Id
	* Update User
	* Delete User
	* Get User By Email

### USERS<a name="get-all-users"></a>

#### Get All Users
```
/users
```

#### Get User By Id
```
/users/{userId}
```

#### Get User By Email
```
/users/email={email}
```

#### Save User
```
/users
```

#### Update User
```
/users/{userId}
```

#### Delete User
```
/users/{userId}
```


### FUNCTIONALITIES<a name="functionalities"></a>

#### Get All Functionalities
```
/functionalities
```

#### Get Functionality By Id
```
/functionalities/{functionalityId}
```

#### Get All SessionStadistics By Functionality Id
```
/functionalities/{functionalityId}/sessions
```


### SESSION STADISTICS<a name="session-stadistics"></a>

#### Get All SessionStadistics By Session Id
```
/sessions/{sessionId}/stadistics
```

#### Assign SessionStadistic
```
/sessions/{sessionId}/stadistics/{functionalityId}
```


### NOTIFICATIONS<a name="notifications"></a>

#### Get All Notifications
```
/notifications
```


### USER NOTIFICATIONS<a name="user-notifications"></a>

#### Get All Notifications Sent By User Id
```
/users/{userId}/notifications/sent
```

#### Get All Notifications Received By User Id
```
/users/{userId}/notifications/received
```

#### Get Notification By Id And User Id
```
/users/{userId}/notifications/{notificationId}
```

#### Save Notification
```
/users/{userId}/notifications
```

#### Delete Notification
```
/users/{userId}/notifications/{notificationId}
```