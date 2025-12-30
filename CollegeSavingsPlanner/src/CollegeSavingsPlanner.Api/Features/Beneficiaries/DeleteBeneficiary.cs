// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollegeSavingsPlanner.Api.Features.Beneficiaries;

/// <summary>
/// Command to delete a beneficiary.
/// </summary>
public class DeleteBeneficiaryCommand : IRequest<bool>
{
    public Guid BeneficiaryId { get; set; }
}

/// <summary>
/// Handler for DeleteBeneficiaryCommand.
/// </summary>
public class DeleteBeneficiaryCommandHandler : IRequestHandler<DeleteBeneficiaryCommand, bool>
{
    private readonly ICollegeSavingsPlannerContext _context;

    public DeleteBeneficiaryCommandHandler(ICollegeSavingsPlannerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBeneficiaryCommand request, CancellationToken cancellationToken)
    {
        var beneficiary = await _context.Beneficiaries
            .FirstOrDefaultAsync(b => b.BeneficiaryId == request.BeneficiaryId, cancellationToken);

        if (beneficiary == null)
        {
            return false;
        }

        _context.Beneficiaries.Remove(beneficiary);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
