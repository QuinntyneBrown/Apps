# Requirements - Conversation Starter App

## Overview
Provide conversation prompts and questions to deepen relationships and spark meaningful discussions across various contexts (romantic, family, friends, professional).

## Core Features

### 1. Prompt Generation
- **FR-1.1**: FR-PG-001: Generate prompts based on category and difficulty
  - **AC1**: Categories/tags can be created, edited, and deleted
  - **AC2**: Items can be assigned to multiple categories
  - **AC3**: Category changes are reflected immediately
- **FR-1.2**: FR-PG-002: Filter prompts by context (romantic/family/friends/professional)
  - **AC1**: Search results are accurate and relevant
  - **AC2**: Search completes within 2 seconds
  - **AC3**: No results state provides helpful suggestions
- **FR-1.3**: FR-PG-003: Skip and get new prompt
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-1.4**: FR-PG-004: Favorite prompts for later use
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-1.5**: FR-PG-005: Create custom prompts
  - **AC1**: Given the user is authenticated, the add/create form is accessible
  - **AC2**: When valid data is submitted, the item is created successfully
  - **AC3**: Then the user receives confirmation and the new item appears in the list

### 2. Conversation Tracking
- **FR-2.1**: FR-CT-001: Mark prompt as used when conversation starts
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-2.2**: FR-CT-002: Rate conversation quality after use
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-2.3**: FR-CT-003: Add notes about insights from conversation
  - **AC1**: Given the user is authenticated, the add/create form is accessible
  - **AC2**: When valid data is submitted, the item is created successfully
  - **AC3**: Then the user receives confirmation and the new item appears in the list
- **FR-2.4**: FR-CT-004: Track conversation duration
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range

### 3. Collections
- **FR-3.1**: FR-COL-001: Create themed prompt collections
  - **AC1**: Given the user is authenticated, the add/create form is accessible
  - **AC2**: When valid data is submitted, the item is created successfully
  - **AC3**: Then the user receives confirmation and the new item appears in the list
- **FR-3.2**: FR-COL-002: Share collections with others
  - **AC1**: Export includes all relevant data fields
  - **AC2**: Export format is compatible with common tools
  - **AC3**: Large exports are handled without timeout
- **FR-3.3**: FR-COL-003: Browse community collections
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-3.4**: FR-COL-004: Add prompts to collections
  - **AC1**: Given the user is authenticated, the add/create form is accessible
  - **AC2**: When valid data is submitted, the item is created successfully
  - **AC3**: Then the user receives confirmation and the new item appears in the list

### 4. Progress & Insights
- **FR-4.1**: FR-PI-001: Track usage streaks
  - **AC1**: Historical data is preserved and accessible
  - **AC2**: Timestamps are accurate and consistently formatted
  - **AC3**: Data can be filtered by date range
- **FR-4.2**: FR-PI-002: Progress through difficulty levels
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully
- **FR-4.3**: FR-PI-003: View conversation history
  - **AC1**: Data is displayed in a clear, organized format
  - **AC2**: View loads within acceptable performance limits
  - **AC3**: Empty states are handled with appropriate messaging
  - **AC4**: Historical data is preserved and accessible
- **FR-4.4**: FR-PI-004: Get personalized recommendations
  - **AC1**: Given an authenticated user, the feature is accessible
  - **AC2**: When the feature is used correctly, it performs as expected
  - **AC3**: Then appropriate feedback is provided to the user
  - **AC4**: The feature handles edge cases gracefully

## Technical Requirements
- Frontend: React SPA with offline support
- Backend: ASP.NET Core API
- Database: SQL Server
- Prompt library: Minimum 500 curated prompts


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

