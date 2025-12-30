using MediatR;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.ReturnWindows;

public class CreateReturnWindowCommand : IRequest<ReturnWindowDto>
{
    public Guid PurchaseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationDays { get; set; }
    public string? PolicyDetails { get; set; }
    public string? Conditions { get; set; }
    public decimal? RestockingFeePercent { get; set; }
    public string? Notes { get; set; }
}

public class CreateReturnWindowCommandHandler : IRequestHandler<CreateReturnWindowCommand, ReturnWindowDto>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public CreateReturnWindowCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReturnWindowDto> Handle(CreateReturnWindowCommand request, CancellationToken cancellationToken)
    {
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = request.PurchaseId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            DurationDays = request.DurationDays,
            Status = ReturnWindowStatus.Open,
            PolicyDetails = request.PolicyDetails,
            Conditions = request.Conditions,
            RestockingFeePercent = request.RestockingFeePercent,
            Notes = request.Notes
        };

        _context.ReturnWindows.Add(returnWindow);
        await _context.SaveChangesAsync(cancellationToken);

        return returnWindow.ToDto();
    }
}
