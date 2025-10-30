# Changelog

This file follows the Keep a Changelog format and Semantic Versioning (SemVer) when applicable.

All dates use YYYY-MM-DD and are based on this repo's commit history.

## [Unreleased]
- (Planned) Expand the read side in `Ticketing.Query`.
- (Planned) Extra endpoints and queries over the Event Store.

## [0.6.0] - 2025-10-29
### Changed
- Refactor: Better comments and async method implementations in `MongoRepository` (clearer session/transaction handling).
- Refactor: Simplified generic usage in `MongoRepository<T>` and improved `BsonCollectionAttribute` docs.
- Refactor: Updated `IMongoRepository` and `EventModel` properties for consistency.

### Docs
- README: Improved Docker and MongoDB instructions, including vhosts config on Linux.

## [0.4.0] - 2025-10-06
### Changed
- refactor(database): Changed primary key for users from INT to UUID.
- API configuration refactor and general improvements.

### Added
- Registered service configuration and event mapping in infrastructure.

## [0.3.0] - 2025-10-03
### Added
- Feature: `TicketCreate` (command to create tickets with validation, mapping, and event persistence).

### Changed
- Restructured namespaces for event models.
- Aligned repository usage and refined project dependencies.

## [0.2.0] - 2025-10-01
### Added
- Implemented infrastructure services and MongoDB configuration.

## [0.1.0] - 2025-09-30
### Added
- Initial MongoDB setup and base repositories.
- Domain layer with interfaces for `MongoRepository` and decoupled data access.
- Initial project structure with Event-Driven Architecture (EDA).

---

## Key project pieces (current view)

### Architecture and solution
- Solution with three main projects:
	- `Common/Common.Core`: Shared types (events and messages) across services.
	- `Projects/Ticketing.Command`: Write-side microservice (Command) using MediatR, FluentValidation, AutoMapper, and MongoDB.
	- `Projects/Ticketing.Query`: Read-side microservice (Query), currently a skeleton.

### Event persistence (Event Store)
- Document `EventModel` (`Projects/Ticketing.Command/Domain/EventModels/EventModel.cs`):
	- Decorated with `[BsonCollection("eventStores")]` to resolve the MongoDB collection.
	- Main fields: `TimeStamp`, `AggregateIdentifier`, `AggregateType`, `Version`, `EventType`, and `EventData` (`BaseEvent`).
	- Uses base `Document` and maps fields via `BsonElement` attributes.

- Generic repository `MongoRepository<T>` (`Projects/Ticketing.Command/Infrastructure/Repositories/MongoRepository.cs`):
	- Resolves the collection through the `BsonCollectionAttribute`.
	- Exposes `AsQueryable()` for LINQ over Mongo.
	- Manages sessions/transactions: `BeginSessionAsync`, `BeginTransaction`, `CommitTransactionAsync`, `RollBackTransactionAsync`, `DisposeSession`.
	- `InsertOneAsync` supports transactions.

- Contracts:
	- `IMongoRepository<T>` and `ISession` define the repo contract and transaction lifecycle.
	- `IEventModelRepository` specialized for `EventModel` (implemented by `EventModelRepository`).

### Common events and messages
- `Common.Core/Events/BaseEvent.cs`: Base event class inheriting from `Message`, exposes `Version` and `Type`. The ctor sets `Type`.
- `Common.Core/Events/TicketCreatedEvent.cs`: Domain event for ticket creation. Required props: `UserName`, `DetailError`; optional `TypeError`.
- `Common.Core/Messages/Message.cs`: Base class with `[BsonId] string Id` for Mongo identity.

### Feature: ticket creation (Command)
- Location: `Projects/Ticketing.Command/Features/Tickets/TicketCreate.cs`.
- Components:
	- `TicketCreateRequest` (class with primary constructor and public properties): input data (`Username`, `TypeError`, `DetailError`).
	- `TicketCreateCommand` (record) implements MediatR `IRequest<bool>`.
	- Validation: `TicketCreateValidator` and `TicketCreateCommandValidator` (FluentValidation) require `Username` and `DetailError`.
	- Handler: `TicketCreateCommandHandler` maps to `TicketCreatedEvent` using AutoMapper and persists an `EventModel` inside a Mongo transaction.
		- Generates `AggregateIdentifier` via `Guid.CreateVersion7(DateTimeOffset.UtcNow)` (time-ordered, .NET 9).
		- Opens session, starts transaction, inserts, and commits. On error, rolls back and disposes the session in `finally`.

- AutoMapper profile: `Projects/Ticketing.Command/Application/Profiles/MappingProfile.cs` defines `CreateMap<TicketCreateRequest, TicketCreatedEvent>()`.

### Service registration
- `ApplicationServiceRegistration` (`Projects/Ticketing.Command/Application/Extension/ApplicationServiceRegistration.cs`):
	- Binds `MongoSettings` from `appsettings.json`.
	- Registers assembly scanning for MediatR, FluentValidation, and AutoMapper.

- `InfrastructureServiceRegistration` (`Projects/Ticketing.Command/Infrastructure/Extension/InfrastructureServiceRegistration.cs`):
	- Registers `BsonClassMap` for `BaseEvent` and `TicketCreatedEvent` (MongoDB serialization).
	- `IMongoClient` as a singleton using `ConnectionStrings:MongoDb`.
	- Repositories: `IMongoRepository<>` (scoped) and `IEventModelRepository` (transient).

### Exposed API (Command)
- `Program.cs` (`Projects/Ticketing.Command/Program.cs`):
	- `POST /api/ticket`: accepts `TicketCreateRequest`, sends `TicketCreateCommand` through MediatR, and returns a `bool` indicating success.
	- Sample endpoints: `/weatherforecast`, `/test`.
	- OpenAPI enabled in Development.

### Local config and dev setup
- `appsettings.json` (Command): Mongo connection string with `replicaSet=rs0` to enable transactions.
- `docker-compose.yml`:
	- `mongodb:7.0` service with single-node replica set (`rs0`) and a healthcheck that auto-initializes the RS.
	- Persistent volumes (`mongodb_data`, `mongodb_config`).
	- Network `microserviceNetwork`.

---

## Notes and considerations
- Mongo transactions require a Replica Set; the provided `docker-compose` sets it up. If running locally, wait for the healthcheck to finish before starting the Command service.
- `Ticketing.Query` is currently a base service; projections/reads will arrive in future versions.
- There's no Kafka integration in the current codebase yet; the repo name and common library suggest it's coming next.

[Unreleased]: #
[0.6.0]: #
[0.4.0]: #
[0.3.0]: #
[0.2.0]: #
[0.1.0]: #

