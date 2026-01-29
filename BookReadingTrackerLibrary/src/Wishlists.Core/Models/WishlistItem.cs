namespace Wishlists.Core.Models;

public class WishlistItem
{
    public Guid WishlistItemId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string BookTitle { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public string? Isbn { get; private set; }
    public int Priority { get; private set; }
    public string? Notes { get; private set; }
    public DateTime AddedAt { get; private set; }

    private WishlistItem() { }

    public WishlistItem(Guid tenantId, Guid userId, string bookTitle, string author, int priority = 1, string? isbn = null, string? notes = null)
    {
        if (string.IsNullOrWhiteSpace(bookTitle))
            throw new ArgumentException("Book title cannot be empty.", nameof(bookTitle));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        if (priority < 1)
            throw new ArgumentException("Priority must be at least 1.", nameof(priority));

        WishlistItemId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        BookTitle = bookTitle;
        Author = author;
        Priority = priority;
        Isbn = isbn;
        Notes = notes;
        AddedAt = DateTime.UtcNow;
    }

    public void Update(string? bookTitle = null, string? author = null, int? priority = null, string? isbn = null, string? notes = null)
    {
        if (bookTitle != null)
        {
            if (string.IsNullOrWhiteSpace(bookTitle))
                throw new ArgumentException("Book title cannot be empty.", nameof(bookTitle));
            BookTitle = bookTitle;
        }

        if (author != null)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.", nameof(author));
            Author = author;
        }

        if (priority.HasValue)
        {
            if (priority.Value < 1)
                throw new ArgumentException("Priority must be at least 1.", nameof(priority));
            Priority = priority.Value;
        }

        if (isbn != null)
            Isbn = isbn;

        if (notes != null)
            Notes = notes;
    }
}
