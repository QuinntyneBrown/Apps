// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Query to get all letters for a user.
/// </summary>
public record GetLettersQuery : IRequest<IEnumerable<LetterDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }
}

/// <summary>
/// Handler for GetLettersQuery.
/// </summary>
public class GetLettersQueryHandler : IRequestHandler<GetLettersQuery, IEnumerable<LetterDto>>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<GetLettersQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetLettersQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetLettersQueryHandler(
        ILetterToFutureSelfContext context,
        ILogger<GetLettersQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<LetterDto>> Handle(GetLettersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting all letters for user {UserId}",
            request.UserId);

        var letters = await _context.Letters
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.WrittenDate)
            .ToListAsync(cancellationToken);

        return letters.Select(x => x.ToDto());
    }
}
