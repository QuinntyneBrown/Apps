using DateManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateManagement.Api.Features;

public record UpdateImportantDateCommand(
    Guid ImportantDateId,
    string Title,
    DateTime Date,
    string? Description,
    int DaysBeforeReminder,
    bool IsActive) : IRequest<ImportantDateDto?>;

public class UpdateImportantDateCommandHandler : IRequestHandler<UpdateImportantDateCommand, ImportantDateDto?>
{
    private readonly IDateManagementDbContext _context;
    private readonly ILogger<UpdateImportantDateCommandHandler> _logger;

    public UpdateImportantDateCommandHandler(IDateManagementDbContext context, ILogger<UpdateImportantDateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ImportantDateDto?> Handle(UpdateImportantDateCommand request, CancellationToken cancellationToken)
    {
        var importantDate = await _context.ImportantDates
            .FirstOrDefaultAsync(d => d.ImportantDateId == request.ImportantDateId, cancellationToken);

        if (importantDate == null) return null;

        importantDate.Title = request.Title;
        importantDate.Date = request.Date;
        importantDate.Description = request.Description;
        importantDate.DaysBeforeReminder = request.DaysBeforeReminder;
        importantDate.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Important date updated: {ImportantDateId}", importantDate.ImportantDateId);

        return importantDate.ToDto();
    }
}
