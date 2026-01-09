# Implementation Specifications

## Architecture Constraints

### Backend (.NET Core)

#### 1. NO Repository Pattern
- Use `IAutomatedMarketIntelligenceToolContext` directly in services
- Do NOT create repository classes or interfaces
- DbContext is injected directly into services

#### 2. Services Location
- Business logic services belong in the **Core** project (PREFERRED)
- Services use the DbContext interface for data access
- Keep services focused and single-responsibility

#### 3. Namespace Architecture
- Namespaces MUST match the physical file location
- Example: `AutomatedMarketIntelligenceTool.Core.Models.CompetitorAggregate`
- No abbreviated or flattened namespaces

#### 4. One Type Per File
- Each file contains exactly ONE class, interface, enum, or record
- File name matches the type name exactly
- No partial classes unless absolutely necessary

#### 5. Entity Naming Conventions
- Primary key properties include entity name: `CompetitorId`, `AlertId`, `InsightId`
- NOT generic `Id` property
- Foreign keys follow same pattern: `CompetitorId` on related entities

#### 6. No AutoMapper
- Use extension methods with `ToDto()` pattern
- Define extension methods in the same namespace as the DTO
- Keep mapping logic simple and explicit

#### 7. Multi-Tenancy
- All aggregate roots MUST have `TenantId` property
- Global query filters apply tenant isolation automatically
- Never bypass tenant filtering without explicit justification

#### 8. Structured Logging
- Use Serilog for all logging
- Log levels: Information, Warning, Error, Critical
- Enrich logs with CorrelationId, UserId, Timestamp
- No sensitive data in logs

---

### Frontend (Angular)

#### 1. Angular Material Only
- Use Angular Material 3 components exclusively
- Follow Material Design guidelines for spacing and layout
- Use default Material theme colors
- NO custom color palettes

#### 2. No NgRx
- Use RxJS BehaviorSubjects for state management
- Services expose observables for reactive data flow
- Keep state management simple and localized

#### 3. No Angular Signals
- Use traditional RxJS observables
- Async pipe pattern in templates
- BehaviorSubjects for mutable state

#### 4. Async Pipe Pattern (ReactiveDataLoading)
- ALWAYS use async pipe in templates
- NO manual `.subscribe()` in components
- Combine observables with `combineLatest` or `forkJoin`
- Use `map` and `switchMap` for transformations

```typescript
// CORRECT
viewModel$ = this.service.data$.pipe(
  map(data => ({ items: data, count: data.length }))
);

// INCORRECT - Never do this
ngOnInit() {
  this.service.data$.subscribe(data => this.data = data);
}
```

#### 5. BEM CSS Naming Convention
- Block: `.sidebar`
- Element: `.sidebar__item`, `.sidebar__icon`
- Modifier: `.sidebar__item--active`, `.sidebar__item--disabled`

#### 6. Design Tokens
- Use SCSS variables from `_tokens.scss`
- Consistent spacing scale (8px base unit)
- Responsive breakpoints defined as tokens

#### 7. Component File Separation
- Separate files for `.ts`, `.html`, `.scss`
- NO inline templates or styles
- Each component folder has `index.ts` barrel export

#### 8. Naming Conventions
- NO "Component" suffix in class names: `Sidebar`, not `SidebarComponent`
- NO "component" in file names: `sidebar.component.ts` becomes `sidebar.ts`
- Wait, actually keep `.component.ts` in filename but class is just `Sidebar`

#### 9. Testing
- Jest/Vitest for unit tests
- Playwright for E2E tests
- Mock HTTP responses in E2E tests
- E2E tests in `/e2e` folder

---

## System-Wide Requirements

### API Response Format

```json
{
  "data": { },
  "success": true,
  "message": null,
  "errors": []
}
```

### Error Response Format

```json
{
  "data": null,
  "success": false,
  "message": "Validation failed",
  "errors": ["Field X is required", "Field Y must be positive"]
}
```

### HTTP Status Codes
- 200: Success
- 201: Created
- 204: No Content (successful delete)
- 400: Bad Request (validation errors)
- 401: Unauthorized
- 403: Forbidden
- 404: Not Found
- 500: Internal Server Error

---

## Domain Model

### Core Aggregates

#### Competitor
- CompetitorId (PK)
- TenantId
- Name
- Industry
- Website
- Description
- EmployeeCount
- AnnualRevenue
- MarketPosition (Leader, Challenger, Follower, Nicher)
- Strengths
- Weaknesses
- CreatedAt
- UpdatedAt

