using DateManagement.Core;
using DateManagement.Core.Models;
using MediatR;

namespace DateManagement.Api.Features;

public record CreateImportantDateCommand(
    Guid UserId,
    Guid TenantId,
    string Title,
    DateTime Date,
    string? Description,
    int DaysBeforeReminder,
    bool IsActive) : IRequest<ImportantDateDto>;

public class CreateImportantDateCommandHandler : IRequestHandler<CreateImportantDateCommand, ImportantDateDto>
{
    private readonly IDateManagementDbContext _context;
    private readonly ILogger<CreateImportantDateCommandHandler> _logger;

    public CreateImportantDateCommandHandler(IDateManagementDbContext context, ILogger<CreateImportantDateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ImportantDateDto> Handle(CreateImportantDateCommand request, CancellationToken cancellationToken)
    {
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            Title = request.Title,
            Date = request.Date,
            Description = request.Description,
            DaysBeforeReminder = request.DaysBeforeReminder,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        _context.ImportantDates.Add(importantDate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Important date created: {ImportantDateId}", importantDate.ImportantDateId);

        return importantDate.ToDto();
    }
}
