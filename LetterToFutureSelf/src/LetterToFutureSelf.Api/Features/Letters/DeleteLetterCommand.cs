// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Command to delete a letter.
/// </summary>
public record DeleteLetterCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }
}

/// <summary>
/// Handler for DeleteLetterCommand.
/// </summary>
public class DeleteLetterCommandHandler : IRequestHandler<DeleteLetterCommand, bool>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<DeleteLetterCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteLetterCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteLetterCommandHandler(
        ILetterToFutureSelfContext context,
        ILogger<DeleteLetterCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteLetterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting letter {LetterId}", request.LetterId);

        var letter = await _context.Letters
            .FirstOrDefaultAsync(x => x.LetterId == request.LetterId, cancellationToken);

        if (letter == null)
        {
            _logger.LogWarning("Letter {LetterId} not found", request.LetterId);
            return false;
        }

        _context.Letters.Remove(letter);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted letter {LetterId}", request.LetterId);

        return true;
    }
}
