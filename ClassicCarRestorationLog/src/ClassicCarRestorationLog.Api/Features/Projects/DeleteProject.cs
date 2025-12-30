// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Projects;

public class DeleteProject
{
    public class Command : IRequest<bool>
    {
        public Guid ProjectId { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

            if (project == null)
            {
                return false;
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
