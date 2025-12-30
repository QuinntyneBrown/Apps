// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Projects;

public class GetProjectById
{
    public class Query : IRequest<ProjectDto?>
    {
        public Guid ProjectId { get; set; }
    }

    public class Handler : IRequestHandler<Query, ProjectDto?>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<ProjectDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

            return project == null ? null : ProjectDto.FromEntity(project);
        }
    }
}
