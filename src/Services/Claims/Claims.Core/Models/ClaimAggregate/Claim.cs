namespace Claims.Core.Models;

public enum ClaimStatus
{
    Draft,
    Submitted,
    UnderReview,
    Approved,
    Rejected,
    Paid
}

public class Claim
{
    public Guid ClaimId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal TotalAmount { get; private set; }
    public ClaimStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? SubmittedAt { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public Guid? ApprovedByUserId { get; private set; }

    private Claim() { }

    public Claim(Guid tenantId, Guid userId, string title, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        ClaimId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Title = title;
        Description = description;
        TotalAmount = 0;
        Status = ClaimStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string? title = null, string? description = null)
    {
        if (Status != ClaimStatus.Draft)
            throw new InvalidOperationException("Cannot update a claim that is not in draft status.");

        if (title != null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            Title = title;
        }

        Description = description ?? Description;
    }

    public void SetTotalAmount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Total amount cannot be negative.", nameof(amount));

        TotalAmount = amount;
    }

    public void Submit()
    {
        if (Status != ClaimStatus.Draft)
            throw new InvalidOperationException("Only draft claims can be submitted.");

        if (TotalAmount <= 0)
            throw new InvalidOperationException("Cannot submit a claim with no expenses.");

        Status = ClaimStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
    }

    public void Approve(Guid approvedByUserId)
    {
        if (Status != ClaimStatus.Submitted && Status != ClaimStatus.UnderReview)
            throw new InvalidOperationException("Only submitted or under review claims can be approved.");

        Status = ClaimStatus.Approved;
        ApprovedAt = DateTime.UtcNow;
        ApprovedByUserId = approvedByUserId;
    }

    public void Reject()
    {
        if (Status != ClaimStatus.Submitted && Status != ClaimStatus.UnderReview)
            throw new InvalidOperationException("Only submitted or under review claims can be rejected.");

        Status = ClaimStatus.Rejected;
    }

    public void MarkAsPaid()
    {
        if (Status != ClaimStatus.Approved)
            throw new InvalidOperationException("Only approved claims can be marked as paid.");

        Status = ClaimStatus.Paid;
    }
}
