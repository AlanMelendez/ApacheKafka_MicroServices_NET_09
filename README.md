## Comands to generate the project with SDK version 


## Configuration VHost in linux Machine
```bash
 cd sudo nano /etc/docker/daemon.json
```
add the following lines
```json
{
  "hosts": ["unix:///var/run/docker.sock"],
  "bip": "172.17.0.1/16"
}
```
or maybe you may add permanently to /etc/hosts file the following line
```bash
echo "172.17.0.1 host.docker.internal" | sudo tee -a /etc/hosts
```
then restart docker service
```bash
sudo systemctl restart docker
```

#### How can work the last configuration?
When you run docker containers in linux machine, the docker engine create a network bridge with a default
IP range (172.17.0.0/16) and assigns a unique IP address to each container from this range. The host machine can access the containers using the IP address assigned to them. However, the containers cannot access the host machine directly using localhost or host.docker.internal because these addresses resolve to the container's own network namespace, not the host's.

To allow containers to access services running on the host machine, you can use the host's IP address within the bridge network. In this case, the host's IP address in the default bridge network is typically 172.17.0.1. By adding an entry to the /etc/hosts file inside the container that maps host.docker.internal to 172.17.0.1, you can enable this communication.


---
<!-- ----------------------------------------------------------------------------
    Separation between Docker configuration / explanations and the SDK/project steps
    Use this horizontal rule to clearly divide configuration sections from build/setup
---------------------------------------------------------------------------- -->
---



## List all SDKs installed in your machine
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



## How can connect with MongoDB extension in Visual Studio Code.

1. Install the MongoDB extension for Visual Studio Code if you haven't already.
2. Open Visual Studio Code.
3. Go to the "MongoDB" extension panel on the left sidebar.
4. Click on the "Connect" button (usually represented by a plug icon).
5. In the connection dialog, enter the following connection string:
    ```
    mongodb://host.docker.internal:27017 
    mongodb://172.18.0.1:27017/?replicaSet=rs0&tls=false
    ```
6. Click "Connect" to establish the connection to the MongoDB instance running in your Docker container.



