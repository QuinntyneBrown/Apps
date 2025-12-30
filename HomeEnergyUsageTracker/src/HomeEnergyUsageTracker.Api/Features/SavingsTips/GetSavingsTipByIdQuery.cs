// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.SavingsTips;

public class GetSavingsTipByIdQuery : IRequest<SavingsTipDto>
{
    public Guid SavingsTipId { get; set; }
}

public class GetSavingsTipByIdQueryHandler : IRequestHandler<GetSavingsTipByIdQuery, SavingsTipDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public GetSavingsTipByIdQueryHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<SavingsTipDto> Handle(GetSavingsTipByIdQuery request, CancellationToken cancellationToken)
    {
        var savingsTip = await _context.SavingsTips
            .FirstOrDefaultAsync(x => x.SavingsTipId == request.SavingsTipId, cancellationToken);

        if (savingsTip == null)
        {
            throw new KeyNotFoundException($"SavingsTip with id {request.SavingsTipId} not found.");
        }

        return SavingsTipDto.FromSavingsTip(savingsTip);
    }
}
