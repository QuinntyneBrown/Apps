# Core Feature - Backend Requirements

## API Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/items | List all items |
| GET | /api/items/{id} | Get item details |
| POST | /api/items | Create item |
| PUT | /api/items/{id} | Update item |
| DELETE | /api/items/{id} | Delete item |

## Domain Events
See domain-events.md for full event list

## Database Schema
```sql
CREATE TABLE Items (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(50),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);
```
