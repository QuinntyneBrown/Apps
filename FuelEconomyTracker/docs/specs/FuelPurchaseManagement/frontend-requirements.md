# Fuel Purchase Management - Frontend Requirements

## Pages/Views

### 1. Add Fuel Purchase Page
**Route**: `/vehicles/{vehicleId}/fuel-purchases/new`

**UI Components**:
- **Header**: "Add Fuel Purchase" with back button
- **Form Sections**:
  - Basic Information
    - Date picker (default: today)
    - Odometer reading input (numeric, required)
    - Fuel amount (gallons/liters) input (decimal, required)
    - Total cost input (currency, required)
    - Cost per gallon (auto-calculated, read-only)

  - Fuel Details
    - Fuel grade selector (Regular/Premium/Diesel/E85)
    - Fill type toggle (Full Fill / Partial Fill)
    - Tank level sliders (if partial fill, before/after percentages)

  - Station Information
    - Station name input with autocomplete
    - Location picker (map or address search)
    - "Use current location" button
    - Recent stations quick-select

  - Payment & Notes
    - Payment method selector
    - Notes textarea (optional)

- **Action Buttons**:
  - "Save Fill-Up" (primary)
  - "Cancel" (secondary)

- **Real-time Calculations**:
  - Display calculated MPG (if possible)
  - Show miles since last fill
  - Highlight if new personal best

**Validation**:
- Odometer must be >= previous reading
- Gallons must be > 0 and < vehicle tank capacity
- Date cannot be future
- Cost must be reasonable range
- Show warning if values seem unusual

**Success State**:
- Show success message with calculated MPG
- Display quick stats (current avg, comparison to previous)
- Options: "Add Another" or "View History"

### 2. Fuel History Page
**Route**: `/vehicles/{vehicleId}/fuel-purchases`

**UI Components**:
- **Header**:
  - Title: "Fuel History"
  - "+ Add Fill-Up" button
  - Filter toggle button
  - Export button

- **Filter Panel** (collapsible):
  - Date range picker
  - Fuel grade filter
  - Station filter
  - Sort options (Date, MPG, Cost)

- **Summary Cards** (top of page):
  - Current Average MPG (with trend indicator)
  - Total Spent (this month)
  - Fills This Month
  - Best MPG This Month

- **Fuel Purchase List**:
  - Each item shows:
    - Date (prominent)
    - Gallons and total cost
    - MPG badge (color-coded: green=good, yellow=average, red=poor)
    - Station name and location
    - Odometer reading
    - Action menu (Edit, Delete, View Details)

  - Visual indicators:
    - Personal best icon
    - Partial fill badge
    - Low MPG warning icon

- **Pagination**: Load more or infinite scroll

- **Empty State**:
  - Friendly illustration
  - "No fill-ups recorded yet"
  - Large "Add Your First Fill-Up" button
  - Quick tips on tracking fuel economy

**Interactions**:
- Tap item to view details
- Swipe to delete (with confirmation)
- Pull to refresh
- Filter applies instantly
- Smooth animations for list updates

### 3. Fuel Purchase Details Page
**Route**: `/vehicles/{vehicleId}/fuel-purchases/{id}`

**UI Components**:
- **Header**: Date and time, Edit button
- **MPG Display** (large, prominent):
  - MPG value
  - Comparison to vehicle average
  - Percentile ranking
  - Personal best indicator

- **Purchase Information**:
  - Cost breakdown (gallons × price = total)
  - Odometer reading
  - Miles since last fill
  - Fuel grade badge

- **Station Information**:
  - Station name and address
  - Map preview (tappable for full map)
  - "Rate this station" button
  - Distance from home/work

- **Trip Context**:
  - Driving conditions (if logged)
  - Weather at time
  - Notes entered

- **Actions**:
  - Edit button
  - Delete button
  - Share button (share stats/achievement)

### 4. Station Rating Modal
**Trigger**: Click "Rate Station" from purchase details or station selection

**UI Components**:
- **Modal Header**: Station name and location
- **Rating Sections**:
  - Price Competitiveness (1-5 stars)
  - Cleanliness (1-5 stars)
  - Amenities (1-5 stars)
  - Service (1-5 stars)
  - "Would you return?" toggle

- **Additional Feedback**:
  - Notes textarea
  - Photo upload (optional)

- **Actions**:
  - "Submit Rating" button
  - "Cancel" button

## UI/UX Specifications

### Design System

**Color Palette**:
- Primary: #2563EB (Blue) - main actions, links
- Success: #10B981 (Green) - good MPG, achievements
- Warning: #F59E0B (Amber) - average MPG, alerts
- Error: #EF4444 (Red) - poor MPG, validation errors
- Neutral: Tailwind gray scale for backgrounds and text

**Typography**:
- Headers: Inter Bold, 24-32px
- Body: Inter Regular, 16px
- Labels: Inter Medium, 14px
- Numbers: Tabular figures for alignment

**Spacing**:
- Page padding: 16px mobile, 24px tablet/desktop
- Component spacing: 16px between major sections
- Form field spacing: 12px between fields
- List item padding: 16px vertical, 16px horizontal

