// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.DigitalAccounts.Queries;

/// <summary>
/// Query to get a digital account by ID.
/// </summary>
public class GetDigitalAccountByIdQuery : IRequest<DigitalAccountDto?>
{
    public Guid DigitalAccountId { get; set; }
}

/// <summary>
/// Handler for GetDigitalAccountByIdQuery.
/// </summary>
public class GetDigitalAccountByIdQueryHandler : IRequestHandler<GetDigitalAccountByIdQuery, DigitalAccountDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public GetDigitalAccountByIdQueryHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<DigitalAccountDto?> Handle(GetDigitalAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.DigitalAccountId == request.DigitalAccountId, cancellationToken);

        if (account == null)
        {
            return null;
        }

        return new DigitalAccountDto
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
        };
    }
}
