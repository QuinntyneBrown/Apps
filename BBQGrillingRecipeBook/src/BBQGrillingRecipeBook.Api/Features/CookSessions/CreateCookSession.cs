// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;

namespace BBQGrillingRecipeBook.Api.Features.CookSessions;

/// <summary>
/// Command to create a new cook session.
/// </summary>
public class CreateCookSessionCommand : IRequest<CookSessionDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }

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
    public bool WasSuccessful { get; set; } = true;
}

/// <summary>
/// Handler for CreateCookSessionCommand.
/// </summary>
public class CreateCookSessionCommandHandler : IRequestHandler<CreateCookSessionCommand, CookSessionDto>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCookSessionCommandHandler"/> class.
    /// </summary>
    public CreateCookSessionCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<CookSessionDto> Handle(CreateCookSessionCommand request, CancellationToken cancellationToken)
    {
        var session = new CookSession
        {
            CookSessionId = Guid.NewGuid(),
            UserId = request.UserId,
            RecipeId = request.RecipeId,
            CookDate = request.CookDate,
            ActualCookTimeMinutes = request.ActualCookTimeMinutes,
            TemperatureUsed = request.TemperatureUsed,
            Rating = request.Rating,
            Notes = request.Notes,
            Modifications = request.Modifications,
            WasSuccessful = request.WasSuccessful,
            CreatedAt = DateTime.UtcNow
        };

        _context.CookSessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);

        return CookSessionDto.FromEntity(session);
    }
}
