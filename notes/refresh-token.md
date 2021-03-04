# Refresh tokens

- `Auth/login` (Creds) --> JWT
- `Auth/register` (Creds) --> JWT
- `Auth/refresh-token` (RFT) --> JWT

Process:

- User Register or Singin

  - Password is given
  - Check if user email exist + Password is hashed
  - Generate JWToken
  - Generate RefreshToken then store it into the database.

- JWToken expired
  - Request with refresh token to `/Auth/refresh`
  - token is validate with `ValitationParamters`
  - Then the RefreshToken row is retrieve from the db thanks to a _"GetByToken"_
  - User is retrieve from the UserId (ForeignKey --> use of Include) in the RefreshToken row.
  - Then the refresh token is deleted
  - The new JWT token + refresh token is generated with the user credentials.

# Things to handle

Should add a `cron` don't know the equivalent for csharp or postgre that delete all the RefreshToken row for which the refreshToken is expired.

How to do that:

- From Csharp ?
- From DB with view (should add an expireDate which is better than decoding the RefreshToken )?

--> **ASK THE TEACHER**

A solution could also be a small application link with the database which delete all the expired RefreshToken with golang and cron on unix

> https://www.elephantsql.com/docs/go.html

with a small script like this:

```go
//This example uses the ORM jet
package main

import (
    "github.com/eaigner/jet"
    "github.com/lib/pq"
    "log"
    "os"
)

func logFatal(err error) {
    if err != nil {
        log.Fatal(err)
    }
}
func main() {
    //Make sure you setup the ELEPHANTSQL_URL to be a uri, e.g. 'postgres://user:pass@host/db?options'
    pgUrl, err := pq.ParseURL(os.Getenv("ELEPHANTSQL_URL"))
    logFatal(err)
    db, err := jet.Open("postgres", pgUrl)
    logFatal(err)

    err = db.Query("DELETE * FROM RefreshTokens WHERE montest")
    logFatal(err)
}

```

# Questions/Troubleshouting:

_When logout the refresh token associate with the user should be deleted/Revoke_

--> Problems:

- All the refresh token will be deleted, so if the user is connected from another platform, It gonna deleted the RefreshToken associate with an active JWToken.
- Moreover the JWToken itself will not be revoke, because not stored into the database

# Soluce ?

- Should store the JWToken with the Refresh Token ?
  - Then we should add logic inside the JWToken middleware in order to check if the token exist in the database. (could be a bad idea in term of database call)
