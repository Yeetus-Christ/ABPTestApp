# Overview

This is an API, which provides functionality for working with service for booking conference halls.

# Project setup

1. Open appsetting.json file and change the connection string to the connection string of your local database.

```json
"ConnectionStrings": {
  "ABPDb": "Your connection string"
}
```

2. Open the project in the terminal. Input the following command to create the tables in your local database and fill them with initial data:

```
dotnet ef database update
```
