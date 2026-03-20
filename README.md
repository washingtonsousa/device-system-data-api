# DeviceSystemDataAPI

.NET 9 REST API for device registration and state management. Built with Clean Architecture, CQRS through MediatR, and MySQL for persistence.

## Stack

- .NET 9 / ASP.NET Core
- Entity Framework Core 9 (Pomelo MySQL)
- MediatR 14.1
- MySQL 8.4
- Docker Compose
- xUnit + Moq

## Project structure

```
DeviceSystemDataAPI/           -> Controllers, filters, Program.cs
Application/                   -> Commands and queries (CQRS)
Domain/                        -> Entities, repository contracts
Infrastructure/                -> DbContext, repository implementations
DeviceSystemDataAPI.UnitTests/ -> Unit tests
```

## Endpoints

Base path: `api/DeviceData`

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/` | Paginated list. Query params: `page`, `page_size`, `brand`, `state`, `deviceId` |
| GET | `/{id}` | Get by ID |
| POST | `/` | Create a device |
| PUT | `/{id}` | Full update |
| PATCH | `/{id}` | Update state only |
| DELETE | `/{id}` | Delete a device |

Valid states: `available`, `in-use`, `inactive`

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker](https://docs.docker.com/get-docker/) — required to run MySQL, even for local development

## Getting started

Docker Compose is required to run MySQL. This applies both when running the API through Docker and when running it locally with `dotnet run`.

```bash
docker compose up -d
```

This starts MySQL on port `3306` with a persistent volume. On the first run, `init.sql` creates the table.

> To reset the database: `docker compose down -v` and bring it up again.

### Running with Docker (API + MySQL)

```bash
dotnet publish DeviceSystemDataAPI/DeviceSystemDataAPI.csproj -c Release -o publish
docker compose up -d --build
```

The API will be available at `http://localhost:5000`.

### Running locally (API only)

With MySQL already running from Docker Compose:

```bash
dotnet run --project DeviceSystemDataAPI/DeviceSystemDataAPI.csproj
```

The API will be available at `http://localhost:5036`.

## Tests

```bash
dotnet test
```

29 tests covering domain entity, repository and CQRS handlers.

## Swagger

Interactive API docs available at:

- Local: `http://localhost:5036/swagger`
- Docker: `http://localhost:5000/swagger`

## Postman

The collection `DeviceSystemDataAPI.postman_collection.json` is at the project root, ready to import. The `baseUrl` variable defaults to `http://localhost:5000` (Docker) — change it to `http://localhost:5036` when running locally.

## Configuration

The connection string is set through the `CONN_STRING` environment variable, already configured in `docker-compose.yml` and `launchSettings.json`.
