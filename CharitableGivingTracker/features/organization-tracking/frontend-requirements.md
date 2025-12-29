# Organization Tracking - Frontend Requirements

## Overview
Provides interface for managing charitable organizations, verifying tax-exempt status, viewing ratings, and setting preferences.

## User Interface Components

### 1. Organization List Page
**Components**:
- Organization cards with name, EIN, verification status, rating
- Search and filter options
- Add organization button
- Preferred organizations section

**Data Requirements**:
- GET /api/organizations

### 2. Add Organization Form
**Form Fields**:
- Organization Name* (text input)
- EIN* (formatted input XX-XXXXXXX)
- Charity Type* (dropdown)
- Website (URL input)
- Contact Email (email input)
- Contact Phone (phone input)
- Mission (textarea)

**API Calls**:
- POST /api/organizations
- POST /api/organizations/{id}/verify

### 3. Organization Details Page
**Sections**:
- Header with name, logo, verification badge
- Verification status and rating
- Contact information
- Mission statement
- Donation history to this organization
- Set as preferred button

**API Calls**:
- GET /api/organizations/{id}
- GET /api/donations?organizationId={id}

### 4. Charity Rating Display
**Components**:
- Star rating visualization
- Rating source badge (Charity Navigator, GiveWell)
- Last updated date
- Refresh rating button

## Responsive Design
- Mobile: Stacked cards
- Tablet: 2-column grid
- Desktop: 3-column grid with filters sidebar
