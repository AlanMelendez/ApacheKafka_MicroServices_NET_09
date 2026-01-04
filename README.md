## How to Run the Project

This guide assumes you have Docker and .NET 9 SDK installed.

### 1. Start the Database
First, get the MongoDB container running. This setup uses a replica set, which is necessary for transactions.

```bash
# This command starts MongoDB in the background and forces a recreate to apply any changes.
docker compose up -d --force-recreate
```
Wait a few seconds for the container's health check to initialize the replica set. You can check the container logs or status to be sure.

### 2. Run the API Services
With the database ready, you can start the .NET applications.

```bash
# Run the Command API (handles writes)
dotnet run --project Projects/Ticketing.Command
```

```bash
# In a separate terminal, run the Query API (handles reads)
dotnet run --project Projects/Ticketing.Query
```

The `Ticketing.Command` API will now be running and able to connect to the MongoDB replica set.

---

## Project Setup from Scratch

These were the commands used to generate the initial project structure. You don't need to run them again.

### List all SDKs installed in your machine
```bash
dotnet --list-sdks
# Example output: 9.0.304 [C:\Program Files\dotnet\sdk]
```
### Create file global.json with the SDK version you want to use
```bash
dotnet new globaljson --sdk-version 9.0.304 --force
```

### Create Solution Project
```bash
dotnet new sln --name Microservices
```

### Create Projects
```bash
dotnet new webapi --output Projects/Ticketing.Command
```

```bash
dotnet new webapi --output Projects/Ticketing.Query
```

### Add Projects to Solution
```bash
dotnet sln add Projects/Ticketing.Command
dotnet add package Scalar.AspNetCore --version 2.0.2
```

```bash
dotnet sln add Projects/Ticketing.Query
```

### Generate build to validate everything is ok
```bash
dotnet build
```

## Generate command project to communicate between microservices
```bash
dotnet new classlib -o Common/Common.Core
```
## Add project Common/Common.Core to solution

```bash
dotnet sln add Common/Common.Core
```

## Add PackageNugget to Common.Core project
```bash
cd Common/Common.Core
```

```bash
dotnet add package MongoDB.Bson --version 3.1.0
dotnet add package MongoDB.Driver --version 3.1.0
dotnet add package Newtonsoft.Json --version 13.0.3
```



## How can connect with MongoDB extension in Visual Studio Code.

1. Install the MongoDB extension for Visual Studio Code if you haven't already.
2. Open Visual Studio Code.
3. Go to the "MongoDB" extension panel on the left sidebar.
4. Click on the "Connect" button (usually represented by a plug icon).
5. In the connection dialog, enter the following connection string. Using `replicaSet=rs0` is important for it to work correctly with the project's setup.
    ```
    mongodb://localhost:27017/?replicaSet=rs0
    ```
6. Click "Connect" to establish the connection to the MongoDB instance running in your Docker container.












## Scalar API interface xdxdxd
``bash
dotnet watch --project Projects/Ticketing.Command/
``



