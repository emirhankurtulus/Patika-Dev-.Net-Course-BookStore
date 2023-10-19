
# BookStore Api Project

The bookstore API is a system used to add book categories, authors, and brief descriptions. Employees use this API to update and manage this information. The API provides functionalities such as categorizing books and filtering based on specific authors.

## API Usage

#### Register


```http
  POST /register
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Email` | `string` | **Required**. Valid email format. |
| ` Password` | `string`| **Required** Max 20 Min 8 Length|
| ` FirstName` | `string`| **Required** Max 50 Length|
| ` Surname` | `string`| **Required** Max 50 Length|

#### Get Token

```http
  Post /user/login
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `email`      | `string` | **Required** |
| `password`      | `string` | **Required** |


#### Get Refresh Token
```http
  Get /user/refreshtoken
```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

#### Update A User
```http
  PUT /user
```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `Guid` | **Required**. The user ID associated with the logged-in user and the token.. |
| `Email` | `string` | **Required**. Valid email format. |
| ` FirstName` | `string`| **Required** Max 50 Length|
| ` Surname` | `string`| **Required** Max 50 Length|

#### Change Password
```http
  Post /user/changePassword
```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `Guid` | **Required**. The user ID associated with the logged-in user and the token. |
| `PreviousPassword` | `string` | **Required**. Max 20 Min 8 Length. |
| ` NewPassword` | `string`| **Required** Max 20 Min 8 Length|

#### Get Current User's Info
```http
  Get /user/me
```

  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

#### Get An Author With Id

```http
  GET /authors/${id}
```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

#### Get Authors

```http
  GET /authors
```

  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

    Filters can be used at this endpoint.

#### Delete An Author With Id

```http
  Delete /authors/${id}
```
#### Add An Author

```http
  Post /authors
  ``` 
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE
  | Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `FirstName` | `string` | **Required**. The user ID associated with the logged-in user and the token.. |
| ` FirstName` | `string`| **Required** Max 50 Length|
| ` Surname` | `string`| **Required** Max 50 Length|
| ` BirthDate` | `dateOnly`| **Required**|


#### Update An Author With Id

```http
  Put /authors
  ```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE
    
  | Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `Guid` | **Required**. The user ID associated with the logged-in user and the token.. |
| `Email` | `string` | **Required**. Valid email format. |
| ` FirstName` | `string`| **Required** Max 50 Length|
| ` Surname` | `string`| **Required** Max 50 Length|
| ` BirthDate` | `dateOnly`| **Required**|

#### Get A Genre With Id

```http
  GET /genres/${id}
```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

#### Get Genres

```http
  GET /genres
```

  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

    Filters can be used at this endpoint.

#### Delete A Genre With Id

```http
  Delete /genres/${id}
  ```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

  #### Add A Genre

```http
  Post /genres
  ``` 
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE
  | Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Name` | `string` | **Required**. Max 50 Length |

```http
  Put /genres
  ``` 
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE
  | Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `Guid` | **Required**.|
| `Name` | `string` | **Required**. Max 50 Length |

  #### Get A Book

```http
  GET /books/${id}
```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

#### Get Books

```http
  GET /Books
```

  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

    Filters can be used at this endpoint.

#### Delete A Book With Id

```http
  Delete /books/${id}
  ```
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

#### Add A Book

```http
  Post /books
  ``` 
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

  | Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `AuthorId` | `Guid` | **Required**. A valid AuthorId|
| ` Title` | `string`| **Required** Max 100 Length|
| ` PageCount` | `int`| **Required** Greater Than 0|
| ` PublishDate` | `dateOnly`| **Required** Less Than Now|

#### Update A Book

```http
  Put /books
  ``` 
  Headers:
    Authorization: Bearer YOUR_TOKEN_HERE

  | Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `Guid` | **Required**.|
| `AuthorId` | `Guid` | **Required**. A valid AuthorId|
| ` Title` | `string`| **Required** Max 100 Length|
| ` PageCount` | `int`| **Required** Greater Than 0|
| ` PublishDate` | `dateOnly`| **Required** Less Than Now|

### Used packages

* AutoMapper
* BCrypt.Net-Next
* Fluent Validation
* EF Core In Memory
* JwtBearer
* NewtonSoft.Json
* xunit



  
