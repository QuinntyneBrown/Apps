using Appliances.Core;
using Appliances.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appliances.Api.Features;

public record UpdateApplianceCommand(
    Guid ApplianceId,
    string Name,
    ApplianceType ApplianceType,
    string? Brand,
    string? ModelNumber,
    string? SerialNumber,
    DateTime? PurchaseDate,
    decimal? PurchasePrice) : IRequest<ApplianceDto?>;

public class UpdateApplianceCommandHandler : IRequestHandler<UpdateApplianceCommand, ApplianceDto?>
{
    private readonly IAppliancesDbContext _context;

    public UpdateApplianceCommandHandler(IAppliancesDbContext context)
    {
        _context = context;
    }

    public async Task<ApplianceDto?> Handle(UpdateApplianceCommand request, CancellationToken cancellationToken)
    {
        var appliance = await _context.Appliances
            .FirstOrDefaultAsync(a => a.ApplianceId == request.ApplianceId, cancellationToken);

        if (appliance == null) return null;

        appliance.Name = request.Name;
        appliance.ApplianceType = request.ApplianceType;
        appliance.Brand = request.Brand;
        appliance.ModelNumber = request.ModelNumber;
        appliance.SerialNumber = request.SerialNumber;
        appliance.PurchaseDate = request.PurchaseDate;
        appliance.PurchasePrice = request.PurchasePrice;

        await _context.SaveChangesAsync(cancellationToken);
        return appliance.ToDto();
    }
}
