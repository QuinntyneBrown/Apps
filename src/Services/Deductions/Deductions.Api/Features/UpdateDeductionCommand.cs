using Deductions.Core;
using Deductions.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Deductions.Api.Features;

public record UpdateDeductionCommand(
    Guid DeductionId,
    string? Description,
    decimal? Amount,
    DateTime? Date,
    DeductionCategory? Category,
    string? Notes) : IRequest<DeductionDto?>;

public class UpdateDeductionCommandHandler : IRequestHandler<UpdateDeductionCommand, DeductionDto?>
{
    private readonly IDeductionsDbContext _context;
    private readonly ILogger<UpdateDeductionCommandHandler> _logger;

    public UpdateDeductionCommandHandler(IDeductionsDbContext context, ILogger<UpdateDeductionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DeductionDto?> Handle(UpdateDeductionCommand request, CancellationToken cancellationToken)
    {
        var deduction = await _context.Deductions
            .FirstOrDefaultAsync(d => d.DeductionId == request.DeductionId, cancellationToken);

        if (deduction == null) return null;

        if (request.Description != null) deduction.Description = request.Description;
        if (request.Amount.HasValue) deduction.Amount = request.Amount.Value;
        if (request.Date.HasValue) deduction.Date = request.Date.Value;
        if (request.Category.HasValue) deduction.Category = request.Category.Value;
        if (request.Notes != null) deduction.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deduction updated: {DeductionId}", deduction.DeductionId);

        return deduction.ToDto();
    }
}
