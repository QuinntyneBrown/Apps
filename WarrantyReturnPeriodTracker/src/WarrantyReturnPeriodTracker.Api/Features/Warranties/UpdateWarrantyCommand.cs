using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Warranties;

public class UpdateWarrantyCommand : IRequest<WarrantyDto>
{
    public Guid WarrantyId { get; set; }
    public WarrantyType WarrantyType { get; set; }
    public string Provider { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationMonths { get; set; }
    public WarrantyStatus Status { get; set; }
    public string? CoverageDetails { get; set; }
    public string? Terms { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Notes { get; set; }
}

public class UpdateWarrantyCommandHandler : IRequestHandler<UpdateWarrantyCommand, WarrantyDto>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public UpdateWarrantyCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<WarrantyDto> Handle(UpdateWarrantyCommand request, CancellationToken cancellationToken)
    {
        var warranty = await _context.Warranties
            .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);

        if (warranty == null)
        {
            throw new InvalidOperationException($"Warranty with ID {request.WarrantyId} not found.");
        }

        warranty.WarrantyType = request.WarrantyType;
        warranty.Provider = request.Provider;
        warranty.StartDate = request.StartDate;
        warranty.EndDate = request.EndDate;
        warranty.DurationMonths = request.DurationMonths;
        warranty.Status = request.Status;
        warranty.CoverageDetails = request.CoverageDetails;
        warranty.Terms = request.Terms;
        warranty.RegistrationNumber = request.RegistrationNumber;
        warranty.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return warranty.ToDto();
    }
}
