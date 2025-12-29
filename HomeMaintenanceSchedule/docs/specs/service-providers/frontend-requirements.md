# Service Providers - Frontend Requirements

## Overview
The Service Providers frontend provides an intuitive interface for managing trusted home service professionals, tracking service history, ratings, and quickly accessing provider contact information.

## User Interface Components

### 1. Providers Directory
**Purpose**: Browse and search service providers

**Components**:
- **Search Bar**: Text search with autocomplete
- **Filter Panel**:
  - Category filter (multi-select)
  - Rating filter (1-5 stars)
  - Preferred only toggle
  - Active/Inactive toggle
  - Specialty search
- **Sort Options**: Name, Rating, Most Used, Recently Added
- **View Toggle**: Grid view / List view
- **Provider Cards** (Grid View):
  - Provider name & company
  - Star rating (visual stars)
  - Specialty badge
  - Phone number (click to call)
  - Preferred star icon
  - Quick action buttons (Edit, Call, Email, View Details)
- **Provider Rows** (List View):
  - Compact table format
  - Sortable columns
  - Quick actions dropdown

### 2. Provider Profile View
**Purpose**: Detailed provider information and service history

**Sections**:
- **Header**:
  - Provider name & company
  - Star rating with review count
  - Preferred badge
  - Action buttons (Edit, Mark Preferred, Deactivate, Delete)
- **Contact Information**:
  - Phone (click to call)
  - Email (click to email)
  - Website (click to visit)
  - Address with map link
- **Professional Details**:
  - License number
  - Insurance information
  - Service categories (badges)
  - Specialty description
- **Statistics Cards**:
  - Total services provided
  - Average cost per service
  - Last service date
  - Total spent
- **Service History Table**:
  - Date, Description, Cost, Rating, Invoice
  - Sortable columns
  - Paginated
  - Export to CSV option
- **Reviews Section**:
  - List of all reviews with dates
  - Star ratings
  - Review text
  - Service date reference
- **Notes Section**:
  - Free-form notes
  - Edit inline

### 3. Add/Edit Provider Form
**Purpose**: Create or modify provider profiles

**Form Fields**:
```
Basic Information:
- Name * (text input)
- Company Name (text input, optional)
- Specialty * (text input with suggestions)
- Service Categories * (multi-select dropdown)

Contact Information:
- Phone * (phone input with formatting)
- Email (email input with validation)
- Website (URL input)

Address:
- Street Address (text input)
- City (text input)
- State (dropdown)
- ZIP Code (text input with validation)

Professional Details:
- License Number (text input)
- Insurance Information (textarea)

Preferences:
- Mark as Preferred Provider (checkbox)
- Active Status (toggle switch)

Notes:
- Internal Notes (textarea, rich text)
```

**Validation**:
- Real-time validation
- Required field indicators
- Format validation (phone, email, URL)
- Duplicate phone number warning

### 4. Record Service Modal
**Purpose**: Log completed service from provider

**Form Fields**:
```
- Service Date * (date/time picker)
- Related Task (dropdown, searchable, optional)
- Description * (textarea)
- Cost * (currency input)
- Duration (time input, hours:minutes)
- Rating (1-5 star selector)
- Review (textarea)
- Invoice Upload (file upload, PDF/images)
- Photos (multiple file upload)
```

### 5. Provider Comparison View
**Purpose**: Compare multiple providers side-by-side

**Features**:
- Select 2-4 providers to compare
- Side-by-side cards showing:
  - Rating
  - Average cost
  - Total services
  - Service categories
  - Last service date
  - Contact info
- Export comparison to PDF

### 6. Quick Contact Panel
**Purpose**: Fast access to provider contact methods

**Features**:
- Click-to-call phone numbers
- Email link (opens email client)
- SMS link (on mobile)
- WhatsApp link (if available)
- Website quick link
- Copy contact info to clipboard

## State Management

### Redux Store Structure
```javascript
{
  providers: {
    items: [],
    selectedProvider: null,
    loading: false,
    error: null,
    filters: {
      search: '',
      categories: [],
      minRating: 0,
      isPreferred: false,
      isActive: true
    },
    sort: {
      field: 'name',
      direction: 'asc'
    },
    viewMode: 'grid', // or 'list'
    pagination: {
      page: 1,
      pageSize: 20,
      totalCount: 0
    }
  },
  services: {
    items: [],
    loading: false,
    error: null,
    selectedService: null
  }
}
```

