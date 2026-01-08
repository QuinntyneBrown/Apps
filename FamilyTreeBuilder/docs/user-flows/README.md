# FamilyTreeBuilder User Flows

This directory contains PlantUML sequence diagrams documenting the major user flows for the FamilyTreeBuilder application.

## Overview

These user flows were created based on the frontend and backend requirement specifications located in `/docs/specs/`. Each flow includes:
- A `.puml` PlantUML source file
- A `.png` rendered diagram
- Actor interactions, UI components, API calls, and database operations

## User Flows

### 01. User Login Authentication Flow
**File:** `01-user-login.puml`

Shows the complete authentication flow from user credential entry through JWT token generation and validation. Includes both success and failure scenarios with password hashing verification.

**Key Components:**
- Login Page UI
- Auth Service and Controller
- Password Hashing Service (PBKDF2 with salt)
- JWT token generation with user claims
- Database authentication

---

### 02. Add Person to Family Tree Flow
**File:** `02-add-person.puml`

Demonstrates the process of adding a new family member to the tree, including form validation, multi-tenant support, and domain event emission.

**Key Components:**
- Add Person Modal with form fields
- Person Service and Controller
- AddPersonCommandHandler
- TenantContext for multi-tenancy
- Generation level calculation
- PersonAdded domain event

---

### 03. Update Person Details Flow
**File:** `03-update-person.puml`

Illustrates editing existing person information, from loading the current data through saving changes and tracking modified fields.

**Key Components:**
- Person Profile Page with edit mode
- UpdatePersonCommandHandler
- Field change tracking
- PersonDetailsUpdated domain event
- Multi-tenant filtering

---

### 04. Record Death Information Flow
**File:** `04-record-death.puml`

Shows how to transition a living person to deceased status, recording death date, place, and burial location.

**Key Components:**
- Death information form
- RecordDeathCommandHandler
- Living status update (IsLiving = false)
- DeathRecorded domain event
- Profile update with death details

---

### 05. Establish Relationship Between Persons Flow
**File:** `05-establish-relationship.puml`

Depicts creating relationships between family members including parent-child, sibling, spouse, and adoptive relationships.

**Key Components:**
- Relationship form with person dropdowns
- Multiple relationship types (Parent, Child, Sibling, Spouse, Adoptive)
- EstablishRelationshipCommandHandler
- RelationshipEstablished domain event
- Multi-tenant support

---

### 06. Record Marriage Between Spouses Flow
**File:** `06-record-marriage.puml`

Details the marriage recording process including automatic spouse relationship creation and transaction handling.

**Key Components:**
- Marriage form with spouse selection
- Marriage date and place entry
- RecordMarriageCommandHandler
- Database transaction for data consistency
- Automatic spouse relationship creation
- MarriageRecorded domain event

---

### 07. View Family Tree Visualization Flow
**File:** `07-view-family-tree.puml`

Demonstrates loading and rendering the interactive family tree with multiple layout options and user interactions.

**Key Components:**
- Parallel loading of persons and relationships
- Tree Rendering Engine
- Multiple layout options (Pedigree, Fan Chart, Descendant Tree)
- Interactive features: zoom, pan, node selection
- SVG-based visualization

---

### 08. Admin User Management Flow
**File:** `08-admin-user-management.puml`

Comprehensive admin workflow for managing users including CRUD operations and role assignments.

**Key Components:**
- User list view
- Create user with password hashing (PBKDF2, 100k iterations)
- Update user profile
- Assign/remove roles
- Delete user with cascade
- Admin authorization

---

### 09. View Person Profile and Details Flow
**File:** `09-view-person-profile.puml`

Shows the complete person profile viewing experience with parallel data loading and navigation between related persons.

**Key Components:**
- Person List/Search integration
- Parallel loading of person data and relationships
- Profile sections: vital statistics, family relationships, biography
- Navigation to related persons
- Integration with edit and relationship flows

---

### 10. Search and Discover Persons Flow
**File:** `10-search-persons.puml`

Illustrates the search functionality with basic and advanced search options, filtering, and result management.

**Key Components:**
- Search page with query input
- Basic search by name, dates, location
- Advanced search with multiple criteria
- Result filtering and sorting
- Grouping by generation, surname, time period
- Save search functionality

---

## Technical Architecture

All flows follow these architectural patterns:

### Multi-Tenancy
- Every database operation includes TenantId filtering
- ITenantContext provides current tenant identifier
- Row-level security enforced at database level

### Domain-Driven Design
- Command handlers process business logic
- Domain events raised for important state changes
- Aggregates encapsulate business rules

### Data Access
- IFamilyTreeBuilderContext used directly (no repository pattern)
- DbContext provides persistence surface
- Entity Framework Core for ORM

### API Structure
- RESTful endpoints
- MediatR for CQRS pattern
- DTOs for data transfer
- Extension methods for mapping (ToDto)

### Frontend Architecture
- Angular with Material Design
- RxJS for reactive state management
- Async pipe pattern for data loading
- Service layer for API communication

---

## Rendering Diagrams

To regenerate PNG files from PlantUML sources:

```bash
# Render all diagrams
plantuml -tpng *.puml

# Render specific diagram
plantuml -tpng 01-user-login.puml
```

---

## Related Documentation

- **Frontend Requirements:** `/docs/specs/person-management-frontend-requirements.md`
- **Backend Requirements:** `/docs/specs/person-management-backend-requirements.md`
- **Relationship Management:** `/docs/specs/relationship-management-*.md`
- **User Management:** `/docs/specs/user-management-srs.md`
- **Identity Spec:** `/docs/specs/identity-spec.md`
- **Implementation Specs:** `/docs/specs/implementation-specs.md`

---

*Generated: 2026-01-08*
*Branch: claude/add-plantuml-user-flows-I3c0v*
