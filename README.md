# Server Side Software - Ultimate Team

## Link Api
```
https://localhost:44345/api
```

## Api Endpoints
* [Users](#users)
	* Get All Users
	* Get User By Id
	* Save User
	* Update User
	* Delete User

* [Functionalities](#functionalities)
	* Get All Functionalities
	* Get Functionality By Id
	* Get All SessionStadistics By Functionality Id

* [Session Stadistics](#session-stadistics)
	* Get All SessionStadistics By Session Id
	* Assign SessionStadistic

### USERS<a name="get-all-users"></a>

#### Get All Users
```
/users
```

#### Get User By Id
```
/users/{userId}
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