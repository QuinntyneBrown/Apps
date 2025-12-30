// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Asset;

public record UpdateAssetCommand(
    Guid AssetId,
    string Name,
    AssetType AssetType,
    decimal CurrentValue,
    decimal? PurchasePrice,
    DateTime? PurchaseDate,
    string? Institution,
    string? AccountNumber,
    string? Notes) : IRequest<AssetDto>;

public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, AssetDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public UpdateAssetCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<AssetDto> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets
            .FirstOrDefaultAsync(x => x.AssetId == request.AssetId, cancellationToken)
            ?? throw new InvalidOperationException($"Asset with ID {request.AssetId} not found.");

        asset.Name = request.Name;
        asset.AssetType = request.AssetType;
        asset.CurrentValue = request.CurrentValue;
        asset.PurchasePrice = request.PurchasePrice;
        asset.PurchaseDate = request.PurchaseDate;
        asset.Institution = request.Institution;
        asset.AccountNumber = request.AccountNumber;
        asset.Notes = request.Notes;
        asset.LastUpdated = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return asset.ToDto();
    }
}
