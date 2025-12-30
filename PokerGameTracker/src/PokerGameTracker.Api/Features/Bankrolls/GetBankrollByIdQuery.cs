// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Bankrolls;

public class GetBankrollByIdQuery : IRequest<BankrollDto?>
{
    public Guid BankrollId { get; set; }
}

public class GetBankrollByIdQueryHandler : IRequestHandler<GetBankrollByIdQuery, BankrollDto?>
{
    private readonly IPokerGameTrackerContext _context;

    public GetBankrollByIdQueryHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<BankrollDto?> Handle(GetBankrollByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Bankrolls
            .Where(b => b.BankrollId == request.BankrollId)
            .Select(b => new BankrollDto
            {
                BankrollId = b.BankrollId,
                UserId = b.UserId,
                Amount = b.Amount,
                RecordedDate = b.RecordedDate,
                Notes = b.Notes,
                CreatedAt = b.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
