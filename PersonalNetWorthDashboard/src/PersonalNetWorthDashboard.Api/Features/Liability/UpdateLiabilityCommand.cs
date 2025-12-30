// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Liability;

public record UpdateLiabilityCommand(
    Guid LiabilityId,
    string Name,
    LiabilityType LiabilityType,
    decimal CurrentBalance,
    decimal? OriginalAmount,
    decimal? InterestRate,
    decimal? MonthlyPayment,
    string? Creditor,
    string? AccountNumber,
    DateTime? DueDate,
    string? Notes) : IRequest<LiabilityDto>;

public class UpdateLiabilityCommandHandler : IRequestHandler<UpdateLiabilityCommand, LiabilityDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public UpdateLiabilityCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<LiabilityDto> Handle(UpdateLiabilityCommand request, CancellationToken cancellationToken)
    {
        var liability = await _context.Liabilities
            .FirstOrDefaultAsync(x => x.LiabilityId == request.LiabilityId, cancellationToken)
            ?? throw new InvalidOperationException($"Liability with ID {request.LiabilityId} not found.");

        liability.Name = request.Name;
        liability.LiabilityType = request.LiabilityType;
        liability.CurrentBalance = request.CurrentBalance;
        liability.OriginalAmount = request.OriginalAmount;
        liability.InterestRate = request.InterestRate;
        liability.MonthlyPayment = request.MonthlyPayment;
        liability.Creditor = request.Creditor;
        liability.AccountNumber = request.AccountNumber;
        liability.DueDate = request.DueDate;
        liability.Notes = request.Notes;
        liability.LastUpdated = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return liability.ToDto();
    }
}
