# Parts Management - Backend Requirements

## Overview
API for tracking parts sourcing, ordering, receiving, installation, and returns.

## Domain Events
- **PartSourced**: Parts source identified
- **PartOrdered**: Part order placed
- **PartReceived**: Part delivered
- **PartInstalled**: Part fitted to vehicle
- **PartReturned**: Part sent back to supplier

## API Endpoints

### Parts Catalog
- `POST /api/projects/{id}/parts` - Add part to catalog
- `GET /api/projects/{id}/parts` - List project parts
- `GET /api/parts/{partId}` - Get part details
- `PUT /api/parts/{partId}` - Update part info
- `DELETE /api/parts/{partId}` - Remove part

### Orders
- `POST /api/parts/{partId}/order` - Place order
- `GET /api/projects/{id}/orders` - List orders
- `PUT /api/orders/{orderId}` - Update order
- `POST /api/orders/{orderId}/receive` - Mark received
- `POST /api/orders/{orderId}/return` - Initiate return

### Installation
- `POST /api/parts/{partId}/install` - Record installation
- `GET /api/projects/{id}/installations` - List installations

### Suppliers
- `GET /api/suppliers` - List suppliers
- `POST /api/suppliers` - Add supplier
- `GET /api/suppliers/{id}/parts` - Parts from supplier

## Data Models

### Part
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "partName": "string",
    "partNumber": "string",
    "category": "enum[Engine, Transmission, Body, Interior, Electrical, Suspension, Brakes, Trim]",
    "condition": "enum[NOS, Used, Reproduction]",
    "supplierId": "guid",
    "price": "decimal",
    "availability": "enum[InStock, Backorder, Special Order]",
    "status": "enum[Sourced, Ordered, Received, Installed, Returned]"
}
```

### Order
```csharp
{
    "id": "guid",
    "partId": "guid",
    "orderDate": "datetime",
    "supplierId": "guid",
    "cost": "decimal",
    "quantity": "int",
    "shippingCost": "decimal",
    "expectedDelivery": "datetime",
    "trackingNumber": "string",
    "receivedDate": "datetime?",
    "qualityCheckPassed": "boolean?"
}
```

### Installation
```csharp
{
    "id": "guid",
    "partId": "guid",
    "installDate": "datetime",
    "difficulty": "enum[Easy, Moderate, Difficult, Expert]",
    "fitQuality": "enum[Perfect, Good, Acceptable, Poor]",
    "timeSpent": "decimal",
    "helperRequired": "boolean",
    "notes": "string"
}
```

## Business Rules
- Part numbers must be unique per project
- Cannot order already installed parts
- Received parts must pass quality check
- Installation requires part to be received
- Returns must be within supplier policy period

## Validation Rules
- Part name: 1-100 characters
- Part number: max 50 characters
- Price/cost: >= 0
- Quantity: > 0
- Tracking number: max 100 characters
