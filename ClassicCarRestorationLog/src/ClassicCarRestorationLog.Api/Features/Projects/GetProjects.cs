// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Projects;

public class GetProjects
{
    public class Query : IRequest<List<ProjectDto>>
    {
        public Guid? UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ProjectDto>>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Projects.AsQueryable();

            if (request.UserId.HasValue)
            {
                query = query.Where(p => p.UserId == request.UserId.Value);
            }

            var projects = await query
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);

            return projects.Select(ProjectDto.FromEntity).ToList();
        }
    }
}
