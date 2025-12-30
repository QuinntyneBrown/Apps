// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeBuilder.Api.Features.Stories;

public class DeleteStory
{
    public class Command : IRequest<bool>
    {
        public Guid StoryId { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IFamilyTreeBuilderContext _context;

        public Handler(IFamilyTreeBuilderContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var story = await _context.Stories
                .FirstOrDefaultAsync(s => s.StoryId == request.StoryId, cancellationToken);

            if (story == null)
            {
                return false;
            }

            _context.Stories.Remove(story);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
