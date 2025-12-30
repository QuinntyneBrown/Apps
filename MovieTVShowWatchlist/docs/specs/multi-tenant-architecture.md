# Multi-Tenant Architecture Specification for MovieTVShowWatchlist

## Overview

This document describes the multi-tenant architecture implemented in MovieTVShowWatchlist, enabling complete data isolation between different tenants (organizations) sharing the same application instance.

## Architecture

### Tenant Isolation Strategy

MovieTVShowWatchlist uses a **shared database with tenant ID column** approach, where:

- All tenant data is stored in a single database
- Each record includes a `TenantId` column for tenant identification
- EF Core global query filters ensure automatic tenant data isolation
- All queries are automatically scoped to the current tenant

### Components

#### 1. ITenantContext Interface (`MovieTVShowWatchlist.Core/ITenantContext.cs`)

Defines the contract for accessing the current tenant context:

```csharp
public interface ITenantContext
{
    Guid TenantId { get; }
    bool HasTenant { get; }
}
```

#### 2. TenantContext Service (`MovieTVShowWatchlist.Infrastructure/Services/TenantContext.cs`)

Implements tenant resolution from:
- JWT token claims (`tenant_id` claim)
- HTTP header fallback (`X-Tenant-Id` header)

#### 3. Entity Tenant Properties

All aggregate root entities include a `TenantId` property:

- **User**: `TenantId` property for tenant isolation
- **Movie**: `TenantId` property for tenant isolation
- **TVShow**: `TenantId` property for tenant isolation
- **Episode**: `TenantId` property for tenant isolation
- **ContentGenre**: `TenantId` property for tenant isolation
- **ContentAvailability**: `TenantId` property for tenant isolation
- **WatchlistItem**: `TenantId` property for tenant isolation
- **ViewingRecord**: `TenantId` property for tenant isolation
- **EpisodeViewingRecord**: `TenantId` property for tenant isolation
- **ViewingCompanion**: `TenantId` property for tenant isolation
- **ShowProgress**: `TenantId` property for tenant isolation
- **AbandonedContent**: `TenantId` property for tenant isolation
- **BingeSession**: `TenantId` property for tenant isolation
- **Rating**: `TenantId` property for tenant isolation
- **SeasonRating**: `TenantId` property for tenant isolation
- **Review**: `TenantId` property for tenant isolation
- **ReviewTheme**: `TenantId` property for tenant isolation
- **Favorite**: `TenantId` property for tenant isolation
- **Recommendation**: `TenantId` property for tenant isolation
- **SimilarContent**: `TenantId` property for tenant isolation
- **GenrePreference**: `TenantId` property for tenant isolation
- **ViewingStreak**: `TenantId` property for tenant isolation
- **ViewingMilestone**: `TenantId` property for tenant isolation
- **ViewingStatistics**: `TenantId` property for tenant isolation
- **YearInReview**: `TenantId` property for tenant isolation
- **StreamingSubscription**: `TenantId` property for tenant isolation
- **WatchParty**: `TenantId` property for tenant isolation
- **WatchPartyParticipant**: `TenantId` property for tenant isolation

#### 4. Global Query Filters

The `MovieTVShowWatchlistContext` DbContext applies automatic query filters:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<Movie>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<TVShow>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<Episode>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ContentGenre>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ContentAvailability>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<WatchlistItem>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ViewingRecord>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<EpisodeViewingRecord>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ViewingCompanion>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ShowProgress>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<AbandonedContent>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<BingeSession>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<Rating>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<SeasonRating>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<Review>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ReviewTheme>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<Favorite>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<Recommendation>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<SimilarContent>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<GenrePreference>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ViewingStreak>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ViewingMilestone>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<ViewingStatistics>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<YearInReview>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<StreamingSubscription>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<WatchParty>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
    modelBuilder.Entity<WatchPartyParticipant>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
}
```

## Tenant Resolution Flow

```
1. HTTP Request arrives
   ↓
2. JWT Token extracted from Authorization header
   ↓
3. TenantContext reads tenant_id claim from token
   ↓
4. If no JWT, fallback to X-Tenant-Id header
   ↓
5. TenantId is available throughout the request
   ↓
6. EF Core query filters automatically scope all queries
```

## API Authentication

### JWT Token Structure

Tokens must include a `tenant_id` claim:

```json
{
  "sub": "user-id",
  "tenant_id": "00000000-0000-0000-0000-000000000001",
  "iat": 1234567890,
  "exp": 1234571490
}
```

### Header-Based Tenant Resolution

For scenarios where JWT is not available, clients can send:

```
X-Tenant-Id: 00000000-0000-0000-0000-000000000001
```

## Database Schema

### Tenant ID Column

All tenant-scoped tables include:

| Column | Type | Description |
|--------|------|-------------|
| TenantId | UNIQUEIDENTIFIER | Foreign key to tenant |

### Index Recommendations

For optimal performance, composite indexes should include TenantId:

```sql
CREATE INDEX IX_EntityName_TenantId ON EntityName(TenantId);
CREATE INDEX IX_EntityName_TenantId_UserId ON EntityName(TenantId, UserId);
```

## Security Considerations

### Data Isolation Guarantees

1. **Query-Level Isolation**: EF Core global filters prevent cross-tenant data access
2. **Write Protection**: Commands should validate TenantId before persisting
3. **No Direct SQL Access**: Raw SQL queries must include tenant filters manually

### Audit Trail

All data modifications should log:
- TenantId
- UserId
- Timestamp
- Operation type

## Migration Guide

### Adding TenantId to Existing Data

1. Add TenantId column with default value
2. Update existing records with appropriate TenantId
3. Remove default constraint
4. Add non-null constraint

```sql
ALTER TABLE EntityName ADD TenantId UNIQUEIDENTIFIER NULL;
UPDATE EntityName SET TenantId = 'default-tenant-id';
ALTER TABLE EntityName ALTER COLUMN TenantId UNIQUEIDENTIFIER NOT NULL;
```

## Configuration

### appsettings.json

```json
{
  "MultiTenancy": {
    "Enabled": true,
    "DefaultTenantId": "00000000-0000-0000-0000-000000000001",
    "RequireTenant": true
  }
}
```

## Testing

### Unit Tests

Mock ITenantContext for isolated testing:

```csharp
var mockTenantContext = new Mock<ITenantContext>();
mockTenantContext.Setup(x => x.TenantId).Returns(testTenantId);
mockTenantContext.Setup(x => x.HasTenant).Returns(true);
```

### Integration Tests

Use test fixtures with known tenant IDs:

```csharp
public class TenantIsolationTests
{
    private readonly Guid _tenantA = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private readonly Guid _tenantB = Guid.Parse("22222222-2222-2222-2222-222222222222");
    
    [Fact]
    public async Task TenantA_CannotSee_TenantB_Data()
    {
        // Test implementation
    }
}
```

## Performance Considerations

1. **Index TenantId**: All tenant-scoped tables should have TenantId indexes
2. **Partition by Tenant**: Consider table partitioning for large datasets
3. **Cache by Tenant**: Implement tenant-aware caching strategies

## Future Enhancements

1. **Tenant Management API**: CRUD operations for tenants
2. **Tenant Settings**: Per-tenant configuration
3. **Usage Tracking**: Tenant-level resource consumption
4. **Cross-Tenant Reporting**: Admin-level aggregate reports
