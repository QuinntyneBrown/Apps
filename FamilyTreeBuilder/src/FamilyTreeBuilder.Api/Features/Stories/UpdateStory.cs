// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Stories;

public class UpdateStory
{
    public class Command : IRequest<StoryDto?>
    {
        public Guid StoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
    }

    public class Handler : IRequestHandler<Command, StoryDto?>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<StoryDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var story = await _context.Stories
                .FirstOrDefaultAsync(s => s.StoryId == request.StoryId, cancellationToken);

            if (story == null)
            {
                return null;
            }

            story.Title = request.Title;
            story.Content = request.Content;

            await _context.SaveChangesAsync(cancellationToken);

            return StoryDto.FromStory(story);
        }
    }
}
