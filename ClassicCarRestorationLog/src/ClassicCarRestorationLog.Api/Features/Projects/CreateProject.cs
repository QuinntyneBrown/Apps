// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Projects;

public class CreateProject
{
    public class Command : IRequest<ProjectDto>
    {
        public Guid UserId { get; set; }
        public string CarMake { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public int? Year { get; set; }
        public ProjectPhase Phase { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? EstimatedBudget { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, ProjectDto>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<ProjectDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                ProjectId = Guid.NewGuid(),
                UserId = request.UserId,
                CarMake = request.CarMake,
                CarModel = request.CarModel,
                Year = request.Year,
                Phase = request.Phase,
                StartDate = request.StartDate ?? DateTime.UtcNow,
                EstimatedBudget = request.EstimatedBudget,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync(cancellationToken);

            return ProjectDto.FromEntity(project);
        }
    }
}
