// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Mortgage;

/// <summary>
/// Command to create a new mortgage.
/// </summary>
public record CreateMortgageCommand : IRequest<MortgageDto>
{
    public string PropertyAddress { get; set; } = string.Empty;
    public string Lender { get; set; } = string.Empty;
    public decimal OriginalLoanAmount { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal InterestRate { get; set; }
    public int LoanTermYears { get; set; }
    public decimal MonthlyPayment { get; set; }
    public DateTime StartDate { get; set; }
    public MortgageType MortgageType { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateMortgageCommand.
/// </summary>
public class CreateMortgageCommandHandler : IRequestHandler<CreateMortgageCommand, MortgageDto>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public CreateMortgageCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<MortgageDto> Handle(CreateMortgageCommand request, CancellationToken cancellationToken)
    {
        var mortgage = new Core.Mortgage
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = request.PropertyAddress,
            Lender = request.Lender,
            OriginalLoanAmount = request.OriginalLoanAmount,
            CurrentBalance = request.CurrentBalance,
            InterestRate = request.InterestRate,
            LoanTermYears = request.LoanTermYears,
            MonthlyPayment = request.MonthlyPayment,
            StartDate = request.StartDate,
            MortgageType = request.MortgageType,
            IsActive = request.IsActive,
            Notes = request.Notes
        };

        _context.Mortgages.Add(mortgage);
        await _context.SaveChangesAsync(cancellationToken);

        return mortgage.ToDto();
    }
}
