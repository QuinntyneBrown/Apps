// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.Recipes;

/// <summary>
/// Command to create a new recipe.
/// </summary>
public class CreateRecipeCommand : IRequest<RecipeDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the recipe name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipe description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the meat type.
    /// </summary>
    public MeatType MeatType { get; set; }

    /// <summary>
    /// Gets or sets the cooking method.
    /// </summary>
    public CookingMethod CookingMethod { get; set; }

    /// <summary>
    /// Gets or sets the preparation time in minutes.
    /// </summary>
    public int PrepTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the cooking time in minutes.
    /// </summary>
    public int CookTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the ingredients list.
    /// </summary>
    public string Ingredients { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cooking instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target temperature.
    /// </summary>
    public int? TargetTemperature { get; set; }

    /// <summary>
    /// Gets or sets the number of servings.
    /// </summary>
    public int Servings { get; set; } = 4;

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateRecipeCommand.
/// </summary>
public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, RecipeDto>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRecipeCommandHandler"/> class.
    /// </summary>
    public CreateRecipeCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RecipeDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            MeatType = request.MeatType,
            CookingMethod = request.CookingMethod,
            PrepTimeMinutes = request.PrepTimeMinutes,
            CookTimeMinutes = request.CookTimeMinutes,
            Ingredients = request.Ingredients,
            Instructions = request.Instructions,
            TargetTemperature = request.TargetTemperature,
            Servings = request.Servings,
            Notes = request.Notes,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync(cancellationToken);

        return RecipeDto.FromEntity(recipe);
    }
}
