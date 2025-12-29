# Wine Collection - Backend Requirements

## Overview
Core functionality for cataloging wine bottles, tracking inventory, managing cellar locations, and monitoring collection value.

## Domain Model

### WineBottle Aggregate
- **BottleId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **WineName**: Wine name (string, max 200)
- **Producer**: Winery/producer (string, max 200)
- **Vintage**: Year (int)
- **Region**: Wine region (string, max 100)
- **Varietal**: Grape variety (string, max 100)
- **AcquisitionDate**: Purchase date (DateTime)
- **AcquisitionCost**: Purchase price (decimal)
- **CurrentValue**: Estimated value (decimal)
- **Quantity**: Number of bottles (int)
- **LocationId**: Storage location (Guid)
- **DrinkingWindowStart**: Optimal start date (DateTime, nullable)
- **DrinkingWindowEnd**: Optimal end date (DateTime, nullable)
- **Status**: Status (enum: InCellar, Consumed, Damaged, Sold)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

### CellarLocation Entity
- **LocationId**: Unique identifier (Guid)
- **Name**: Location name (string, max 100)
- **Zone**: Cellar zone (string)
- **RackNumber**: Rack identifier (int, nullable)
- **ShelfNumber**: Shelf identifier (int, nullable)
- **Position**: Position number (int, nullable)
- **Capacity**: Max bottles (int)
- **CurrentCount**: Current bottles (int)
- **Temperature**: Target temp (decimal, nullable)

## Commands

### AcquireWineBottleCommand
- Records new wine acquisition
- Validates wine details
- Raises **WineBottleAcquired** event

### AssignBottleLocationCommand
- Assigns bottle to cellar location
- Updates location capacity
- Raises **BottleLocationAssigned** event

### OpenBottleCommand
- Records bottle consumption
- Updates inventory count
- Raises **BottleOpened** event

### UpdateWineValueCommand
- Updates current market value
- Tracks value appreciation
- Raises **WineValueAssessed** event

## Queries

### GetWinesByUserIdQuery
- Returns all user's wines

### GetWinesByLocationQuery
- Returns wines in specific location

### GetWinesInDrinkingWindowQuery
- Returns wines at peak maturity

### GetCollectionValueQuery
- Calculates total collection worth

## Domain Events

### WineBottleAcquired
```csharp
public class WineBottleAcquired : DomainEvent
{
    public Guid BottleId { get; set; }
    public string WineName { get; set; }
    public string Producer { get; set; }
    public int Vintage { get; set; }
    public string Region { get; set; }
    public decimal AcquisitionCost { get; set; }
    public int Quantity { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### BottleOpened
```csharp
public class BottleOpened : DomainEvent
{
    public Guid BottleId { get; set; }
    public DateTime OpenDate { get; set; }
    public string Occasion { get; set; }
    public List<string> Companions { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### WineValueAssessed
```csharp
public class WineValueAssessed : DomainEvent
{
    public Guid BottleId { get; set; }
    public decimal CurrentMarketValue { get; set; }
    public decimal AppreciationSincePurchase { get; set; }
    public string ValuationSource { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/wines
- Acquires wine bottle
- Returns: 201 Created

### PUT /api/wines/{id}
- Updates wine details
- Returns: 200 OK

### GET /api/wines/{id}
- Gets wine details
- Returns: 200 OK

### GET /api/wines
- Gets user's collection
- Returns: 200 OK

### POST /api/wines/{id}/open
- Records consumption
- Returns: 200 OK

### GET /api/wines/drinking-window
- Gets wines ready to drink
- Returns: 200 OK

## Business Rules

1. **Vintage Validation**: Year must be 1800-current year
2. **Quantity Management**: Cannot open more bottles than available
3. **Location Capacity**: Cannot exceed location capacity
4. **Drinking Window**: Start must be before end date

## Data Persistence

### WineBottles Table
```sql
CREATE TABLE WineBottles (
    BottleId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    WineName NVARCHAR(200) NOT NULL,
    Producer NVARCHAR(200) NOT NULL,
    Vintage INT NOT NULL,
    Region NVARCHAR(100) NOT NULL,
    Varietal NVARCHAR(100) NOT NULL,
    AcquisitionDate DATE NOT NULL,
    AcquisitionCost DECIMAL(10,2) NOT NULL,
    CurrentValue DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    LocationId UNIQUEIDENTIFIER NOT NULL,
    DrinkingWindowStart DATE NULL,
    DrinkingWindowEnd DATE NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'InCellar',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_WineBottles_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_WineBottles_Locations FOREIGN KEY (LocationId) REFERENCES CellarLocations(LocationId),
    INDEX IX_WineBottles_UserId (UserId),
    INDEX IX_WineBottles_Status (Status),
    INDEX IX_WineBottles_DrinkingWindow (DrinkingWindowStart, DrinkingWindowEnd)
);
```

### CellarLocations Table
```sql
CREATE TABLE CellarLocations (
    LocationId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Zone NVARCHAR(50) NULL,
    RackNumber INT NULL,
    ShelfNumber INT NULL,
    Position INT NULL,
    Capacity INT NOT NULL,
    CurrentCount INT NOT NULL DEFAULT 0,
    Temperature DECIMAL(5,2) NULL,

    CONSTRAINT FK_CellarLocations_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_CellarLocations_UserId (UserId)
);
```
