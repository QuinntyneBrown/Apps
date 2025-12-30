using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiCategories;

public record UpdateWikiCategoryCommand : IRequest<WikiCategoryDto?>
{
    public Guid WikiCategoryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid? ParentCategoryId { get; init; }
    public string? Icon { get; init; }
}

public class UpdateWikiCategoryCommandHandler : IRequestHandler<UpdateWikiCategoryCommand, WikiCategoryDto?>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<UpdateWikiCategoryCommandHandler> _logger;

    public UpdateWikiCategoryCommandHandler(
        IPersonalWikiContext context,
        ILogger<UpdateWikiCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WikiCategoryDto?> Handle(UpdateWikiCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating wiki category {WikiCategoryId}", request.WikiCategoryId);

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.WikiCategoryId == request.WikiCategoryId, cancellationToken);

        if (category == null)
        {
            _logger.LogWarning("Wiki category {WikiCategoryId} not found", request.WikiCategoryId);
            return null;
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.ParentCategoryId = request.ParentCategoryId;
        category.Icon = request.Icon;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated wiki category {WikiCategoryId}", request.WikiCategoryId);

        return category.ToDto();
    }
}
