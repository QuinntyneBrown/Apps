// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.Account;

/// <summary>
/// Query to get an account by ID.
/// </summary>
public record GetAccountByIdQuery(Guid AccountId) : IRequest<AccountDto?>;

/// <summary>
/// Handler for GetAccountByIdQuery.
/// </summary>
public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto?>
{
    private readonly IPasswordAccountAuditorContext _context;

    public GetAccountByIdQueryHandler(IPasswordAccountAuditorContext context)
    {
        _context = context;
    }

    public async Task<AccountDto?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AccountId == request.AccountId, cancellationToken);

        return account?.ToDto();
    }
}
