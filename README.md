# DeviceSystemDataAPI

A .NET 9.0 Web API that handles device registration and status tracking. It exposes a RESTful interface over a MySQL database, structured around Clean Architecture with CQRS via MediatR.

## Stack

- .NET 9.0 / ASP.NET Core
- Entity Framework Core 9.0 + [Pomelo MySQL provider](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
- MediatR 14.1
- MySQL 8.4
- Docker

## Structure

```
DeviceSystemDataAPI/   → Controllers, filters, Program.cs
Application/           → Commands, queries and their handlers (CQRS)
Domain/                → Entities, repository contracts
Infrastructure/        → EF Core context, repository implementations
```

Dependencies go **inward**: Presentation → Application → Domain ← Infrastructure.

## Endpoints

Base path: `api/devicesystemdataapi`

| Method | Route | What it does |
|---|---|---|
| GET | `/` | Paged list — accepts `page`, `deviceId`, `brand` as query params |
| GET | `/{id}` | Single device by ID |
| POST | `/` | Create a device |
| PUT | `/{id}` | Replace a device |
| PATCH | `/{id}` | Partial update |
| DELETE | `/{id}` | Delete a device |

## Running with Docker

You'll need the [.NET 9.0 SDK](https://learn.microsoft.com/en-us/dotnet/core/install/) installed locally to publish, and Docker to run the containers.

**Publish first** — the Dockerfile copies from a `publish/` folder, so you need to generate it:

```bash
dotnet publish DeviceSystemDataAPI/DeviceSystemDataAPI.csproj -c Release -o publish
```

**Then bring everything up:**

```bash
docker compose up -d --build
```

This starts two containers on the default Docker network:
- The API on `http://localhost:5000`
- MySQL 8.4 on port `3306` with a persistent volume

**Useful commands:**

```bash
docker compose ps              # check status
docker compose logs -f api     # tail the API logs
docker compose down            # stop everything
docker compose down -v         # stop and wipe the database volume
```

## Configuration

The connection string is passed via the `CONN_STRING` environment variable, already wired in `docker-compose.yml`. If you need to point the API to a different database, just override it there.

## Docs and references

- [.NET SDK](https://learn.microsoft.com/en-us/dotnet/core/sdk)
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
- [MediatR](https://github.com/jbogard/MediatR)
- [Docker Compose file reference](https://docs.docker.com/compose/compose-file/)
