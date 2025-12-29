# Item Management - Backend Requirements

## Overview
The Item Management feature provides core functionality for cataloging household possessions, tracking details, and maintaining item lifecycle from acquisition to disposal.

## Domain Model

### Item Aggregate
- **ItemId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **Name**: Item name (string, max 200)
- **Description**: Detailed description (string, max 2000)
- **CategoryId**: Category reference (Guid)
- **PurchaseDate**: Date of purchase (DateTime, nullable)
- **PurchasePrice**: Original cost (decimal, nullable)
- **CurrentValue**: Estimated current value (decimal)
- **LocationId**: Storage location (Guid)
- **SerialNumber**: Serial/model number (string, nullable, max 100)
- **Condition**: Item condition (enum: Excellent, Good, Fair, Poor)
- **Status**: Item status (enum: Active, Disposed, Sold, Donated, Lost, Stolen)
- **Photos**: List<PhotoReference>
- **ReceiptId**: Receipt reference (Guid, nullable)
- **Notes**: Additional notes (string, max 5000)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

### Category Entity
- **CategoryId**: Unique identifier (Guid)
- **Name**: Category name (string, max 100)
- **ParentCategoryId**: Parent category (Guid, nullable)
- **DepreciationRate**: Annual depreciation percentage (decimal)
- **InsuranceClassification**: Insurance category (string)
- **IconName**: UI icon reference (string)

### PhotoReference Value Object
- **PhotoId**: Unique identifier (Guid)
- **Url**: Storage URL (string)
- **UploadedAt**: Upload timestamp (DateTime)
- **IsPrimary**: Primary photo flag (bool)
- **Caption**: Photo description (string, max 200)

## Commands

### AddItemCommand
- Adds a new item to inventory
- Validates required fields (Name, CategoryId, LocationId)
- Calculates initial current value from purchase price
- Raises **ItemAdded** event

### UpdateItemCommand
- Updates existing item details
- Validates item exists and user owns it
- Tracks changed fields
- Raises **ItemUpdated** event

### RemoveItemCommand
- Marks item as removed from active inventory
- Records removal reason and disposal method
- Calculates ownership duration
- Raises **ItemRemoved** event

### RelocateItemCommand
- Moves item to different location
- Records previous and new locations
- Raises **ItemRelocated** event

### BulkImportItemsCommand
- Imports multiple items from CSV/Excel
- Validates data format
- Creates items in batch
- Returns import summary

## Queries

### GetItemByIdQuery
- Returns single item with full details
- Includes photos, category, location

### GetItemsByUserIdQuery
- Returns all user's items
- Supports pagination, sorting, filtering
- Filter by category, location, status, value range

### SearchItemsQuery
- Full-text search across name, description, serial number
- Returns ranked results

### GetItemsByCategoryQuery
- Returns items in specific category
- Includes subcategories if requested

### GetItemsByLocationQuery
- Returns items at specific location
- Used for location inventory counts

### GetHighValueItemsQuery
- Returns items above specified value threshold
- Used for insurance documentation

## Domain Events

