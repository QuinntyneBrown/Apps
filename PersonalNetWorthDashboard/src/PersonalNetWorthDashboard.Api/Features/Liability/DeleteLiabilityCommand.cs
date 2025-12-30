// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Liability;

public record DeleteLiabilityCommand(Guid LiabilityId) : IRequest;

public class DeleteLiabilityCommandHandler : IRequestHandler<DeleteLiabilityCommand>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public DeleteLiabilityCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteLiabilityCommand request, CancellationToken cancellationToken)
    {
        var liability = await _context.Liabilities
            .FirstOrDefaultAsync(x => x.LiabilityId == request.LiabilityId, cancellationToken)
            ?? throw new InvalidOperationException($"Liability with ID {request.LiabilityId} not found.");

        _context.Liabilities.Remove(liability);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
