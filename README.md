## Comands to generate the project with SDK version 

### List all SDKs installed in your machine
```bash
dotnet --list-sdks
# 9.0.304 [C:\Program Files\dotnet\sdk]
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
```

```bash
dotnet sln add Projects/Ticketing.Query
```

### Generate build to validate everything is ok
```bash
dotnet build
```

```bash
dotnet run --project Projects/Ticketing.Command
```

```bash
dotnet run --project Projects/Ticketing.Query
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


