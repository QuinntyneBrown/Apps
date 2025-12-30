using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Deadlines;

public record UpdateDeadlineCommand : IRequest<DeadlineDto?>
{
    public Guid DeadlineId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime DeadlineDateTime { get; init; }
    public string? Category { get; init; }
    public bool RemindersEnabled { get; init; }
    public int ReminderDaysAdvance { get; init; }
    public string? Notes { get; init; }
}

public class UpdateDeadlineCommandHandler : IRequestHandler<UpdateDeadlineCommand, DeadlineDto?>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<UpdateDeadlineCommandHandler> _logger;

    public UpdateDeadlineCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<UpdateDeadlineCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DeadlineDto?> Handle(UpdateDeadlineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating deadline {DeadlineId}", request.DeadlineId);

        var deadline = await _context.Deadlines
            .FirstOrDefaultAsync(d => d.DeadlineId == request.DeadlineId, cancellationToken);

        if (deadline == null)
        {
            _logger.LogWarning("Deadline {DeadlineId} not found", request.DeadlineId);
            return null;
        }

        deadline.Title = request.Title;
        deadline.Description = request.Description;
        deadline.DeadlineDateTime = request.DeadlineDateTime;
        deadline.Category = request.Category;
        deadline.RemindersEnabled = request.RemindersEnabled;
        deadline.ReminderDaysAdvance = request.ReminderDaysAdvance;
        deadline.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated deadline {DeadlineId}", request.DeadlineId);

        return deadline.ToDto();
    }
}
