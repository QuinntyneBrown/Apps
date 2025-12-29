# Fuel Purchase Management - Backend Requirements

## API Endpoints

### POST /api/fuel-purchases
Create a new fuel purchase record.

**Request Body**:
```json
{
  "vehicleId": "string (uuid)",
  "date": "datetime (ISO 8601)",
  "gallons": "decimal (positive)",
  "costPerGallon": "decimal (positive)",
  "totalCost": "decimal (positive)",
  "odometerReading": "integer (positive)",
  "fuelGrade": "enum (Regular, Premium, Diesel, E85)",
  "stationLocation": {
    "name": "string",
    "address": "string",
    "latitude": "decimal",
    "longitude": "decimal"
  },
  "paymentMethod": "enum (Cash, Credit, Debit, App)",
  "isPartialFill": "boolean",
  "tankLevelBefore": "integer (0-100, optional)",
  "tankLevelAfter": "integer (0-100, optional)",
  "notes": "string (optional)"
}
```

**Response**: 201 Created
```json
{
  "id": "string (uuid)",
  "calculatedMPG": "decimal (nullable)",
  "milesSinceLastFill": "integer (nullable)",
  "createdAt": "datetime"
}
```

**Domain Events Triggered**:
- `FuelPurchased`
- `PartialFillUpRecorded` (if isPartialFill)

### GET /api/fuel-purchases
Retrieve fuel purchase history with filtering and pagination.

**Query Parameters**:
- `vehicleId` (required): Filter by vehicle
- `startDate` (optional): Filter from date
- `endDate` (optional): Filter to date
- `page` (default: 1)
- `pageSize` (default: 25, max: 100)
- `sortBy` (default: date, options: date, totalCost, mpg)
- `sortOrder` (default: desc)

**Response**: 200 OK
```json
{
  "data": [
    {
      "id": "string",
      "date": "datetime",
      "gallons": "decimal",
      "totalCost": "decimal",
      "odometerReading": "integer",
      "mpg": "decimal (nullable)",
      "stationName": "string"
    }
  ],
  "pagination": {
    "page": "integer",
    "pageSize": "integer",
    "totalRecords": "integer",
    "totalPages": "integer"
  }
}
```

### PUT /api/fuel-purchases/{id}
Update an existing fuel purchase.

**Domain Events Triggered**:
- `FuelPurchaseUpdated`
- `FuelEconomyRecalculated`

### DELETE /api/fuel-purchases/{id}
Delete a fuel purchase record.

**Domain Events Triggered**:
- `FuelPurchaseDeleted`
- `FuelEconomyRecalculated`

### POST /api/fuel-stations/{id}/rate
Rate a fuel station.

**Request Body**:
```json
{
  "priceCompetitiveness": "integer (1-5)",
  "cleanliness": "integer (1-5)",
  "amenities": "integer (1-5)",
  "service": "integer (1-5)",
  "wouldReturn": "boolean",
  "notes": "string (optional)"
}
```

**Domain Events Triggered**:
- `FuelStationRated`

### POST /api/fuel-prices
Record local fuel price.

**Request Body**:
```json
{
  "stationId": "string (uuid)",
  "date": "datetime",
  "regularPrice": "decimal (optional)",
  "premiumPrice": "decimal (optional)",
  "dieselPrice": "decimal (optional)"
}
```

**Domain Events Triggered**:
- `FuelPriceTracked`

## Domain Models

### FuelPurchase
```csharp
public class FuelPurchase : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public decimal Gallons { get; private set; }
    public decimal CostPerGallon { get; private set; }
    public decimal TotalCost { get; private set; }
    public int OdometerReading { get; private set; }
    public FuelGrade FuelGrade { get; private set; }
    public FuelStationLocation StationLocation { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public bool IsPartialFill { get; private set; }
    public int? TankLevelBefore { get; private set; }
    public int? TankLevelAfter { get; private set; }
    public string Notes { get; private set; }
    public decimal? CalculatedMPG { get; private set; }
    public int? MilesSinceLastFill { get; private set; }

    // Domain events
    public void RecordFuelPurchase() { /* raises FuelPurchased */ }
    public void CalculateMPG(FuelPurchase previousFill) { /* calculates and raises FuelEconomyCalculated */ }
}
```

### FuelStation
```csharp
public class FuelStation : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Address Location { get; private set; }
    public GeoCoordinates Coordinates { get; private set; }
    public List<FuelStationRating> Ratings { get; private set; }
    public decimal AverageRating { get; private set; }

    public void AddRating(FuelStationRating rating) { /* raises FuelStationRated */ }
}
```

## Business Logic

