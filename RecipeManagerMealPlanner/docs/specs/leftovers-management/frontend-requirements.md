# Frontend Requirements - Leftovers Management

## Pages/Views

### 1. Leftovers Tracker Page (`/leftovers`)

**Components**:
- LeftoversInventory: Active leftovers list
- ExpirationWarnings: Items expiring soon
- WasteMetrics: Food waste statistics dashboard
- LeftoverCard: Recipe name, quantity, storage date, best-by

**Features**:
- View all active leftovers
- Sort by expiration date
- Mark as consumed or discarded
- Get reminders before expiration
- See waste analytics (cost, quantity, trends)
- Portion size recommendations
- Add to meal plan suggestions

**API Calls**:
- GET /api/leftovers
- PUT /api/leftovers/{id}/consume
- PUT /api/leftovers/{id}/discard
- GET /api/leftovers/waste-report

### 2. Log Leftovers Dialog

**Components**:
- OriginalRecipeDisplay: Which meal
- QuantityEstimator: How much left
- StorageLocationSelector: Where stored
- BestByCalculator: Auto-calculate expiration

**Features**:
- Quick log from completed meal
- Estimate serving equivalents
- Auto-suggest best-by dates
- Photo of leftovers (optional)

### 3. Waste Analytics View

**Components**:
- WasteTrendChart: Monthly waste trends
- CostImpactMeter: Money wasted
- EnvironmentalImpact: CO2 equivalent
- PortionRecommendations: Adjust recipe servings

**Features**:
- Visualize food waste trends
- Calculate financial and environmental impact
- Get portion size recommendations
- Identify frequent waste sources

## State Management
- `leftoversSlice`: Active leftovers and waste data
