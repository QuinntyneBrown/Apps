// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Bankrolls;

public class GetBankrollsQuery : IRequest<List<BankrollDto>>
{
}

public class GetBankrollsQueryHandler : IRequestHandler<GetBankrollsQuery, List<BankrollDto>>
{
    private readonly IPokerGameTrackerContext _context;

    public GetBankrollsQueryHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<BankrollDto>> Handle(GetBankrollsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Bankrolls
            .Select(b => new BankrollDto
            {
                BankrollId = b.BankrollId,
                UserId = b.UserId,
                Amount = b.Amount,
                RecordedDate = b.RecordedDate,
                Notes = b.Notes,
                CreatedAt = b.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
