using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiCategories;

public record DeleteWikiCategoryCommand : IRequest<bool>
{
    public Guid WikiCategoryId { get; init; }
}

public class DeleteWikiCategoryCommandHandler : IRequestHandler<DeleteWikiCategoryCommand, bool>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<DeleteWikiCategoryCommandHandler> _logger;

    public DeleteWikiCategoryCommandHandler(
        IPersonalWikiContext context,
        ILogger<DeleteWikiCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteWikiCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting wiki category {WikiCategoryId}", request.WikiCategoryId);

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.WikiCategoryId == request.WikiCategoryId, cancellationToken);

        if (category == null)
        {
            _logger.LogWarning("Wiki category {WikiCategoryId} not found", request.WikiCategoryId);
            return false;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted wiki category {WikiCategoryId}", request.WikiCategoryId);

        return true;
    }
}
