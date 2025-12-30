// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;

namespace BBQGrillingRecipeBook.Api.Features.CookSessions;

/// <summary>
/// Data transfer object for CookSession.
/// </summary>
public class CookSessionDto
{
    /// <summary>
    /// Gets or sets the cook session ID.
    /// </summary>
    public Guid CookSessionId { get; set; }

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
    public bool WasSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Maps a CookSession entity to CookSessionDto.
    /// </summary>
    public static CookSessionDto FromEntity(CookSession session)
    {
        return new CookSessionDto
        {
            CookSessionId = session.CookSessionId,
            UserId = session.UserId,
            RecipeId = session.RecipeId,
            CookDate = session.CookDate,
            ActualCookTimeMinutes = session.ActualCookTimeMinutes,
            TemperatureUsed = session.TemperatureUsed,
            Rating = session.Rating,
            Notes = session.Notes,
            Modifications = session.Modifications,
            WasSuccessful = session.WasSuccessful,
            CreatedAt = session.CreatedAt
        };
    }
}
