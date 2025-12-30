// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Accomplishments;

/// <summary>
/// Command to delete an accomplishment.
/// </summary>
public class DeleteAccomplishmentCommand : IRequest<bool>
{
    public Guid AccomplishmentId { get; set; }
}

/// <summary>
/// Handler for DeleteAccomplishmentCommand.
/// </summary>
public class DeleteAccomplishmentCommandHandler : IRequestHandler<DeleteAccomplishmentCommand, bool>
{
    private readonly IWeeklyReviewSystemContext _context;

    public DeleteAccomplishmentCommandHandler(IWeeklyReviewSystemContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteAccomplishmentCommand request, CancellationToken cancellationToken)
    {
        var accomplishment = await _context.Accomplishments
            .FirstOrDefaultAsync(a => a.AccomplishmentId == request.AccomplishmentId, cancellationToken);

        if (accomplishment == null)
        {
            return false;
        }

        _context.Accomplishments.Remove(accomplishment);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
