// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core;

public class Album
{
    public Guid AlbumId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid? ArtistId { get; set; }
    public Artist? Artist { get; set; }
    public Format Format { get; set; }
    public Genre Genre { get; set; }
    public int? ReleaseYear { get; set; }
    public string? Label { get; set; }
    public decimal? PurchasePrice { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<ListeningLog> ListeningLogs { get; set; } = new List<ListeningLog>();
}
