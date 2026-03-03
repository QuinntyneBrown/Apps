# Gift Registry Organizer - Requirements Document

## Overview
Gift Registry Organizer is a web application that helps users create and manage gift registries for events such as weddings, baby showers, birthdays, and holidays. Users can curate wish lists, share registries with guests, track gift purchases, and manage thank-you notes.

## Business Objectives
- Enable users to create organized gift registries for any occasion
- Allow sharing registries with family and friends
- Prevent duplicate gift purchases through real-time tracking
- Simplify thank-you note management after events
- Support multiple registries across different life events

## Target Users
- Couples planning weddings or anniversaries
- Parents organizing baby showers or children's birthdays
- Families coordinating holiday gift-giving
- Anyone hosting events where gift coordination is helpful

## Core Features

### Feature 1: Registry Management
**Description**: Create, organize, and manage gift registries for various events and occasions.

- **FR1.1**: Create a new registry with name, event type, event date, and description
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Registry is created with a unique shareable link
  - **AC3**: Event types include Wedding, Baby Shower, Birthday, Holiday, Housewarming, and Custom
  - **AC4**: Feature handles error conditions gracefully
- **FR1.2**: Edit registry details including name, date, description, and privacy settings
  - **AC1**: Feature is accessible to the registry owner
  - **AC2**: Changes are saved and reflected immediately
  - **AC3**: Feature handles error conditions gracefully
- **FR1.3**: Delete a registry with confirmation
  - **AC1**: Deletion requires explicit confirmation
  - **AC2**: All associated gift items and sharing links are removed
  - **AC3**: Feature handles error conditions gracefully
- **FR1.4**: Set registry visibility (public, private, link-only)
  - **AC1**: Public registries are discoverable by search
  - **AC2**: Private registries are only visible to the owner
  - **AC3**: Link-only registries are accessible via shareable URL
- **FR1.5**: Archive completed registries after the event
  - **AC1**: Archived registries are preserved but moved out of active view
  - **AC2**: Archived registries can be restored if needed

### Feature 2: Gift Item Management
**Description**: Add, organize, and manage gift items within registries.

- **FR2.1**: Add gift items with name, description, price, quantity needed, and optional URL
  - **AC1**: Feature is accessible to the registry owner
  - **AC2**: Price supports currency formatting
  - **AC3**: External product URLs can be linked for easy purchasing
  - **AC4**: Feature handles error conditions gracefully
- **FR2.2**: Organize items by category (e.g., Kitchen, Bedroom, Electronics, Clothing)
  - **AC1**: Default categories are provided
  - **AC2**: Users can create custom categories
  - **AC3**: Items can be moved between categories
- **FR2.3**: Set priority levels on gift items (must-have, nice-to-have, optional)
  - **AC1**: Priority is visible to registry viewers
  - **AC2**: Items can be sorted by priority
- **FR2.4**: Mark items as fulfilled when purchased
  - **AC1**: Fulfilled items are visually distinguished from unfulfilled items
  - **AC2**: Partial fulfillment is supported for items with quantity > 1
- **FR2.5**: Remove or edit existing gift items
  - **AC1**: Feature is accessible to the registry owner
  - **AC2**: Editing preserves purchase history
  - **AC3**: Feature handles error conditions gracefully

### Feature 3: Sharing and Collaboration
**Description**: Share registries with guests and allow collaborative gift coordination.

- **FR3.1**: Generate shareable links for each registry
  - **AC1**: Links are unique and non-guessable
  - **AC2**: Links can be regenerated if needed
- **FR3.2**: Share registries via email invitation
  - **AC1**: Feature is accessible to the registry owner
  - **AC2**: Invitations include the registry link and event details
  - **AC3**: Feature handles error conditions gracefully
- **FR3.3**: Allow guests to mark items as purchased (claim a gift)
  - **AC1**: Claimed items are hidden from other guests to prevent duplicates
  - **AC2**: The registry owner cannot see who purchased which item (surprise)
  - **AC3**: Guests can unclaim items if plans change
- **FR3.4**: Support co-owners for shared registries (e.g., couples)
  - **AC1**: Co-owners have full edit access to the registry
  - **AC2**: Co-owner invitation requires acceptance

### Feature 4: Purchase Tracking
**Description**: Track gift purchases, fulfillment status, and spending summaries.

- **FR4.1**: Display real-time fulfillment progress for each registry
  - **AC1**: Data is displayed in a clear, readable format
  - **AC2**: Progress bar shows percentage of items fulfilled
- **FR4.2**: Track which items have been purchased and by whom (owner view post-event)
  - **AC1**: Purchase details are revealed to the owner only after the event date
  - **AC2**: Historical data is preserved and queryable
- **FR4.3**: Show spending summary with total registry value and amount fulfilled
  - **AC1**: Calculations are mathematically accurate
  - **AC2**: Currency formatting is consistent
- **FR4.4**: Send notifications when items are purchased
  - **AC1**: Notifications are delivered promptly
  - **AC2**: Users can configure notification preferences
  - **AC3**: Notification content does not reveal the purchaser before the event

### Feature 5: Thank-You Note Management
**Description**: Track and manage thank-you notes for received gifts.

- **FR5.1**: Generate a thank-you list from purchased gifts after the event
  - **AC1**: List includes gift details and purchaser information
  - **AC2**: List is generated automatically when registry is archived
- **FR5.2**: Mark thank-you notes as sent for each gift
  - **AC1**: Feature is accessible to the registry owner
  - **AC2**: Sent/unsent status is tracked per item
- **FR5.3**: Track thank-you note progress (sent vs. pending)
  - **AC1**: Data is displayed in a clear, readable format
  - **AC2**: Progress is shown as a completion percentage

## Core Entities
- User, Registry, GiftItem, Category, GiftClaim, Invitation, ThankYouNote

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
