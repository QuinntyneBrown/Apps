// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.DigitalAccounts.Queries;

/// <summary>
/// Query to get all digital accounts for a user.
/// </summary>
public class GetDigitalAccountsQuery : IRequest<List<DigitalAccountDto>>
{
    public Guid? UserId { get; set; }
    public AccountType? AccountType { get; set; }
}

/// <summary>
/// Handler for GetDigitalAccountsQuery.
/// </summary>
public class GetDigitalAccountsQueryHandler : IRequestHandler<GetDigitalAccountsQuery, List<DigitalAccountDto>>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public GetDigitalAccountsQueryHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<List<DigitalAccountDto>> Handle(GetDigitalAccountsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Accounts.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        if (request.AccountType.HasValue)
        {
            query = query.Where(a => a.AccountType == request.AccountType.Value);
        }

        var accounts = await query.ToListAsync(cancellationToken);

        return accounts.Select(account => new DigitalAccountDto
        {
            DigitalAccountId = account.DigitalAccountId,
            UserId = account.UserId,
            AccountType = account.AccountType,
            AccountName = account.AccountName,
            Username = account.Username,
            PasswordHint = account.PasswordHint,
            Url = account.Url,
            DesiredAction = account.DesiredAction,
            Notes = account.Notes,
            CreatedAt = account.CreatedAt,
            LastUpdatedAt = account.LastUpdatedAt
        }).ToList();
    }
}
