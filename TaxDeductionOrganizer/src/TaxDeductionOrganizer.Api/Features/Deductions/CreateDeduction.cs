// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Deductions;

public class CreateDeduction
{
    public class Command : IRequest<DeductionDto>
    {
        public Guid TaxYearId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public DeductionCategory Category { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, DeductionDto>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<DeductionDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var deduction = new Deduction
            {
                DeductionId = Guid.NewGuid(),
                TaxYearId = request.TaxYearId,
                Description = request.Description,
                Amount = request.Amount,
                Date = request.Date,
                Category = request.Category,
                Notes = request.Notes,
                HasReceipt = false
            };

            _context.Deductions.Add(deduction);
            await _context.SaveChangesAsync(cancellationToken);

            return deduction.ToDto();
        }
    }
}
