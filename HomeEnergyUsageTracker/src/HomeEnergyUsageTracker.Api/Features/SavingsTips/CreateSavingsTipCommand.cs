// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.SavingsTips;

public class CreateSavingsTipCommand : IRequest<SavingsTipDto>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateSavingsTipCommandHandler : IRequestHandler<CreateSavingsTipCommand, SavingsTipDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public CreateSavingsTipCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<SavingsTipDto> Handle(CreateSavingsTipCommand request, CancellationToken cancellationToken)
    {
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        _context.SavingsTips.Add(savingsTip);
        await _context.SaveChangesAsync(cancellationToken);

        return SavingsTipDto.FromSavingsTip(savingsTip);
    }
}
