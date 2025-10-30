# Changelog

This file follows the Keep a Changelog format and Semantic Versioning (SemVer) when applicable.

All dates use YYYY-MM-DD and are based on this repo's commit history.

## [Unreleased]
### Fixed
- **MongoDB Connection on Linux**: Corrected the replica set initialization in `docker-compose.yml`. The replica set member host was changed from `host.docker.internal` to `localhost`. This ensures that clients running on the host machine (like the .NET API or VS Code extensions) can correctly resolve and connect to the primary node when using `replicaSet=rs0` in the connection string.

### Planned
- Expand the read side in `Ticketing.Query`.
- Extra endpoints and queries over the Event Store.

## [0.6.0] - 2025-10-29
// ...existing code...