### MPG Calculation Rules
1. MPG can only be calculated when there are two consecutive full fill-ups
2. Partial fills are tracked but skipped in MPG calculations
3. Formula: MPG = (Current Odometer - Previous Odometer) / Gallons Purchased
4. Minimum reasonable MPG: 5, Maximum reasonable MPG: 100
5. Flag suspicious values for review (outside vehicle's typical range by >50%)

### Data Validation
- Odometer reading must be greater than previous reading
- Date cannot be in the future
- Gallons must be positive and realistic (< tank capacity)
- Cost per gallon must be within reasonable range ($1-$15)
- Tank level percentages must be 0-100
- Partial fill must have tank level data

### Performance Considerations
- Index on: vehicleId, purchaseDate, userId
- Eager load station data for list views
- Cache frequently accessed station information
- Batch MPG recalculations when updating historical records

## Domain Events

### FuelPurchased
```csharp
public class FuelPurchased : DomainEvent
{
    public Guid FuelPurchaseId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Gallons { get; set; }
    public decimal TotalCost { get; set; }
    public int OdometerReading { get; set; }
    public FuelGrade FuelGrade { get; set; }
    public string StationName { get; set; }
}
```

### PartialFillUpRecorded
```csharp
public class PartialFillUpRecorded : DomainEvent
{
    public Guid PartialFillId { get; set; }
    public Guid VehicleId { get; set; }
    public decimal GallonsAdded { get; set; }
    public int TankLevelBefore { get; set; }
    public int TankLevelAfter { get; set; }
    public string Reason { get; set; }
}
```

### FuelStationRated
```csharp
public class FuelStationRated : DomainEvent
{
    public Guid RatingId { get; set; }
    public Guid StationId { get; set; }
    public DateTime RatingDate { get; set; }
    public int PriceCompetitiveness { get; set; }
    public int Cleanliness { get; set; }
    public bool WouldReturn { get; set; }
}
```

### FuelPriceTracked
```csharp
public class FuelPriceTracked : DomainEvent
{
    public Guid PriceRecordId { get; set; }
    public Guid StationId { get; set; }
    public DateTime RecordDate { get; set; }
    public decimal RegularPrice { get; set; }
    public decimal PremiumPrice { get; set; }
    public string PriceTrend { get; set; }
}
```

## Event Handlers

### When FuelPurchased
1. Calculate MPG if conditions met
2. Update vehicle's last fill-up reference
3. Trigger expense tracking update
4. Check for personal best MPG
5. Update running averages

### When PartialFillUpRecorded
1. Update fuel log
2. Track partial fill patterns
3. Adjust tank level estimates

### When FuelStationRated
1. Update station average rating
2. Add to user's station preferences
3. Update station recommendations

### When FuelPriceTracked
1. Check for price spike conditions
2. Update historical price trends
3. Notify users of significant changes
4. Update budget projections

## Database Schema

### fuel_purchases
- id (uuid, PK)
- user_id (uuid, FK)
- vehicle_id (uuid, FK)
- purchase_date (timestamp)
- gallons (decimal)
- cost_per_gallon (decimal)
- total_cost (decimal)
- odometer_reading (integer)
- fuel_grade (varchar)
- fuel_station_id (uuid, FK, nullable)
- payment_method (varchar)
- is_partial_fill (boolean)
- tank_level_before (integer, nullable)
- tank_level_after (integer, nullable)
- notes (text, nullable)
- calculated_mpg (decimal, nullable)
- miles_since_last_fill (integer, nullable)
- created_at (timestamp)
- updated_at (timestamp)

**Indexes**:
- idx_fuel_purchases_vehicle_date (vehicle_id, purchase_date DESC)
- idx_fuel_purchases_user (user_id)
- idx_fuel_purchases_station (fuel_station_id)

### fuel_stations
- id (uuid, PK)
- name (varchar)
- address (varchar)
- city (varchar)
- state (varchar)
- zip (varchar)
- latitude (decimal)
- longitude (decimal)
- created_at (timestamp)

**Indexes**:
- idx_fuel_stations_location (latitude, longitude)
- idx_fuel_stations_city_state (city, state)

### fuel_station_ratings
- id (uuid, PK)
- fuel_station_id (uuid, FK)
- user_id (uuid, FK)
- rating_date (timestamp)
- price_competitiveness (integer)
- cleanliness (integer)
- amenities (integer)
- service (integer)
- would_return (boolean)
- notes (text, nullable)

### fuel_price_history
- id (uuid, PK)
- fuel_station_id (uuid, FK)
- record_date (timestamp)
- regular_price (decimal, nullable)
- premium_price (decimal, nullable)
- diesel_price (decimal, nullable)

**Indexes**:
- idx_fuel_price_station_date (fuel_station_id, record_date DESC)
