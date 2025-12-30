// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.CookSessions;

/// <summary>
/// Command to update a cook session.
/// </summary>
public class UpdateCookSessionCommand : IRequest<CookSessionDto?>
{
    /// <summary>
    /// Gets or sets the cook session ID.
    /// </summary>
    public Guid CookSessionId { get; set; }

    /// <summary>
    /// Gets or sets the cook date.
    /// </summary>
    public DateTime CookDate { get; set; }

    /// <summary>
    /// Gets or sets the actual cook time in minutes.
    /// </summary>
    public int ActualCookTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the temperature used.
    /// </summary>
    public int? TemperatureUsed { get; set; }

    /// <summary>
    /// Gets or sets the rating.
    /// </summary>
    public int? Rating { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the modifications.
    /// </summary>
    public string? Modifications { get; set; }

    /// <summary>
    /// Gets or sets whether the session was successful.
    /// </summary>
    public bool WasSuccessful { get; set; }
}

/// <summary>
/// Handler for UpdateCookSessionCommand.
/// </summary>
public class UpdateCookSessionCommandHandler : IRequestHandler<UpdateCookSessionCommand, CookSessionDto?>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCookSessionCommandHandler"/> class.
    /// </summary>
    public UpdateCookSessionCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<CookSessionDto?> Handle(UpdateCookSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.CookSessions
            .FirstOrDefaultAsync(s => s.CookSessionId == request.CookSessionId, cancellationToken);

        if (session == null)
        {
            return null;
        }

        session.CookDate = request.CookDate;
        session.ActualCookTimeMinutes = request.ActualCookTimeMinutes;
        session.TemperatureUsed = request.TemperatureUsed;
        session.Rating = request.Rating;
        session.Notes = request.Notes;
        session.Modifications = request.Modifications;
        session.WasSuccessful = request.WasSuccessful;

        await _context.SaveChangesAsync(cancellationToken);

        return CookSessionDto.FromEntity(session);
    }
}
