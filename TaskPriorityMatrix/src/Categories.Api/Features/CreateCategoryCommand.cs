using Categories.Core;
using Categories.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Categories.Api.Features;

public record CreateCategoryCommand(Guid TenantId, string Name, string Color) : IRequest<CategoryDto>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoriesDbContext _context;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateCategoryCommandHandler(
        ICategoriesDbContext context,
        ILogger<CreateCategoryCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            TenantId = request.TenantId,
            Name = request.Name,
            Color = request.Color,
            CreatedAt = DateTime.UtcNow
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        await PublishCategoryCreatedEventAsync(category);

        _logger.LogInformation("Category created: {CategoryId}", category.CategoryId);

        return category.ToDto();
    }

    private Task PublishCategoryCreatedEventAsync(Category category)
    {
        if (_rabbitConnection == null) return Task.CompletedTask;

        try
        {
            using var channel = _rabbitConnection.CreateModel();
            channel.ExchangeDeclare("categories-events", ExchangeType.Topic, durable: true);

            var @event = new CategoryCreatedEvent
            {
                TenantId = category.TenantId,
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish("categories-events", "category.created", null, body);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to publish CategoryCreatedEvent");
        }

        return Task.CompletedTask;
    }
}
