// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;

namespace BBQGrillingRecipeBook.Api.Features.Techniques;

/// <summary>
/// Command to create a new technique.
/// </summary>
public class CreateTechniqueCommand : IRequest<TechniqueDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

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
    public int DifficultyLevel { get; set; } = 1;

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
/// Handler for CreateTechniqueCommand.
/// </summary>
public class CreateTechniqueCommandHandler : IRequestHandler<CreateTechniqueCommand, TechniqueDto>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTechniqueCommandHandler"/> class.
    /// </summary>
    public CreateTechniqueCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TechniqueDto> Handle(CreateTechniqueCommand request, CancellationToken cancellationToken)
    {
        var technique = new Technique
        {
            TechniqueId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Category = request.Category,
            DifficultyLevel = request.DifficultyLevel,
            Instructions = request.Instructions,
            Tips = request.Tips,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Techniques.Add(technique);
        await _context.SaveChangesAsync(cancellationToken);

        return TechniqueDto.FromEntity(technique);
    }
}
