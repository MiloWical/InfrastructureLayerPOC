# Introduction 
This project is designed to give an overview of what is being termed an "Infrastructure Layer" for web services.

In particular, an "Infrastructure Layer" as used here is an implementation of what, in other circles is termed an "Anticorruption Layer". 
For reference, here are a few links for the Anticorruption Pattern:

* [Microsoft](https://docs.microsoft.com/en-us/azure/architecture/patterns/anti-corruption-layer)
* [Eric Evans](https://domainlanguage.com/wp-content/uploads/2016/05/DDD_Reference_2015-03.pdf) (The originator of the term - see p. 34)
* [Martin Fowler](https://martinfowler.com/articles/refactoring-external-service.html#SeparatingTheYoutubeDataStructureIntoAGateway) (More of an illustration than definition)
* [Dino Esposito](https://www.microsoftpressstore.com/articles/article.aspx?p=2248811&seqNum=3) (A holistic view of where ACLs fit in a broader context.)


#Project Structure
The projects in this solution are built for the following purposes:

| Project Name| Purpose |
| ---| --- |
| DemoService.Contract | Defines the operations that exist on the service implementation. Also contains definitions for various SettingProviders. |
| DemoService.Contract.UnitTest | Mostly just unit tests for the SettingProviders. |
| DemoService.Implementation | The core service implementation of IDemonstrationService. |
| DemoService.Infrastructure.Docker | A Docker wrapper around the service implementation using a custom protocol on TCP port 54321. (See [this](https://github.com/MiloWical/InfrastructureLayerPOC/blob/master/InfrastructureLayerPOC/DemoService.Infrastructure.Docker/Readme.txt) |
| DemoService.Infrastructure.WCF | A traditional WCF-based SOAP web service that wraps the service implementation. The WCF contract and the service interface are intentionally different. |
| DemoService.Infrastructure.WebAPI | A REST-based wrapper around the service implementation. It's also got a Swagger UI - append /swagger to the service URL when running to access it. |
| DemoService.Test | Unit and load tests for the service implementation. (Load testing may require VS 2017.) |
| TcpTestClient | A .NET Core test client that connects to an IP address and TCP port to send ASCII messages. An empty message terminates the application. |

# Getting Started

There are a few prerequisites you'd need to set up before building and running the projects: 

* __DemoService.Contract__: .NET Framework 4.6.2
* __DemoService.Implementation__: .NET Framework 4.6.2
* __DemoService.Infrastructure.Docker__: A Docker host (Windows or Linux), .NET Core 2.0
* __DemoService.Infrastructure.WCF__: .NET Framework 4.6.2, IIS for local hosting (_optional_)
* __DemoService.Infrastructure.WebAPI__: .NET Framework 4.6.2, ASP.NET, IIS for local hosting (_optional_)
* __TcpTestClient__: .NET Core 2.0

All projects need access to nuget.org to download any 3rd-party dependencies at build.

# Build and Test
If building the projects from scratch, they need to be built in the following order:

1. DemoService.Contract
2. DemoService.Implementation
3. One of (depends on how you want to run): DemoService.Infrastructure.Docker, DemoService.Infrastructure.WCF, or DemoService.Infrastructure.WebAPI
4. TcpTestClient (if you're running the Docker host - a test client (WCF) or Swagger (WebAPI) is easier for the others)

# Contribute
Please don't update this repo without talking to Milo about it first. It's a point-in-time style project, and making modifications will be problematic
and things could get out-of-hand.
