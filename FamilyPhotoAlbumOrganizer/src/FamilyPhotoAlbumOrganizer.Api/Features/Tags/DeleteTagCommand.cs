// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Tags;

/// <summary>
/// Command to delete a tag.
/// </summary>
public class DeleteTagCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the tag ID.
    /// </summary>
    public Guid TagId { get; set; }
}

/// <summary>
/// Validator for DeleteTagCommand.
/// </summary>
public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTagCommandValidator"/> class.
    /// </summary>
    public DeleteTagCommandValidator()
    {
        RuleFor(x => x.TagId).NotEmpty();
    }
}

/// <summary>
/// Handler for DeleteTagCommand.
/// </summary>
public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Unit>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<DeleteTagCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTagCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public DeleteTagCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<DeleteTagCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the DeleteTagCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Unit.</returns>
    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.TagId == request.TagId, cancellationToken);

        if (tag == null)
        {
            throw new KeyNotFoundException($"Tag with ID {request.TagId} not found.");
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
