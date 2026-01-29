namespace Identity.Core.Models.UserAggregate.Entities;

public class Role
{
    public Guid RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
