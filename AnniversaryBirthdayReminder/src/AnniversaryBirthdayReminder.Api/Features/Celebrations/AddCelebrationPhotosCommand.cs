// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to add photos to a celebration.
/// </summary>
public record AddCelebrationPhotosCommand : IRequest<CelebrationDto?>
{
    /// <summary>
    /// Gets or sets the celebration ID.
    /// </summary>
    public Guid CelebrationId { get; init; }

    /// <summary>
    /// Gets or sets the photo URLs.
    /// </summary>
    public List<string> PhotoUrls { get; init; } = new List<string>();
}

/// <summary>
/// Handler for AddCelebrationPhotosCommand.
/// </summary>
public class AddCelebrationPhotosCommandHandler : IRequestHandler<AddCelebrationPhotosCommand, CelebrationDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<AddCelebrationPhotosCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddCelebrationPhotosCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public AddCelebrationPhotosCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<AddCelebrationPhotosCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<CelebrationDto?> Handle(AddCelebrationPhotosCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Adding {PhotoCount} photos to celebration {CelebrationId}",
            request.PhotoUrls.Count,
            request.CelebrationId);

        var celebration = await _context.Celebrations
            .FirstOrDefaultAsync(x => x.CelebrationId == request.CelebrationId, cancellationToken);

        if (celebration == null)
        {
            _logger.LogWarning(
                "Celebration {CelebrationId} not found",
                request.CelebrationId);
            return null;
        }

        celebration.AddPhotos(request.PhotoUrls);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Added {PhotoCount} photos to celebration {CelebrationId}",
            request.PhotoUrls.Count,
            request.CelebrationId);

        return celebration.ToDto();
    }
}
