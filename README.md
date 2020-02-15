# CQS Sample
A sample project which shows the command-query-separation architecture in an ASP.NET Core environment.

## General

This is just a simple example project which shows the basic concept of the command-query-separation architecture. It has no real data layer and just a very simplistic authentication using basic authentication with hardcoded users.

## Domain

This sample provides three HTTP endpoints:
1. `GET /user`: Receiving a list of users. 
2. `GET /user/{id}`: Receiving a user by id. 
3. `POST /user`: Adding or updating a user. (Not really implemented, since no data layer is implemented.)

## Users, Authentication, Permissions and Roles

For this simple sample, three users are configured. Users can have two different roles "Regular" or "Admin":
1. User 1: Sherlock Holmes, Id: `00000000-0000-0000-0000-000000000001`, Login password: `sherlock`, Role: `Admin`
1. User 2: John Watson, Id: `00000000-0000-0000-0000-000000000002`, Login password: `john`, Role: `Regular`
1. User 3: James Moriarty, Id: `00000000-0000-0000-0000-000000000003`, Login password: `james`, Role: `Regular`

The following authorization logic is implemented:
* All users are allowed to call `GET /user` to receive all users.
* Receiving a user by id is either allowed by "Admin" users or users can call the endpoint for themself. If regular users try to call the endpoint for other users's id, they receive an HTTP 403 forbidden error.
* Only "Admin" users are allowed to create new users (i.e. call `POST /user`).

Swagger is configured so the endpoints can be tested. Therefore, basic authentication is used where the User-ID must be used as "username".

## Authorization: Permissions and Access Control

The authorization (i.e. which user is allowed to perform which commands and queries) are checked on two different levels:
1. Permissions: Does the user have the general permission to perform an operation, e.g. query users or add/update new users?
2. Access Control: If a user has a general permission to receive users, the second level of the check is if a user has the right to receive one specific user.

### Permissions

* Permissions are configured by placing a `[Permission(...)]` attribute on commands or queries.
* The permission decorators are responsible for checking the permissions for the current user.
* If a `[Permission(...)]` attribute is missing on a command or a query, an exception will be thrown on application start. With that, it's not possible for the developer to forget to apply the `[Permission(...)]` attribute when implementing commands and queries.

### Access Control

* Access control is checked with additional decorators.
* Commands and queries must implement specific interfaces like the `IAccessesUser` interface. With that in place, the according decorators apply and perform access control checks based on the given `UserId` which is defined in the `IAccessesUser` interface.