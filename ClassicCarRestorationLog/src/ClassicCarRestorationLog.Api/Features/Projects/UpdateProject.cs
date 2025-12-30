// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Projects;

public class UpdateProject
{
    public class Command : IRequest<ProjectDto?>
    {
        public Guid ProjectId { get; set; }
        public string CarMake { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public int? Year { get; set; }
        public ProjectPhase Phase { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal? EstimatedBudget { get; set; }
        public decimal? ActualCost { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, ProjectDto?>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<ProjectDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

            if (project == null)
            {
                return null;
            }

            project.CarMake = request.CarMake;
            project.CarModel = request.CarModel;
            project.Year = request.Year;
            project.Phase = request.Phase;
            project.StartDate = request.StartDate;
            project.CompletionDate = request.CompletionDate;
            project.EstimatedBudget = request.EstimatedBudget;
            project.ActualCost = request.ActualCost;
            project.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return ProjectDto.FromEntity(project);
        }
    }
}
