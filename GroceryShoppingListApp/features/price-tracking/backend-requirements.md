# Price Tracking - Backend Requirements

## Overview
The Price Tracking feature monitors item prices across stores and time, providing users with price history, trends, and savings opportunities.

## Domain Events

### ItemPriceRecorded
**Trigger:** When a user purchases an item and records the actual price
**Payload:**
```json
{
  "priceId": "uuid",
  "itemName": "string",
  "userId": "uuid",
  "price": "decimal",
  "storeName": "string",
  "purchaseDate": "datetime",
  "quantity": "integer",
  "unit": "string"
}
```

### PriceIncreaseDetected
**Trigger:** When system detects item price has increased significantly
**Payload:**
```json
{
  "itemName": "string",
  "storeName": "string",
  "previousPrice": "decimal",
  "currentPrice": "decimal",
  "increasePercentage": "decimal",
  "detectedAt": "datetime"
}
```

### DealIdentified
**Trigger:** When system finds a price significantly lower than average
**Payload:**
```json
{
  "itemName": "string",
  "storeName": "string",
  "regularPrice": "decimal",
  "salePrice": "decimal",
  "savingsAmount": "decimal",
  "savingsPercentage": "decimal",
  "validFrom": "datetime",
  "validTo": "datetime?"
}
```

### BestPriceStoreIdentified
**Trigger:** When analysis determines which store has best price
**Payload:**
```json
{
  "itemName": "string",
  "bestStoreName": "string",
  "bestPrice": "decimal",
  "comparisonStores": [
    {
      "storeName": "string",
      "price": "decimal"
    }
  ],
  "analysisDate": "datetime"
}
```

## API Endpoints

### POST /api/price-tracking/record
Record a price observation
- **Request Body:**
  ```json
  {
    "itemName": "string",
    "price": "decimal",
    "storeName": "string",
    "purchaseDate": "datetime",
    "quantity": "integer",
    "unit": "string"
  }
  ```
- **Response:** 201 Created

### GET /api/price-tracking/items/{itemName}/history
Get price history for an item
- **Query Parameters:**
  - `storeName`: string (optional)
  - `startDate`: datetime
  - `endDate`: datetime
  - `limit`: integer (default 50)
- **Response:** 200 OK
  ```json
  {
    "itemName": "string",
    "priceHistory": [
      {
        "price": "decimal",
        "storeName": "string",
        "date": "datetime",
        "userId": "uuid"
      }
    ],
    "averagePrice": "decimal",
    "lowestPrice": "decimal",
    "highestPrice": "decimal"
  }
  ```

### GET /api/price-tracking/items/{itemName}/comparison
Compare prices across stores
- **Response:** 200 OK
  ```json
  {
    "itemName": "string",
    "storePrices": [
      {
        "storeName": "string",
        "currentPrice": "decimal",
        "lastUpdated": "datetime",
        "trend": "increasing | decreasing | stable"
      }
    ],
    "recommendedStore": "string"
  }
  ```

### GET /api/price-tracking/deals
Get current deals and savings opportunities
- **Query Parameters:**
  - `category`: string
  - `minSavings`: decimal
  - `storeName`: string
- **Response:** 200 OK
  ```json
  {
    "deals": [
      {
        "itemName": "string",
        "storeName": "string",
        "regularPrice": "decimal",
        "salePrice": "decimal",
        "savingsPercentage": "decimal",
        "validUntil": "datetime?"
      }
    ]
  }
  ```

### GET /api/price-tracking/trends
Get price trends for items in user's lists
- **Response:** 200 OK
  ```json
  {
    "trends": [
      {
        "itemName": "string",
        "trend": "increasing | decreasing | stable",
        "percentageChange": "decimal",
        "periodDays": "integer"
      }
    ]
  }
  ```

### GET /api/price-tracking/statistics
Get user's price tracking statistics
- **Response:** 200 OK
  ```json
  {
    "totalSavings": "decimal",
    "averageSavingsPerTrip": "decimal",
    "mostExpensiveItem": "string",
    "biggestPriceIncrease": {
      "itemName": "string",
      "increase": "decimal"
    },
    "bestDealFound": {
      "itemName": "string",
      "savings": "decimal"
    }
  }
  ```

