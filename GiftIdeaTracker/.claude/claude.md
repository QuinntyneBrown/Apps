# GiftIdeaTracker - Claude Code Context

## Project Overview

GiftIdeaTracker is a full-stack application built with:
- **Backend**: .NET (3 projects: Core, Infrastructure, Api)
- **Frontend**: Angular workspace with Angular Material
- **Database**: SQL Server Express
- **State Management**: RxJS (no NgRx, no Angular signals)
- **Testing**: Jest (unit), Playwright (e2e)

## Critical Architectural Constraints

**MUST FOLLOW - These are non-negotiable:**

1. **NO Repository Pattern** - Data access goes directly through `IGiftIdeaTrackerContext` interface
2. **Services in Core (PREFERRED)** - Business logic belongs in `GiftIdeaTracker.Core\Services` unless it has infrastructure dependencies
3. **Flattened Namespaces** - Namespaces MUST match physical file locations exactly
4. **One Type Per File** - Each file contains exactly one class/enum/record/interface
5. **Multi-Tenancy** - All aggregates MUST have `TenantId` property with row-level filtering

## Project Structure

### Backend Projects

```
GiftIdeaTracker.Core/
├── Model/
│   └── {Aggregate}Aggregate/
│       ├── Events/
│       ├── Enums/
│       └── {Aggregate}.cs (root)
├── Services/           # PREFERRED location for services
└── IGiftIdeaTrackerContext.cs

GiftIdeaTracker.Infrastructure/
├── GiftIdeaTrackerContext.cs
├── Migrations/
├── EntityConfigurations/
└── Services/          # Only for services with infrastructure dependencies

GiftIdeaTracker.Api/
├── Features/          # MediatR Commands/Queries by bounded context
│   └── {Context}/
│       ├── {Command}.cs
│       └── {Dto}.cs
├── Controllers/
└── Behaviours/        # Optional MediatR behaviors
```

### Frontend Structure

```
src/GiftIdeaTracker.WebApp/projects/GiftIdeaTracker/src/
├── app/
│   ├── pages/         # Routable components (no "Component" suffix)
│   ├── components/    # Reusable components
│   └── index.ts       # Barrel exports for every folder
└── e2e/              # Playwright tests
```

## Naming Conventions

### Backend
- Identity properties: `{Entity}Id` (NOT just `Id`)
- Namespaces: MUST match file path exactly
- Example: File at `Core/Model/GiftAggregate/Events/GiftCreated.cs` → `namespace GiftIdeaTracker.Core.Model.GiftAggregate.Events;`

### Frontend
- Component classes: NO "Component" suffix (e.g., `export class Header`)
- Component files: NO "component" prefix (e.g., `header.ts`, `header.html`, `header.scss`)
- Observables: Use `$` suffix (e.g., `data$`)
- CSS: Use BEM (Block Element Modifier) naming

## Development Standards

### Code Quality
- **Backend**: StyleCop.Analyzers + Microsoft.CodeAnalysis.NetAnalyzers (warnings = errors, blocks build)
- **Frontend**: ESLint with @angular-eslint (errors block build)
- **Simplicity**: Always choose the simplest solution
- No AutoMapper - use `ToDto()` extension methods in Api layer
- No commented-out code or debug statements

### Data Loading Pattern (CRITICAL)
Components MUST use async pipe pattern:

**Correct:**
```typescript
export class MyComponent {
  data$ = this.service.getData();
}
```
Template: `<div *ngIf="data$ | async as data">{{ data }}</div>`

**Incorrect:**
```typescript
export class MyComponent implements OnInit {
  data: any;
  ngOnInit() {
    this.service.getData().subscribe(result => this.data = result);
  }
}
```

### API Configuration
- CORS: Origins from config, include all frontend URLs
- JSON: Enums serialized as strings (use `JsonStringEnumConverter`)
- baseUrl: Frontend config contains ONLY base URI (e.g., `http://localhost:3200`), services append `/api/...`

### Logging (Serilog)
- **Information**: Normal operations, API calls
- **Warning**: Validation failures, business rule violations
- **Error**: Exceptions, external service failures with full context
- **Critical**: System failures, data corruption
- Enrichment: CorrelationId, UserId, Timestamp, context IDs
- NEVER log sensitive data (passwords, tokens, PII)

### Multi-Tenancy
- Every aggregate root has `TenantId: Guid`
- Global query filters auto-apply in DbContext
- Extract from JWT claims (`tenant_id`) or header (`X-Tenant-Id`)
- Command handlers inject `ITenantContext` for TenantId

## Testing Requirements

All features MUST have:
- Unit tests passing
- Integration tests passing
- Playwright e2e tests passing
- Meaningful test coverage (not just metrics)

## Design & UI

- Angular Material ONLY (default colors, Material 3 guidelines)
- NO custom colors outside theme
- Mobile-first, responsive design
- Design tokens for consistent spacing

## Definition of Done

Before marking any work as complete, verify:

1. **Specification Compliance**: Adheres to [implementation-specs.md](docs/specs/implementation-specs.md)
2. **Build**: Backend AND frontend build without errors/warnings
3. **Tests**: All unit, integration, and Playwright tests pass
4. **Code Quality**: Linting passes, no security vulnerabilities, follows conventions
5. **Documentation**: Code comments where needed, README updated
6. **Git**: Clear commit messages, no merge conflicts, pushed to remote

## Key Files

- **Specs**: [docs/specs/implementation-specs.md](docs/specs/implementation-specs.md) - Full technical requirements
- **DoD**: [docs/definition-of-done.md](docs/definition-of-done.md) - Completion criteria

## Quick Reference

- Database: SQL Server Express
- State: RxJS (NO NgRx, NO signals)
- Mapping: Extension methods with `ToDto()` (NO AutoMapper)
- Data Access: `IGiftIdeaTrackerContext` directly (NO repositories)
- Service Location: `Core\Services` preferred, `Infrastructure\Services` if needed
