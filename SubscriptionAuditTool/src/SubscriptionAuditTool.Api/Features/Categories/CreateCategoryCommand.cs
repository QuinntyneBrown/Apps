using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Categories;

public record CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? ColorCode { get; init; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;

    public CreateCategoryCommandHandler(
        ISubscriptionAuditToolContext context,
        ILogger<CreateCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating category: {CategoryName}",
            request.Name);

        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            ColorCode = request.ColorCode,
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created category {CategoryId} with name {CategoryName}",
            category.CategoryId,
            request.Name);

        return category.ToDto();
    }
}