## Database Schema

### price_records Table
```sql
CREATE TABLE price_records (
    price_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    item_name VARCHAR(255) NOT NULL,
    normalized_item_name VARCHAR(255) NOT NULL, -- For matching variations
    user_id UUID NOT NULL REFERENCES users(user_id),
    price DECIMAL(10, 2) NOT NULL,
    store_name VARCHAR(255) NOT NULL,
    purchase_date TIMESTAMP NOT NULL,
    quantity INTEGER DEFAULT 1,
    unit VARCHAR(50),
    category VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_price_records_item ON price_records(normalized_item_name);
CREATE INDEX idx_price_records_store ON price_records(store_name);
CREATE INDEX idx_price_records_date ON price_records(purchase_date);
CREATE INDEX idx_price_records_user ON price_records(user_id);
```

### price_alerts Table
```sql
CREATE TABLE price_alerts (
    alert_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(user_id),
    item_name VARCHAR(255) NOT NULL,
    alert_type VARCHAR(50) NOT NULL, -- 'price_drop', 'price_increase', 'deal'
    threshold_percentage DECIMAL(5, 2),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_price_alerts_user ON price_alerts(user_id);
CREATE INDEX idx_price_alerts_active ON price_alerts(is_active);
```

### store_averages Table (Materialized View)
```sql
CREATE MATERIALIZED VIEW store_averages AS
SELECT
    normalized_item_name,
    store_name,
    AVG(price) as average_price,
    MIN(price) as lowest_price,
    MAX(price) as highest_price,
    COUNT(*) as sample_count,
    MAX(purchase_date) as last_updated
FROM price_records
WHERE purchase_date >= CURRENT_DATE - INTERVAL '90 days'
GROUP BY normalized_item_name, store_name;

CREATE INDEX idx_store_averages_item ON store_averages(normalized_item_name);
CREATE INDEX idx_store_averages_store ON store_averages(store_name);

-- Refresh daily
REFRESH MATERIALIZED VIEW CONCURRENTLY store_averages;
```

## Business Rules

1. **Price Recording**
   - Prices automatically recorded when items are marked as purchased
   - Manual price recording allowed
   - Prices normalized to standard units for comparison
   - Historical prices never deleted (for trend analysis)

2. **Price Comparison**
   - Compare prices within same unit of measurement
   - Normalize to per-unit price (e.g., price per lb, per oz)
   - Use data from last 90 days for averages
   - Require minimum 3 data points for reliable trends

3. **Price Increase Detection**
   - Alert when price increases >10% from recent average
   - Compare against user's own history and crowd data
   - Consider seasonal variations
   - Notify user when adding item to list

4. **Deal Identification**
   - Deal = price >15% below recent average
   - Consider frequency of purchases
   - Alert for frequently purchased items
   - Time-limited deals tracked separately

5. **Best Price Store**
   - Based on average prices across all items
   - Weight by user's purchase frequency
   - Consider distance/convenience (future)
   - Update weekly

## Analytics & Machine Learning

### Price Prediction
- Predict future price trends using historical data
- Seasonal adjustment for produce and holiday items
- Alert users to buy now or wait

### Personalized Recommendations
- Learn user's preferred brands and stores
- Suggest similar items with better prices
- Identify items frequently bought together

### Crowd-Sourced Data
- Aggregate anonymized price data across users
- Provide market-wide price trends
- Identify regional price variations

## Security & Privacy

- Anonymize price data when aggregating across users
- Users can opt-out of sharing price data
- Price history is user-private by default
- Aggregate statistics require minimum N users

## Performance Considerations

- Use materialized views for price averages
- Cache frequently accessed price comparisons
- Batch process trend analysis (daily job)
- Index on item name and store for fast lookups
- Partition price_records by date (monthly)

## Integration Points

- **Item Management:** Auto-record prices when items purchased
- **Budget Management:** Price predictions inform budget estimates
- **Notification Service:** Send price alerts and deal notifications
- **External APIs:** (Future) Integrate with store APIs for real-time prices
