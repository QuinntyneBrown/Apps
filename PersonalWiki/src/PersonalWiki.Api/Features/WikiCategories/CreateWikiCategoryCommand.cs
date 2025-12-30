using PersonalWiki.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiCategories;

public record CreateWikiCategoryCommand : IRequest<WikiCategoryDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid? ParentCategoryId { get; init; }
    public string? Icon { get; init; }
}

public class CreateWikiCategoryCommandHandler : IRequestHandler<CreateWikiCategoryCommand, WikiCategoryDto>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<CreateWikiCategoryCommandHandler> _logger;

    public CreateWikiCategoryCommandHandler(
        IPersonalWikiContext context,
        ILogger<CreateWikiCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WikiCategoryDto> Handle(CreateWikiCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating wiki category for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var category = new WikiCategory
        {
            WikiCategoryId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId,
            Icon = request.Icon,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created wiki category {WikiCategoryId} for user {UserId}",
            category.WikiCategoryId,
            request.UserId);

        return category.ToDto();
    }
}
