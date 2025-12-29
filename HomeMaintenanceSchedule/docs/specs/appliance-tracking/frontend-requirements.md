# Appliance Tracking - Frontend Requirements

## Overview
Intuitive interface for managing home appliance inventory with warranty tracking, maintenance scheduling, and replacement planning.

## UI Components

### 1. Appliance Inventory Dashboard
- Grid/List view of all appliances
- Filter by category, location, warranty status
- Sort by purchase date, warranty expiration, age
- Search by name, brand, model
- Quick stats cards (Total appliances, Expiring warranties, Needing replacement)

### 2. Appliance Card (Grid View)
- Appliance photo
- Name, brand, model
- Category badge
- Age indicator (e.g., "3 years old")
- Warranty status badge (Active/Expiring Soon/Expired)
- Condition indicator (color-coded)
- Quick actions (View, Edit, Service History)

### 3. Add/Edit Appliance Form
**Basic Information**:
- Name, Category, Brand, Model, Serial Number
- Location in home
- Photo upload

**Purchase Details**:
- Purchase date, Cost, Location
- Receipt/invoice upload

**Warranty Information**:
- Warranty expiration date
- Warranty terms/notes
- Manual upload (PDF)

**Lifecycle**:
- Installation date
- Expected lifespan (years)
- Current condition

### 4. Appliance Detail View
**Header**: Name, brand, model, photo, condition badge
**Info Sections**:
- Purchase & Warranty Information
- Specifications
- Location
- Lifecycle Progress (visual bar showing age vs lifespan)
- Service History Timeline
- Maintenance Schedule
- Cost Summary

**Action Buttons**: Edit, Record Service, Schedule Maintenance, Delete

### 5. Warranty Dashboard
- Expiring warranties list (next 90 days)
- Expired warranties list
- Calendar view of warranty expirations
- Warranty renewal reminders

### 6. Replacement Planning
- Appliances nearing end of life
- Recommended replacement timeline
- Budget planning tool
- Energy efficiency comparison (old vs new models)

## State Management
```javascript
{
  appliances: {
    items: [],
    selected: null,
    expiringWarranties: [],
    replacementCandidates: [],
    filters: {
      category: null,
      location: null,
      warrantyStatus: null
    },
    loading: false
  }
}
```

## Features
- QR code scanning for model/serial numbers
- Receipt OCR for automatic data entry
- Warranty expiration countdown
- Maintenance schedule integration
- Energy cost tracking
- Appliance lifecycle charts
- Export inventory to CSV/PDF

## Responsive Design
- Mobile: Card stack with swipe actions
- Tablet: 2-column grid
- Desktop: 3-4 column grid with sidebar filters

---

**Version**: 1.0
**Last Updated**: 2025-12-29
