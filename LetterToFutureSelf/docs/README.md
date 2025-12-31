# Letter to Future Self

Write messages to your future self with scheduled delivery dates.

## Tech Stack

- **Database**: SQL Server
- **Backend**: ASP.NET Core Web API
- **Frontend**: Angular 18+
- **Authentication**: ASP.NET Core Identity


## Multi-Tenancy

This application supports multi-tenant architecture with complete data isolation:

- **TenantId** property on all aggregate entities
- **Automatic query filtering** by tenant
- **JWT/Header-based** tenant identification
- **Row-level security** for data isolation


## Getting Started

1. Clone the repository
2. Set up SQL Server database
3. Update connection strings in appsettings.json
4. Run migrations
5. Start the API and frontend

## Documentation

- [Domain Events](domain-events.md)
