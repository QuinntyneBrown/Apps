// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Core;

public class GiftIdea
{
    public Guid GiftIdeaId { get; set; }
    public Guid UserId { get; set; }
    public Guid? RecipientId { get; set; }
    public Recipient? Recipient { get; set; }
    public string Name { get; set; } = string.Empty;
    public Occasion Occasion { get; set; }
    public decimal? EstimatedPrice { get; set; }
    public bool IsPurchased { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
