// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Command to create a new letter.
/// </summary>
public record CreateLetterCommand : IRequest<LetterDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

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
/// Handler for CreateLetterCommand.
/// </summary>
public class CreateLetterCommandHandler : IRequestHandler<CreateLetterCommand, LetterDto>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<CreateLetterCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateLetterCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateLetterCommandHandler(
        ILetterToFutureSelfContext context,
        ILogger<CreateLetterCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<LetterDto> Handle(CreateLetterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating letter for user {UserId}, subject: {Subject}",
            request.UserId,
            request.Subject);

        var letter = new Letter
        {
            LetterId = Guid.NewGuid(),
            UserId = request.UserId,
            Subject = request.Subject,
            Content = request.Content,
            WrittenDate = DateTime.UtcNow,
            ScheduledDeliveryDate = request.ScheduledDeliveryDate,
            DeliveryStatus = DeliveryStatus.Pending,
            HasBeenRead = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Letters.Add(letter);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created letter {LetterId} for user {UserId}",
            letter.LetterId,
            request.UserId);

        return letter.ToDto();
    }
}
