// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;

namespace FamilyPhotoAlbumOrganizer.Api.Features.PersonTags;

/// <summary>
/// Command to create a new person tag.
/// </summary>
public class CreatePersonTagCommand : IRequest<PersonTagDto>
{
    /// <summary>
    /// Gets or sets the photo ID.
    /// </summary>
    public Guid PhotoId { get; set; }

    /// <summary>
    /// Gets or sets the person name.
    /// </summary>
    public string PersonName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public int? CoordinateX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public int? CoordinateY { get; set; }
}

/// <summary>
/// Validator for CreatePersonTagCommand.
/// </summary>
public class CreatePersonTagCommandValidator : AbstractValidator<CreatePersonTagCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePersonTagCommandValidator"/> class.
    /// </summary>
    public CreatePersonTagCommandValidator()
    {
        RuleFor(x => x.PhotoId).NotEmpty();
        RuleFor(x => x.PersonName).NotEmpty().MaximumLength(200);
    }
}

/// <summary>
/// Handler for CreatePersonTagCommand.
/// </summary>
public class CreatePersonTagCommandHandler : IRequestHandler<CreatePersonTagCommand, PersonTagDto>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<CreatePersonTagCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePersonTagCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public CreatePersonTagCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<CreatePersonTagCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the CreatePersonTagCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created person tag DTO.</returns>
    public async Task<PersonTagDto> Handle(CreatePersonTagCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var personTag = new PersonTag
        {
            PersonTagId = Guid.NewGuid(),
            PhotoId = request.PhotoId,
            PersonName = request.PersonName,
            CoordinateX = request.CoordinateX,
            CoordinateY = request.CoordinateY,
            CreatedAt = DateTime.UtcNow
        };

        _context.PersonTags.Add(personTag);
        await _context.SaveChangesAsync(cancellationToken);

        return new PersonTagDto
        {
            PersonTagId = personTag.PersonTagId,
            PhotoId = personTag.PhotoId,
            PersonName = personTag.PersonName,
            CoordinateX = personTag.CoordinateX,
            CoordinateY = personTag.CoordinateY,
            CreatedAt = personTag.CreatedAt
        };
    }
}
