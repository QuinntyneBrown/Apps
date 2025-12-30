// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;

namespace FamilyTreeBuilder.Api.Features.FamilyPhotos;

public class FamilyPhotoDto
{
    public Guid FamilyPhotoId { get; set; }
    public Guid PersonId { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Caption { get; set; }
    public DateTime? DateTaken { get; set; }
    public DateTime CreatedAt { get; set; }

    public static FamilyPhotoDto FromFamilyPhoto(FamilyPhoto photo)
    {
        return new FamilyPhotoDto
        {
            FamilyPhotoId = photo.FamilyPhotoId,
            PersonId = photo.PersonId,
            PhotoUrl = photo.PhotoUrl,
            Caption = photo.Caption,
            DateTaken = photo.DateTaken,
            CreatedAt = photo.CreatedAt
        };
    }
}
