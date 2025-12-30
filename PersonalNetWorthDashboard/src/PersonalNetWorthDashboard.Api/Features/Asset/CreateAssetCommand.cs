// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Asset;

public record CreateAssetCommand(
    string Name,
    AssetType AssetType,
    decimal CurrentValue,
    decimal? PurchasePrice,
    DateTime? PurchaseDate,
    string? Institution,
    string? AccountNumber,
    string? Notes) : IRequest<AssetDto>;

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, AssetDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public CreateAssetCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<AssetDto> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = new Core.Asset
        {
            AssetId = Guid.NewGuid(),
            Name = request.Name,
            AssetType = request.AssetType,
            CurrentValue = request.CurrentValue,
            PurchasePrice = request.PurchasePrice,
            PurchaseDate = request.PurchaseDate,
            Institution = request.Institution,
            AccountNumber = request.AccountNumber,
            Notes = request.Notes,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync(cancellationToken);

        return asset.ToDto();
    }
}