#### Insight
- InsightId (PK)
- TenantId
- Title
- Description
- Category (Trend, Opportunity, Threat, General)
- Impact (High, Medium, Low)
- Source
- SourceUrl
- Tags[]
- IsActionable
- CreatedAt
- UpdatedAt

#### Alert
- AlertId (PK)
- TenantId
- Name
- Description
- AlertType (CompetitorActivity, MarketThreshold, Keyword)
- IsActive
- Criteria (JSON)
- NotificationPreference (Email, InApp, Both)
- LastTriggered
- CreatedAt
- UpdatedAt

#### Report
- ReportId (PK)
- TenantId
- Title
- Description
- ReportType (Competitive, Market, Trend)
- GeneratedAt
- Content
- CreatedAt

#### DataSource
- DataSourceId (PK)
- TenantId
- Name
- Type (News, Social, Financial, Industry, Government)
- Url
- ReliabilityScore (1-10)
- IsActive
- LastFetched
- CreatedAt
- UpdatedAt

---

## Folder Structure

### Backend
```
src/
├── AutomatedMarketIntelligenceTool.Core/
│   ├── IAutomatedMarketIntelligenceToolContext.cs
│   ├── ITenantContext.cs
│   ├── Constants.cs
│   ├── Models/
│   │   ├── CompetitorAggregate/
│   │   │   ├── Competitor.cs
│   │   │   └── Enums/
│   │   │       └── MarketPosition.cs
│   │   ├── InsightAggregate/
│   │   │   ├── Insight.cs
│   │   │   └── Enums/
│   │   │       ├── InsightCategory.cs
│   │   │       └── InsightImpact.cs
│   │   ├── AlertAggregate/
│   │   │   ├── Alert.cs
│   │   │   └── Enums/
│   │   │       ├── AlertType.cs
│   │   │       └── NotificationPreference.cs
│   │   └── ...
│   └── Services/
│       ├── CompetitorService.cs
│       ├── InsightService.cs
│       └── AlertService.cs
├── AutomatedMarketIntelligenceTool.Infrastructure/
│   ├── AutomatedMarketIntelligenceToolContext.cs
│   └── Migrations/
└── AutomatedMarketIntelligenceTool.Api/
    ├── Program.cs
    ├── Controllers/
    └── Features/
```

### Frontend
```
projects/automated-market-intelligence-tool/src/
├── app/
│   ├── app.component.ts
│   ├── app.config.ts
│   ├── app.routes.ts
│   ├── components/
│   │   ├── sidebar/
│   │   │   ├── sidebar.component.ts
│   │   │   ├── sidebar.component.html
│   │   │   ├── sidebar.component.scss
│   │   │   └── index.ts
│   │   └── header/
│   │       └── ...
│   ├── layouts/
│   │   └── main-layout/
│   │       └── ...
│   ├── pages/
│   │   ├── login/
│   │   ├── dashboard/
│   │   ├── competitors-list/
│   │   ├── competitor-form/
│   │   ├── alerts-list/
│   │   ├── insights-list/
│   │   └── reports/
│   ├── services/
│   │   ├── auth.service.ts
│   │   ├── competitors.service.ts
│   │   ├── insights.service.ts
│   │   ├── alerts.service.ts
│   │   └── index.ts
│   ├── guards/
│   │   └── auth.guard.ts
│   ├── interceptors/
│   │   ├── auth.interceptor.ts
│   │   └── tenant.interceptor.ts
│   ├── models/
│   │   ├── competitor.model.ts
│   │   ├── insight.model.ts
│   │   ├── alert.model.ts
│   │   └── index.ts
│   └── environments/
│       └── environment.ts
├── styles/
│   ├── _tokens.scss
│   ├── _utilities.scss
│   └── styles.scss
└── index.html
```

---

## Key Implementation Notes

1. **Environment Configuration**: `baseUrl` contains ONLY the base URI without `/api`. Services append `/api/` prefix.

2. **Barrel Exports**: Every folder with multiple files should have an `index.ts` for clean imports.

3. **Observables Naming**: Observable properties end with `$` suffix (e.g., `competitors$`, `alerts$`).

4. **Form Validation**: Use Angular reactive forms with Material form field error display.

5. **Loading States**: Show loading indicators during async operations.

6. **Error Handling**: Display user-friendly error messages in snackbars or inline.
