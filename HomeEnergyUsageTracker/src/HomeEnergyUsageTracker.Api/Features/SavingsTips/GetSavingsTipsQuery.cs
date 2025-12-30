// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.SavingsTips;

public class GetSavingsTipsQuery : IRequest<List<SavingsTipDto>>
{
}

public class GetSavingsTipsQueryHandler : IRequestHandler<GetSavingsTipsQuery, List<SavingsTipDto>>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public GetSavingsTipsQueryHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<SavingsTipDto>> Handle(GetSavingsTipsQuery request, CancellationToken cancellationToken)
    {
        var savingsTips = await _context.SavingsTips
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return savingsTips.Select(SavingsTipDto.FromSavingsTip).ToList();
    }
}
