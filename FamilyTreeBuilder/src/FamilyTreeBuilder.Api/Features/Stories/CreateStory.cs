// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;

namespace FamilyTreeBuilder.Api.Features.Stories;

public class CreateStory
{
    public class Command : IRequest<StoryDto>
    {
        public Guid PersonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
    }

    public class Handler : IRequestHandler<Command, StoryDto>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<StoryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var story = new Story
            {
                StoryId = Guid.NewGuid(),
                PersonId = request.PersonId,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync(cancellationToken);

            return StoryDto.FromStory(story);
        }
    }
}
