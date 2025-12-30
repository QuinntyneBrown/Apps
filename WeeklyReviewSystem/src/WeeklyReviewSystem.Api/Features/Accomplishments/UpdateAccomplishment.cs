// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Accomplishments;

/// <summary>
/// Command to update an existing accomplishment.
/// </summary>
public class UpdateAccomplishmentCommand : IRequest<AccomplishmentDto?>
{
    public Guid AccomplishmentId { get; set; }
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public int? ImpactLevel { get; set; }
}

/// <summary>
/// Handler for UpdateAccomplishmentCommand.
/// </summary>
public class UpdateAccomplishmentCommandHandler : IRequestHandler<UpdateAccomplishmentCommand, AccomplishmentDto?>
{
    private readonly IWeeklyReviewSystemContext _context;

    public UpdateAccomplishmentCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<AccomplishmentDto?> Handle(UpdateAccomplishmentCommand request, CancellationToken cancellationToken)
    {
        var accomplishment = await _context.Accomplishments
            .FirstOrDefaultAsync(a => a.AccomplishmentId == request.AccomplishmentId, cancellationToken);

        if (accomplishment == null)
        {
            return null;
        }

        accomplishment.WeeklyReviewId = request.WeeklyReviewId;
        accomplishment.Title = request.Title;
        accomplishment.Description = request.Description;
        accomplishment.Category = request.Category;
        accomplishment.ImpactLevel = request.ImpactLevel;

        await _context.SaveChangesAsync(cancellationToken);

        return accomplishment.ToDto();
    }
}
