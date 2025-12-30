// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PersonalNetWorthDashboard.Api.Features.Liability;

public record CreateLiabilityCommand(
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

public class CreateLiabilityCommandHandler : IRequestHandler<CreateLiabilityCommand, LiabilityDto>
{
    private readonly IPersonalNetWorthDashboardContext _context;

    public CreateLiabilityCommandHandler(IPersonalNetWorthDashboardContext context)
    {
        _context = context;
    }

    public async Task<LiabilityDto> Handle(CreateLiabilityCommand request, CancellationToken cancellationToken)
    {
        var liability = new Core.Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = request.Name,
            LiabilityType = request.LiabilityType,
            CurrentBalance = request.CurrentBalance,
            OriginalAmount = request.OriginalAmount,
            InterestRate = request.InterestRate,
            MonthlyPayment = request.MonthlyPayment,
            Creditor = request.Creditor,
            AccountNumber = request.AccountNumber,
            DueDate = request.DueDate,
            Notes = request.Notes,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Liabilities.Add(liability);
        await _context.SaveChangesAsync(cancellationToken);

        return liability.ToDto();
    }
}
