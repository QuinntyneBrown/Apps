// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Query to get a letter by ID.
/// </summary>
public record GetLetterByIdQuery : IRequest<LetterDto?>
{
    /// <summary>
    /// Gets or sets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }
}

/// <summary>
/// Handler for GetLetterByIdQuery.
/// </summary>
public class GetLetterByIdQueryHandler : IRequestHandler<GetLetterByIdQuery, LetterDto?>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<GetLetterByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetLetterByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetLetterByIdQueryHandler(
        ILetterToFutureSelfContext context,
        ILogger<GetLetterByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<LetterDto?> Handle(GetLetterByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting letter {LetterId}",
            request.LetterId);

        var letter = await _context.Letters
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LetterId == request.LetterId, cancellationToken);

        return letter?.ToDto();
    }
}
