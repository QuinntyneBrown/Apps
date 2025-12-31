namespace FamilyCalendarEventPlanner.Core.Tests;

/// <summary>
/// Test helpers for creating entities with default TenantId
/// </summary>
public static class TestHelpers
{
    /// <summary>
    /// Default TenantId to use in all tests
    /// </summary>
    public static readonly Guid DefaultTenantId = Guid.Parse("00000000-0000-0000-0000-000000000001");
}
