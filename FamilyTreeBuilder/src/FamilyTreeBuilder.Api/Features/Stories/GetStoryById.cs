// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Stories;

public class GetStoryById
{
    public class Query : IRequest<StoryDto?>
    {
        public Guid StoryId { get; set; }
    }

    public class Handler : IRequestHandler<Query, StoryDto?>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<StoryDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var story = await _context.Stories
                .FirstOrDefaultAsync(s => s.StoryId == request.StoryId, cancellationToken);

            return story == null ? null : StoryDto.FromStory(story);
        }
    }
}
