// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Accomplishments;

/// <summary>
/// Command to create a new accomplishment.
/// </summary>
public class CreateAccomplishmentCommand : IRequest<AccomplishmentDto>
{
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public int? ImpactLevel { get; set; }
}

/// <summary>
/// Handler for CreateAccomplishmentCommand.
/// </summary>
public class CreateAccomplishmentCommandHandler : IRequestHandler<CreateAccomplishmentCommand, AccomplishmentDto>
{
    private readonly IWeeklyReviewSystemContext _context;

    public CreateAccomplishmentCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<AccomplishmentDto> Handle(CreateAccomplishmentCommand request, CancellationToken cancellationToken)
    {
        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = request.WeeklyReviewId,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            ImpactLevel = request.ImpactLevel,
            CreatedAt = DateTime.UtcNow
        };

        _context.Accomplishments.Add(accomplishment);
        await _context.SaveChangesAsync(cancellationToken);

        return accomplishment.ToDto();
    }
}
