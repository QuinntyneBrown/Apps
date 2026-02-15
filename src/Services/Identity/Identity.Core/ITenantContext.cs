namespace Identity.Core;

public interface ITenantContext
{
    Guid TenantId { get; }
}
