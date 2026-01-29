using AdminTasks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdminTasks.Api.Features.AdminTasks;

public record GetAdminTasksQuery : IRequest<IEnumerable<AdminTaskDto>>
{
    public Guid? UserId { get; init; }
}

public class GetAdminTasksQueryHandler : IRequestHandler<GetAdminTasksQuery, IEnumerable<AdminTaskDto>>
{
    private readonly IAdminTasksDbContext _context;

    public GetAdminTasksQueryHandler(IAdminTasksDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AdminTaskDto>> Handle(GetAdminTasksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.AdminTasks.AsNoTracking();

        if (request.UserId.HasValue)
            query = query.Where(t => t.UserId == request.UserId.Value);

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new AdminTaskDto
            {
                AdminTaskId = t.AdminTaskId,
                UserId = t.UserId,
                Title = t.Title,
                Description = t.Description,
                Category = t.Category,
                Priority = t.Priority,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted,
                CompletionDate = t.CompletionDate,
                IsRecurring = t.IsRecurring,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
