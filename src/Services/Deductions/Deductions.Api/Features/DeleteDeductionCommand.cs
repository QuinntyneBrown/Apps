using Deductions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Deductions.Api.Features;

public record DeleteDeductionCommand(Guid DeductionId) : IRequest<bool>;

public class DeleteDeductionCommandHandler : IRequestHandler<DeleteDeductionCommand, bool>
{
    private readonly IDeductionsDbContext _context;
    private readonly ILogger<DeleteDeductionCommandHandler> _logger;

    public DeleteDeductionCommandHandler(IDeductionsDbContext context, ILogger<DeleteDeductionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteDeductionCommand request, CancellationToken cancellationToken)
    {
        var deduction = await _context.Deductions
            .FirstOrDefaultAsync(d => d.DeductionId == request.DeductionId, cancellationToken);

        if (deduction == null) return false;

        _context.Deductions.Remove(deduction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deduction deleted: {DeductionId}", request.DeductionId);

        return true;
    }
}
