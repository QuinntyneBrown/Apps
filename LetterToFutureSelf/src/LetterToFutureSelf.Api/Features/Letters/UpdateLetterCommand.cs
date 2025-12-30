// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Command to update an existing letter.
/// </summary>
public record UpdateLetterCommand : IRequest<LetterDto?>
{
    /// <summary>
    /// Gets or sets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }

    /// <summary>
    /// Gets or sets the subject.
    /// </summary>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    public string Content { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the scheduled delivery date.
    /// </summary>
    public DateTime ScheduledDeliveryDate { get; init; }
}

/// <summary>
/// Handler for UpdateLetterCommand.
/// </summary>
public class UpdateLetterCommandHandler : IRequestHandler<UpdateLetterCommand, LetterDto?>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<UpdateLetterCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateLetterCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateLetterCommandHandler(
        ILetterToFutureSelfContext context,
        ILogger<UpdateLetterCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<LetterDto?> Handle(UpdateLetterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating letter {LetterId}",
            request.LetterId);

        var letter = await _context.Letters
            .FirstOrDefaultAsync(x => x.LetterId == request.LetterId, cancellationToken);

        if (letter == null)
        {
            _logger.LogWarning("Letter {LetterId} not found", request.LetterId);
            return null;
        }

        letter.Subject = request.Subject;
        letter.Content = request.Content;
        letter.ScheduledDeliveryDate = request.ScheduledDeliveryDate;
        letter.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated letter {LetterId}", request.LetterId);

        return letter.ToDto();
    }
}
