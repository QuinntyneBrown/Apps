// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.Account;

/// <summary>
/// Query to get all accounts.
/// </summary>
public record GetAccountsQuery : IRequest<List<AccountDto>>;

/// <summary>
/// Handler for GetAccountsQuery.
/// </summary>
public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountDto>>
{
    private readonly IPasswordAccountAuditorContext _context;

    public GetAccountsQueryHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<List<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _context.Accounts
            .AsNoTracking()
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return accounts.Select(a => a.ToDto()).ToList();
    }
}
