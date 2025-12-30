// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get modifications by category.
/// </summary>
public record GetModificationsByCategoryQuery : IRequest<IEnumerable<ModificationDto>>
{
    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string Category { get; init; } = string.Empty;
}

/// <summary>
/// Handler for GetModificationsByCategoryQuery.
/// </summary>
public class GetModificationsByCategoryQueryHandler : IRequestHandler<GetModificationsByCategoryQuery, IEnumerable<ModificationDto>>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetModificationsByCategoryQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationsByCategoryQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetModificationsByCategoryQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetModificationsByCategoryQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ModificationDto>> Handle(GetModificationsByCategoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting modifications by category {Category}", request.Category);

        // Try to parse the category string to the enum
        if (!Enum.TryParse<ModCategory>(request.Category, true, out var category))
        {
            _logger.LogWarning("Invalid category {Category}", request.Category);
            return Enumerable.Empty<ModificationDto>();
        }

        var modifications = await _context.Modifications
            .AsNoTracking()
            .Where(m => m.Category == category)
            .ToListAsync(cancellationToken);

        return modifications.Select(m => m.ToDto());
    }
}
