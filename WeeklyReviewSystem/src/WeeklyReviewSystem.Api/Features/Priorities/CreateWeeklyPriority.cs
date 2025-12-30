// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Priorities;

/// <summary>
/// Command to create a new weekly priority.
/// </summary>
public class CreateWeeklyPriorityCommand : IRequest<WeeklyPriorityDto>
{
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityLevel Level { get; set; }
    public string? Category { get; set; }
    public DateTime? TargetDate { get; set; }
}

/// <summary>
/// Handler for CreateWeeklyPriorityCommand.
/// </summary>
public class CreateWeeklyPriorityCommandHandler : IRequestHandler<CreateWeeklyPriorityCommand, WeeklyPriorityDto>
{
    private readonly IWeeklyReviewSystemContext _context;

    public CreateWeeklyPriorityCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<WeeklyPriorityDto> Handle(CreateWeeklyPriorityCommand request, CancellationToken cancellationToken)
    {
        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = request.WeeklyReviewId,
            Title = request.Title,
            Description = request.Description,
            Level = request.Level,
            Category = request.Category,
            TargetDate = request.TargetDate,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Priorities.Add(priority);
        await _context.SaveChangesAsync(cancellationToken);

        return priority.ToDto();
    }
}
