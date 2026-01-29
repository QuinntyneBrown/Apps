namespace Books.Core.Models;

public class Book
{
    public Guid BookId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public string? Isbn { get; private set; }
    public int TotalPages { get; private set; }
    public string? Genre { get; private set; }
    public DateTime? PublishedDate { get; private set; }
    public DateTime AddedAt { get; private set; }

    private Book() { }

    public Book(Guid tenantId, Guid userId, string title, string author, int totalPages, string? isbn = null, string? genre = null, DateTime? publishedDate = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        if (totalPages <= 0)
            throw new ArgumentException("Total pages must be positive.", nameof(totalPages));

        BookId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Title = title;
        Author = author;
        TotalPages = totalPages;
        Isbn = isbn;
        Genre = genre;
        PublishedDate = publishedDate;
        AddedAt = DateTime.UtcNow;
    }

    public void Update(string? title = null, string? author = null, int? totalPages = null, string? isbn = null, string? genre = null)
    {
        if (title != null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            Title = title;
        }

        if (author != null)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.", nameof(author));
            Author = author;
        }

        if (totalPages.HasValue)
        {
            if (totalPages.Value <= 0)
                throw new ArgumentException("Total pages must be positive.", nameof(totalPages));
            TotalPages = totalPages.Value;
        }

        if (isbn != null)
            Isbn = isbn;

        if (genre != null)
            Genre = genre;
    }
}
