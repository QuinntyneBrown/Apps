// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Tags;

/// <summary>
/// Command to create a new tag.
/// </summary>
public class CreateTagCommand : IRequest<TagDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tag name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Validator for CreateTagCommand.
/// </summary>
public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTagCommandValidator"/> class.
    /// </summary>
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}

/// <summary>
/// Handler for CreateTagCommand.
/// </summary>
public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagDto>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<CreateTagCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTagCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public CreateTagCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<CreateTagCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the CreateTagCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created tag DTO.</returns>
    public async Task<TagDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return new TagDto
        {
            TagId = tag.TagId,
            UserId = tag.UserId,
            Name = tag.Name,
            CreatedAt = tag.CreatedAt,
            PhotoCount = 0
        };
    }
}
