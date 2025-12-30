// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Payment;

/// <summary>
/// Command to update an existing payment.
/// </summary>
public record UpdatePaymentCommand : IRequest<PaymentDto>
{
    public Guid PaymentId { get; set; }
    public Guid MortgageId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal? ExtraPrincipal { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdatePaymentCommand.
/// </summary>
public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, PaymentDto>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public UpdatePaymentCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<PaymentDto> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(p => p.PaymentId == request.PaymentId, cancellationToken);

        if (payment == null)
        {
            throw new Exception($"Payment with ID {request.PaymentId} not found.");
        }

        payment.MortgageId = request.MortgageId;
        payment.PaymentDate = request.PaymentDate;
        payment.Amount = request.Amount;
        payment.PrincipalAmount = request.PrincipalAmount;
        payment.InterestAmount = request.InterestAmount;
        payment.ExtraPrincipal = request.ExtraPrincipal;
        payment.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return payment.ToDto();
    }
}
