# Appliance Management - Backend Requirements

## API Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/appliances | List all appliances |
| POST | /api/appliances | Register new appliance |
| PUT | /api/appliances/{id} | Update appliance |
| DELETE | /api/appliances/{id} | Retire appliance |
| PUT | /api/appliances/{id}/relocate | Move appliance |

## Domain Events
- ApplianceRegistered
- ApplianceRelocated
- ApplianceRetired
- ApplianceReplacementPlanned

## Database Schema
```sql
CREATE TABLE Appliances (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Brand NVARCHAR(100),
    ModelNumber NVARCHAR(100),
    SerialNumber NVARCHAR(100),
    PurchaseDate DATE,
    PurchasePrice DECIMAL(10,2),
    Retailer NVARCHAR(200),
    Location NVARCHAR(200),
    Status NVARCHAR(50),
    PhotoUrl NVARCHAR(1000),
    CreatedAt DATETIME2 NOT NULL
);
```