### Actions
- `fetchProviders()`, `fetchProviderById(id)`
- `createProvider(data)`, `updateProvider(id, data)`
- `deleteProvider(id)`
- `markPreferred(id)`, `deactivateProvider(id)`
- `recordService(providerId, data)`
- `addRating(providerId, serviceId, rating, review)`
- `setFilter(filterType, value)`, `clearFilters()`
- `setSort(field, direction)`
- `setViewMode(mode)`

## API Integration

### Service Class
```javascript
class ProviderService {
  async getProviders(filters, pagination) { }
  async getProviderById(id) { }
  async createProvider(data) { }
  async updateProvider(id, data) { }
  async deleteProvider(id) { }
  async markPreferred(id) { }
  async deactivateProvider(id) { }
  async getServiceHistory(providerId, pagination) { }
  async recordService(providerId, data) { }
  async addRating(providerId, serviceId, rating, review) { }
  async getStatistics(providerId) { }
  async searchProviders(query) { }
}
```

## Forms & Validation

### Provider Form Schema
```javascript
const providerSchema = yup.object({
  name: yup.string().required().max(100),
  companyName: yup.string().max(150),
  specialty: yup.string().required().max(200),
  phone: yup.string().required().matches(/^\(\d{3}\) \d{3}-\d{4}$/),
  email: yup.string().email(),
  website: yup.string().url(),
  serviceCategories: yup.array().min(1).required(),
  licenseNumber: yup.string().max(50),
  insuranceInfo: yup.string().max(500)
});
```

### Service Record Schema
```javascript
const serviceSchema = yup.object({
  serviceDate: yup.date().required().max(new Date()),
  description: yup.string().required().max(1000),
  cost: yup.number().required().min(0),
  durationMinutes: yup.number().positive(),
  rating: yup.number().min(1).max(5),
  reviewText: yup.string().max(1000)
});
```

## UI Components

### ProviderCard Component
```jsx
<ProviderCard
  provider={provider}
  onEdit={handleEdit}
  onCall={handleCall}
  onEmail={handleEmail}
  onViewDetails={handleView}
  onTogglePreferred={handlePreferred}
/>
```

### StarRating Component
```jsx
<StarRating
  rating={4.5}
  reviewCount={23}
  editable={false}
  size="large"
  onChange={handleRatingChange}
/>
```

### ServiceHistoryTable Component
```jsx
<ServiceHistoryTable
  providerId={providerId}
  services={services}
  onViewService={handleViewService}
  onAddRating={handleAddRating}
  pagination={pagination}
/>
```

### ContactButtons Component
```jsx
<ContactButtons
  phone={phone}
  email={email}
  website={website}
  layout="horizontal"
/>
```

## Features

### 1. Smart Search
- Search across name, company, specialty
- Autocomplete suggestions
- Recent searches
- Search filters applied automatically

### 2. Quick Actions
- One-click call/email/text
- Quick add to task assignment
- Fast service recording
- Instant rating update

### 3. Provider Analytics
- Service frequency chart
- Cost trends over time
- Rating history
- Category distribution

### 4. Bulk Operations
- Export multiple providers to CSV
- Bulk category assignment
- Mass deactivation (with confirmation)

### 5. Integration Features
- Import from contacts
- Export to contacts
- Share provider (email/message)
- Print provider directory

## Accessibility

- Keyboard navigation for all actions
- ARIA labels on star ratings
- Screen reader support for contact buttons
- High contrast mode for ratings
- Focus management in modals
- Alt text for provider photos

## Performance

- Virtual scrolling for large provider lists
- Lazy load service history
- Debounced search (300ms)
- Memoized provider cards
- Optimistic UI updates
- Service Worker caching

## Responsive Design

### Mobile (< 640px)
- Stacked provider cards
- Bottom sheet for filters
- Swipe actions on cards
- Click-to-call prominent
- Simplified service form

### Tablet (640px - 1024px)
- 2-column grid
- Side panel for filters
- Expanded contact options

### Desktop (> 1024px)
- 3-4 column grid
- Fixed filter sidebar
- Inline editing
- Hover actions

## Third-Party Libraries

- **Star Ratings**: react-rating-stars-component
- **Phone Formatting**: libphonenumber-js
- **Map Links**: Google Maps API or Mapbox
- **Charts**: Recharts for analytics
- **Virtual List**: react-window for large lists
- **CSV Export**: react-csv

## Testing Requirements

### Unit Tests
- Provider CRUD operations
- Rating calculation display
- Search/filter logic
- Form validation

### Integration Tests
- Add provider workflow
- Record service workflow
- Rating submission
- Service history display

### E2E Tests
- User can add a provider
- User can record a service
- User can rate a provider
- User can search providers

---

**Version**: 1.0
**Last Updated**: 2025-12-29
