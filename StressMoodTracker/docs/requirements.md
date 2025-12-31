# Requirements - Stress & Mood Tracker

## Overview
Stress & Mood Tracker helps users monitor their mental health through mood logging, stress tracking, trigger identification, journaling, and personalized insights for better well-being.

## Features & Requirements

### 1. Mood Tracking
**Backend**: Store mood entries with ratings, emotion tags, intensity, context notes, detect mood swings, track positive streaks
**Frontend**: Mood check-in interface, emotion selector, mood history timeline, swing alerts, streak celebrations

### 2. Stress Tracking
**Backend**: Record stress levels (1-10), track stress types, physical symptoms, detect high stress alerts, measure stress reduction
**Frontend**: Stress assessment form, quick check-in slider, stress level visualization, alert notifications, reduction tracking

### 3. Trigger Management
**Backend**: Log triggers with types, descriptions, impact levels, detect patterns, track avoidance successes
**Frontend**: Trigger log, pattern insights, trigger library, coping recommendations, success tracker

### 4. Journaling
**Backend**: Store journal entries with text, associated mood/stress, tags, analyze themes, sentiment analysis
**Frontend**: Journal editor, tag manager, theme insights, search functionality, export entries

### 5. Interventions & Coping
**Backend**: Track coping strategies applied, effectiveness ratings, recommend strategies, measure success rates
**Frontend**: Strategy library, quick apply interface, effectiveness dashboard, personalized recommendations

### 6. Analytics & Insights
**Backend**: Generate weekly insights, identify correlations (sleep, exercise, mood), calculate trends
**Frontend**: Insights dashboard, correlation visualizations, trend graphs, actionable recommendations

### 7. Check-ins & Reminders
**Backend**: Schedule check-in reminders, track missed check-ins, calculate streaks, emergency check-in triggers
**Frontend**: Check-in scheduler, reminder notifications, streak display, emergency support button

## Non-Functional Requirements
- HIPAA-compliant data storage for health information
- Encrypted journal entries
- Professional support recommendations for crisis situations
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs the specified function correctly
  - **AC3**: Feature handles error conditions gracefully
- Offline mood logging capability
- Anonymous data export for therapist sharing
- Dark mode for evening journaling


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

