// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.CookSessions;

/// <summary>
/// Query to get a cook session by ID.
/// </summary>
public class GetCookSessionByIdQuery : IRequest<CookSessionDto?>
{
    /// <summary>
    /// Gets or sets the cook session ID.
    /// </summary>
    public Guid CookSessionId { get; set; }
}

/// <summary>
/// Handler for GetCookSessionByIdQuery.
/// </summary>
public class GetCookSessionByIdQueryHandler : IRequestHandler<GetCookSessionByIdQuery, CookSessionDto?>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCookSessionByIdQueryHandler"/> class.
    /// </summary>
    public GetCookSessionByIdQueryHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<CookSessionDto?> Handle(GetCookSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var session = await _context.CookSessions
            .FirstOrDefaultAsync(s => s.CookSessionId == request.CookSessionId, cancellationToken);

        return session == null ? null : CookSessionDto.FromEntity(session);
    }
}
