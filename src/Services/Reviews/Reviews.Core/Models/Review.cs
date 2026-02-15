namespace Reviews.Core.Models;

public class Review
{
    public Guid ReviewId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public int Rating { get; private set; }
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Review() { }

    public Review(Guid tenantId, Guid userId, Guid bookId, int rating, string? title = null, string? content = null)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.", nameof(rating));

        ReviewId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        BookId = bookId;
        Rating = rating;
        Title = title;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(int? rating = null, string? title = null, string? content = null)
    {
        if (rating.HasValue)
        {
            if (rating.Value < 1 || rating.Value > 5)
                throw new ArgumentException("Rating must be between 1 and 5.", nameof(rating));
            Rating = rating.Value;
        }

        if (title != null)
            Title = title;

        if (content != null)
            Content = content;

        UpdatedAt = DateTime.UtcNow;
    }
}