### ItemAdded
```csharp
public class ItemAdded : DomainEvent
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public decimal CurrentValue { get; set; }
    public Guid LocationId { get; set; }
    public string SerialNumber { get; set; }
    public List<string> PhotoUrls { get; set; }
    public Guid? ReceiptId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### ItemUpdated
```csharp
public class ItemUpdated : DomainEvent
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public Dictionary<string, object> PreviousValues { get; set; }
    public Dictionary<string, object> NewValues { get; set; }
    public DateTime UpdateTimestamp { get; set; }
    public string UpdateReason { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### ItemRemoved
```csharp
public class ItemRemoved : DomainEvent
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public string ItemName { get; set; }
    public DateTime RemovalDate { get; set; }
    public string RemovalReason { get; set; }
    public string DisposalMethod { get; set; }
    public decimal FinalValue { get; set; }
    public TimeSpan OwnershipDuration { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### ItemRelocated
```csharp
public class ItemRelocated : DomainEvent
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public string ItemName { get; set; }
    public Guid PreviousLocationId { get; set; }
    public string PreviousLocationName { get; set; }
    public Guid NewLocationId { get; set; }
    public string NewLocationName { get; set; }
    public DateTime RelocationDate { get; set; }
    public string ReasonForMove { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/items
- Creates new item
- Request body: AddItemRequest
- Returns: 201 Created with item details

### PUT /api/items/{itemId}
- Updates existing item
- Request body: UpdateItemRequest
- Returns: 200 OK with updated item

### DELETE /api/items/{itemId}
- Removes item from inventory
- Request body: RemoveItemRequest (reason, disposal method)
- Returns: 204 No Content

### GET /api/items/{itemId}
- Gets item details
- Returns: 200 OK with full item data

### GET /api/items
- Gets user's items with filtering
- Query params: page, pageSize, categoryId, locationId, status, minValue, maxValue, search
- Returns: 200 OK with paginated results

### POST /api/items/{itemId}/relocate
- Relocates item to new location
- Request body: { locationId, reason }
- Returns: 200 OK

### POST /api/items/bulk-import
- Imports items from file
- Request body: multipart/form-data with CSV/Excel file
- Returns: 200 OK with import summary

### GET /api/items/categories
- Gets all categories
- Returns: 200 OK with category tree

## Business Rules

1. **Name Required**: Item must have a name
2. **Category Required**: Item must belong to a category
3. **Location Required**: Item must have an assigned location
4. **Current Value**: Defaults to purchase price if not specified
5. **Serial Number Uniqueness**: Serial numbers should be unique within user's inventory
6. **Photo Limit**: Maximum 10 photos per item
7. **Status Transitions**: Only allow valid status transitions (Active → Disposed/Sold/Donated/Lost/Stolen)
8. **Ownership**: Users can only manage their own items
9. **Removal Permanence**: Removed items are soft-deleted and archived

## Validation Rules

- Name: Required, 1-200 characters
- Description: Optional, max 2000 characters
- Purchase price: Non-negative
- Current value: Non-negative
- Serial number: Alphanumeric, max 100 characters
- Notes: Max 5000 characters

## Data Persistence

### Items Table
```sql
CREATE TABLE Items (
    ItemId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(2000) NULL,
    CategoryId UNIQUEIDENTIFIER NOT NULL,
    PurchaseDate DATE NULL,
    PurchasePrice DECIMAL(18,2) NULL,
    CurrentValue DECIMAL(18,2) NOT NULL,
    LocationId UNIQUEIDENTIFIER NOT NULL,
    SerialNumber NVARCHAR(100) NULL,
    Condition NVARCHAR(20) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Active',
    ReceiptId UNIQUEIDENTIFIER NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Items_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_Items_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    CONSTRAINT FK_Items_Locations FOREIGN KEY (LocationId) REFERENCES Locations(LocationId),
    INDEX IX_Items_UserId (UserId),
    INDEX IX_Items_CategoryId (CategoryId),
    INDEX IX_Items_LocationId (LocationId),
    INDEX IX_Items_Status (Status),
    INDEX IX_Items_SerialNumber (SerialNumber)
);
```

### Categories Table
```sql
CREATE TABLE Categories (
    CategoryId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    ParentCategoryId UNIQUEIDENTIFIER NULL,
    DepreciationRate DECIMAL(5,2) NOT NULL DEFAULT 0,
    InsuranceClassification NVARCHAR(100) NULL,
    IconName NVARCHAR(50) NULL,

    CONSTRAINT FK_Categories_Parent FOREIGN KEY (ParentCategoryId) REFERENCES Categories(CategoryId),
    INDEX IX_Categories_ParentId (ParentCategoryId)
);
```

### ItemPhotos Table
```sql
CREATE TABLE ItemPhotos (
    PhotoId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ItemId UNIQUEIDENTIFIER NOT NULL,
    Url NVARCHAR(500) NOT NULL,
    UploadedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    IsPrimary BIT NOT NULL DEFAULT 0,
    Caption NVARCHAR(200) NULL,

    CONSTRAINT FK_ItemPhotos_Items FOREIGN KEY (ItemId) REFERENCES Items(ItemId) ON DELETE CASCADE,
    INDEX IX_ItemPhotos_ItemId (ItemId)
);
```
