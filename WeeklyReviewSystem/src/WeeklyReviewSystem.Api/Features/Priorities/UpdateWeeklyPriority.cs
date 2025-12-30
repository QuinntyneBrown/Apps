// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Priorities;

/// <summary>
/// Command to update an existing weekly priority.
/// </summary>
public class UpdateWeeklyPriorityCommand : IRequest<WeeklyPriorityDto?>
{
    public Guid WeeklyPriorityId { get; set; }
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityLevel Level { get; set; }
    public string? Category { get; set; }
    public DateTime? TargetDate { get; set; }
    public bool IsCompleted { get; set; }
}

/// <summary>
/// Handler for UpdateWeeklyPriorityCommand.
/// </summary>
public class UpdateWeeklyPriorityCommandHandler : IRequestHandler<UpdateWeeklyPriorityCommand, WeeklyPriorityDto?>
{
    private readonly IWeeklyReviewSystemContext _context;

    public UpdateWeeklyPriorityCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<WeeklyPriorityDto?> Handle(UpdateWeeklyPriorityCommand request, CancellationToken cancellationToken)
    {
        var priority = await _context.Priorities
            .FirstOrDefaultAsync(p => p.WeeklyPriorityId == request.WeeklyPriorityId, cancellationToken);

        if (priority == null)
        {
            return null;
        }

        priority.WeeklyReviewId = request.WeeklyReviewId;
        priority.Title = request.Title;
        priority.Description = request.Description;
        priority.Level = request.Level;
        priority.Category = request.Category;
        priority.TargetDate = request.TargetDate;
        priority.IsCompleted = request.IsCompleted;

        await _context.SaveChangesAsync(cancellationToken);

        return priority.ToDto();
    }
}
