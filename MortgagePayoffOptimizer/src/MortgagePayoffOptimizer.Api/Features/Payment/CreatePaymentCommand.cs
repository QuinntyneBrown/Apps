// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Payment;

/// <summary>
/// Command to create a new payment.
/// </summary>
public record CreatePaymentCommand : IRequest<PaymentDto>
{
    public Guid MortgageId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal? ExtraPrincipal { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreatePaymentCommand.
/// </summary>
public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public CreatePaymentCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = new Core.Payment
        {
            PaymentId = Guid.NewGuid(),
            MortgageId = request.MortgageId,
            PaymentDate = request.PaymentDate,
            Amount = request.Amount,
            PrincipalAmount = request.PrincipalAmount,
            InterestAmount = request.InterestAmount,
            ExtraPrincipal = request.ExtraPrincipal,
            Notes = request.Notes
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync(cancellationToken);

        return payment.ToDto();
    }
}
