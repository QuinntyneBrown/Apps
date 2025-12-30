// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.Techniques;

/// <summary>
/// Command to update a technique.
/// </summary>
public class UpdateTechniqueCommand : IRequest<TechniqueDto?>
{
    /// <summary>
    /// Gets or sets the technique ID.
    /// </summary>
    public Guid TechniqueId { get; set; }

    /// <summary>
    /// Gets or sets the technique name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the technique description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the difficulty level.
    /// </summary>
    public int DifficultyLevel { get; set; }

    /// <summary>
    /// Gets or sets the instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tips.
    /// </summary>
    public string? Tips { get; set; }
}

/// <summary>
/// Handler for UpdateTechniqueCommand.
/// </summary>
public class UpdateTechniqueCommandHandler : IRequestHandler<UpdateTechniqueCommand, TechniqueDto?>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTechniqueCommandHandler"/> class.
    /// </summary>
    public UpdateTechniqueCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TechniqueDto?> Handle(UpdateTechniqueCommand request, CancellationToken cancellationToken)
    {
        var technique = await _context.Techniques
            .FirstOrDefaultAsync(t => t.TechniqueId == request.TechniqueId, cancellationToken);

        if (technique == null)
        {
            return null;
        }

        technique.Name = request.Name;
        technique.Description = request.Description;
        technique.Category = request.Category;
        technique.DifficultyLevel = request.DifficultyLevel;
        technique.Instructions = request.Instructions;
        technique.Tips = request.Tips;

        await _context.SaveChangesAsync(cancellationToken);

        return TechniqueDto.FromEntity(technique);
    }
}
