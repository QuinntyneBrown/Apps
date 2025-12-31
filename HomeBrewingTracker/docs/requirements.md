# Home Brewing Tracker - Requirements

## Executive Summary
Track beer brewing from recipe to bottle, monitor fermentation, log tasting notes, and perfect recipes through detailed brewing analytics.

## Core Features

### 1. Recipe Management
- **FR-1.1**: Create and edit brewing recipes
  - **AC1**: Given the user is authenticated, the add/create form is accessible
  - **AC2**: When valid data is submitted, the item is created successfully
  - **AC3**: Then the user receives confirmation and the new item appears in the list
  - **AC4**: Existing data is pre-populated in the edit form
- **FR-1.2**: Clone recipes for experimentation
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-1.3**: Scale recipes for different batch sizes
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-1.4**: Ingredient database and calculator
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-1.5**: Recipe sharing with community
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully

### 2. Brew Day Tracking
- **FR-2.1**: Schedule brew sessions
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-2.2**: Record mash temperatures
  - **AC1**: Given the user is authenticated, the add/create form is accessible
  - **AC2**: When valid data is submitted, the item is created successfully
  - **AC3**: Then the user receives confirmation and the new item appears in the list
- **FR-2.3**: Log gravity readings (OG/FG)
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range
- **FR-2.4**: Track brewing timeline
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range
- **FR-2.5**: Note issues and adjustments
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully

### 3. Fermentation Monitoring
- **FR-3.1**: Temperature logging
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range
- **FR-3.2**: Fermentation activity tracking
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range
- **FR-3.3**: Gravity check scheduling
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-3.4**: Secondary fermentation tracking
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range
- **FR-3.5**: Completion detection
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully

### 4. Packaging
- **FR-4.1**: Bottling session tracking
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range
- **FR-4.2**: Kegging management
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-4.3**: Carbonation monitoring
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-4.4**: Label design and printing
  - **AC1**: Categories/tags can be created, edited, and deleted
  - **AC2**: Items can be assigned to multiple categories
  - **AC3**: Category changes are reflected immediately
- **FR-4.5**: Inventory management
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully

### 5. Tasting & Evaluation
- **FR-5.1**: Formal tasting notes
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-5.2**: Score batches (appearance, aroma, flavor, mouthfeel)
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-5.3**: Compare batches
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-5.4**: Rate success
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-5.5**: Improvement identification
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully

### 6. Inventory & Shopping
- **FR-6.1**: Ingredient inventory
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-6.2**: Freshness tracking
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range
- **FR-6.3**: Shopping list generation
  - **AC1**: Data is displayed in a clear, organized format
  - **AC2**: View loads within acceptable performance limits
  - **AC3**: Empty states are handled with appropriate messaging
- **FR-6.4**: Supplier management
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-6.5**: Cost tracking
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range

## Technical Requirements
- Temperature sensor integration
- Recipe import/export (BeerXML)
- Batch photo upload
- Competition entry tracking


## Multi-Tenancy Support

### Tenant Isolation
- **FR-MT-1**: Support for multi-tenant architecture with complete data isolation
  - **AC1**: Each tenant's data is completely isolated from other tenants
  - **AC2**: All queries are automatically filtered by TenantId
  - **AC3**: Cross-tenant data access is prevented at the database level
- **FR-MT-2**: TenantId property on all aggregate entities
  - **AC1**: Every aggregate root has a TenantId property
  - **AC2**: TenantId is set during entity creation
  - **AC3**: TenantId cannot be modified after creation
- **FR-MT-3**: Automatic tenant context resolution
  - **AC1**: TenantId is extracted from JWT claims or HTTP headers
  - **AC2**: Invalid or missing tenant context is handled gracefully
  - **AC3**: Tenant context is available throughout the request pipeline

