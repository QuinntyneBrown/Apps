// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Mortgage;

/// <summary>
/// Command to update an existing mortgage.
/// </summary>
public record UpdateMortgageCommand : IRequest<MortgageDto>
{
    public Guid MortgageId { get; set; }
    public string PropertyAddress { get; set; } = string.Empty;
    public string Lender { get; set; } = string.Empty;
    public decimal OriginalLoanAmount { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal InterestRate { get; set; }
    public int LoanTermYears { get; set; }
    public decimal MonthlyPayment { get; set; }
    public DateTime StartDate { get; set; }
    public MortgageType MortgageType { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdateMortgageCommand.
/// </summary>
public class UpdateMortgageCommandHandler : IRequestHandler<UpdateMortgageCommand, MortgageDto>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public UpdateMortgageCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<MortgageDto> Handle(UpdateMortgageCommand request, CancellationToken cancellationToken)
    {
        var mortgage = await _context.Mortgages
            .FirstOrDefaultAsync(m => m.MortgageId == request.MortgageId, cancellationToken);

        if (mortgage == null)
        {
            throw new Exception($"Mortgage with ID {request.MortgageId} not found.");
        }

        mortgage.PropertyAddress = request.PropertyAddress;
        mortgage.Lender = request.Lender;
        mortgage.OriginalLoanAmount = request.OriginalLoanAmount;
        mortgage.CurrentBalance = request.CurrentBalance;
        mortgage.InterestRate = request.InterestRate;
        mortgage.LoanTermYears = request.LoanTermYears;
        mortgage.MonthlyPayment = request.MonthlyPayment;
        mortgage.StartDate = request.StartDate;
        mortgage.MortgageType = request.MortgageType;
        mortgage.IsActive = request.IsActive;
        mortgage.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return mortgage.ToDto();
    }
}
