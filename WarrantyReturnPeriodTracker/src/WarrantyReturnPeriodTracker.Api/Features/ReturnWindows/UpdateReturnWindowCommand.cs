using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.ReturnWindows;

public class UpdateReturnWindowCommand : IRequest<ReturnWindowDto>
{
    public Guid ReturnWindowId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DurationDays { get; set; }
    public ReturnWindowStatus Status { get; set; }
    public string? PolicyDetails { get; set; }
    public string? Conditions { get; set; }
    public decimal? RestockingFeePercent { get; set; }
    public string? Notes { get; set; }
}

public class UpdateReturnWindowCommandHandler : IRequestHandler<UpdateReturnWindowCommand, ReturnWindowDto>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public UpdateReturnWindowCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReturnWindowDto> Handle(UpdateReturnWindowCommand request, CancellationToken cancellationToken)
    {
        var returnWindow = await _context.ReturnWindows
            .FirstOrDefaultAsync(rw => rw.ReturnWindowId == request.ReturnWindowId, cancellationToken);

        if (returnWindow == null)
        {
            throw new InvalidOperationException($"Return window with ID {request.ReturnWindowId} not found.");
        }

        returnWindow.StartDate = request.StartDate;
        returnWindow.EndDate = request.EndDate;
        returnWindow.DurationDays = request.DurationDays;
        returnWindow.Status = request.Status;
        returnWindow.PolicyDetails = request.PolicyDetails;
        returnWindow.Conditions = request.Conditions;
        returnWindow.RestockingFeePercent = request.RestockingFeePercent;
        returnWindow.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return returnWindow.ToDto();
    }
}
