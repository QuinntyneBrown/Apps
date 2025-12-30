using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Deadlines;

public record CreateDeadlineCommand : IRequest<DeadlineDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime DeadlineDateTime { get; init; }
    public string? Category { get; init; }
    public bool RemindersEnabled { get; init; } = true;
    public int ReminderDaysAdvance { get; init; } = 7;
    public string? Notes { get; init; }
}

public class CreateDeadlineCommandHandler : IRequestHandler<CreateDeadlineCommand, DeadlineDto>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<CreateDeadlineCommandHandler> _logger;

    public CreateDeadlineCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<CreateDeadlineCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DeadlineDto> Handle(CreateDeadlineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating deadline for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var deadline = new Deadline
        {
            DeadlineId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            DeadlineDateTime = request.DeadlineDateTime,
            Category = request.Category,
            IsCompleted = false,
            RemindersEnabled = request.RemindersEnabled,
            ReminderDaysAdvance = request.ReminderDaysAdvance,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Deadlines.Add(deadline);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created deadline {DeadlineId} for user {UserId}",
            deadline.DeadlineId,
            request.UserId);

        return deadline.ToDto();
    }
}
