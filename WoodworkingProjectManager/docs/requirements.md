# Woodworking Project Manager - Requirements

## Overview
Comprehensive project management system for woodworkers to plan builds, manage materials and tools, track progress, ensure quality, and develop craftsmanship skills.

## Target Users
- Hobbyist woodworkers
- Professional furniture makers
- Custom woodworking shops
- Woodworking instructors
- DIY enthusiasts

## Core Features

### 1. Project Management
- Conceive and design projects
- Create detailed build plans
- Track project progress
- Manage milestones
- Complete or abandon projects
- Portfolio building

### 2. Material Management
- Purchase and catalog lumber
- Track wood milling
- Allocate materials to projects
- Monitor waste and efficiency
- Reclaim scrap wood
- Calculate yields

### 3. Tool & Equipment
- Tool inventory management
- Usage tracking
- Maintenance scheduling
- Blade/bit sharpening logs
- Tool breakdown tracking
- ROI calculation

### 4. Build Process
- Cut list management
- Joinery execution tracking
- Assembly documentation
- Finishing application logs
- Process photography
- Time tracking per phase

### 5. Quality Assurance
- Quality inspections
- Defect identification
- Rework management
- Quality standards tracking
- Client satisfaction
- Portfolio selection

### 6. Skill Development
- New technique attempts
- Skill mastery tracking
- Mistake learning logs
- Knowledge base building
- Teaching preparation
- Expertise growth

### 7. Client & Delivery
- Custom order management
- Client communication
- Feedback collection
- Project delivery tracking
- Contract management
- Testimonials

## Technical Requirements
- .NET 8 backend, SQL Server
- Rich media support for plans and photos
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs the specified function correctly
  - **AC3**: Feature handles error conditions gracefully
- Measurement calculations
- Material cost tracking
- Time tracking integration
- Mobile app for shop floor use


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


## Success Metrics
- Project completion rate > 70%
- Material waste < 15%
- Quality score average > 4.5/5
- Tool maintenance adherence > 90%