### Responsive Breakpoints
- Mobile: < 640px (single column, full width forms)
- Tablet: 640px - 1024px (optimized layout, side-by-side where appropriate)
- Desktop: > 1024px (multi-column, enhanced visualizations)

### Accessibility
- Semantic HTML throughout
- ARIA labels for all interactive elements
- Keyboard navigation support
- Focus indicators clearly visible
- Color contrast ratio >= 4.5:1
- Screen reader announcements for dynamic content

### Mobile Considerations
- Large touch targets (minimum 44×44px)
- Bottom navigation for frequent actions
- Native number keyboards for numeric inputs
- Optimized for one-handed use
- Offline data entry with sync queue
- GPS integration for automatic location

## State Management

### Component State
```typescript
interface FuelPurchaseForm {
  vehicleId: string;
  date: Date;
  odometerReading: number;
  gallons: number;
  totalCost: number;
  costPerGallon: number; // calculated
  fuelGrade: 'Regular' | 'Premium' | 'Diesel' | 'E85';
  isPartialFill: boolean;
  tankLevelBefore?: number;
  tankLevelAfter?: number;
  station: {
    id?: string;
    name: string;
    location: GeoLocation;
  };
  paymentMethod: PaymentMethod;
  notes?: string;
}

interface FuelPurchaseListState {
  purchases: FuelPurchase[];
  filters: {
    startDate?: Date;
    endDate?: Date;
    fuelGrade?: string;
    stationId?: string;
  };
  sortBy: 'date' | 'mpg' | 'cost';
  sortOrder: 'asc' | 'desc';
  pagination: {
    page: number;
    pageSize: number;
    totalRecords: number;
  };
  loading: boolean;
  error?: string;
}
```

### Global State (Redux/Context)
- User preferences (units, default vehicle)
- Recently used stations
- Current vehicle selection
- Offline queue for pending uploads

## API Integration

### Service Layer
```typescript
class FuelPurchaseService {
  async createFuelPurchase(data: FuelPurchaseForm): Promise<FuelPurchase> {
    // POST /api/fuel-purchases
  }

  async getFuelPurchases(vehicleId: string, filters: Filters): Promise<PaginatedResponse<FuelPurchase>> {
    // GET /api/fuel-purchases
  }

  async updateFuelPurchase(id: string, data: Partial<FuelPurchaseForm>): Promise<FuelPurchase> {
    // PUT /api/fuel-purchases/{id}
  }

  async deleteFuelPurchase(id: string): Promise<void> {
    // DELETE /api/fuel-purchases/{id}
  }

  async rateStation(stationId: string, rating: StationRating): Promise<void> {
    // POST /api/fuel-stations/{id}/rate
  }

  async searchStations(query: string, location: GeoLocation): Promise<Station[]> {
    // GET /api/fuel-stations/search
  }
}
```

### Error Handling
- Network errors: Show retry option with offline queue
- Validation errors: Inline field-level messages
- Server errors: User-friendly messages with support contact
- Optimistic updates: Rollback on failure with notification

## Performance Optimizations

### Data Loading
- Paginate fuel purchase list (25 items per page)
- Lazy load station details on demand
- Cache recent stations in localStorage
- Prefetch next page on scroll proximity

### Rendering
- Virtual scrolling for long lists (>100 items)
- Memoize expensive calculations (MPG, averages)
- Debounce search/filter inputs (300ms)
- Skeleton screens during loading

### Offline Support
- Service worker for offline functionality
- IndexedDB for local fuel purchase queue
- Sync when connection restored
- Clear offline indicators in UI

## Validations & Business Rules

### Client-Side Validation
```typescript
const validateFuelPurchase = (data: FuelPurchaseForm, vehicle: Vehicle, lastPurchase?: FuelPurchase) => {
  const errors: ValidationErrors = {};

  // Odometer validation
  if (lastPurchase && data.odometerReading <= lastPurchase.odometerReading) {
    errors.odometerReading = 'Odometer reading must be greater than previous reading';
  }

  // Date validation
  if (data.date > new Date()) {
    errors.date = 'Date cannot be in the future';
  }

  // Gallons validation
  if (data.gallons <= 0) {
    errors.gallons = 'Gallons must be greater than 0';
  }
  if (data.gallons > vehicle.tankCapacity) {
    errors.gallons = `Gallons exceeds tank capacity (${vehicle.tankCapacity} gal)`;
  }

  // Cost validation
  if (data.costPerGallon < 1 || data.costPerGallon > 15) {
    errors.costPerGallon = 'Price per gallon seems unusual. Please verify.';
  }

  return errors;
};
```

### Warning Conditions
- MPG calculation differs from average by >30%
- Odometer jump is unusually large (>500 miles)
- Very low gallons for claimed full fill (<30% tank)
- Very short time between fill-ups (<24 hours for full fills)

## Analytics & Tracking

### Events to Track
- `fuel_purchase_added` - User creates new purchase
- `fuel_purchase_edited` - User modifies purchase
- `fuel_purchase_deleted` - User removes purchase
- `station_rated` - User rates a fuel station
- `filter_applied` - User filters purchase history
- `export_initiated` - User exports data

### Metrics to Monitor
- Average time to complete add form
- Abandonment rate on add form
- Most common fuel grades
- Filter usage frequency
- Mobile vs desktop usage split
