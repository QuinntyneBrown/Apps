using MediatR;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Warranties;

public class CreateWarrantyCommand : IRequest<WarrantyDto>
{
    public Guid PurchaseId { get; set; }
    public WarrantyType WarrantyType { get; set; }
    public string Provider { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationMonths { get; set; }
    public string? CoverageDetails { get; set; }
    public string? Terms { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Notes { get; set; }
}

public class CreateWarrantyCommandHandler : IRequestHandler<CreateWarrantyCommand, WarrantyDto>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public CreateWarrantyCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<WarrantyDto> Handle(CreateWarrantyCommand request, CancellationToken cancellationToken)
    {
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = request.PurchaseId,
            WarrantyType = request.WarrantyType,
            Provider = request.Provider,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            DurationMonths = request.DurationMonths,
            Status = WarrantyStatus.Active,
            CoverageDetails = request.CoverageDetails,
            Terms = request.Terms,
            RegistrationNumber = request.RegistrationNumber,
            Notes = request.Notes
        };

        _context.Warranties.Add(warranty);
        await _context.SaveChangesAsync(cancellationToken);

        return warranty.ToDto();
    }
}
