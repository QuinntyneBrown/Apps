// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Stories;

public class GetStories
{
    public class Query : IRequest<List<StoryDto>>
    {
        public Guid? PersonId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<StoryDto>>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<List<StoryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Stories.AsQueryable();

            if (request.PersonId.HasValue)
            {
                query = query.Where(s => s.PersonId == request.PersonId.Value);
            }

            var stories = await query
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync(cancellationToken);

            return stories.Select(StoryDto.FromStory).ToList();
        }
    }
}
