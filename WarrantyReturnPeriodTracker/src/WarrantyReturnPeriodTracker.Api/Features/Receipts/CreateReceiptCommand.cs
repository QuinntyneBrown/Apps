using MediatR;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Receipts;

public class CreateReceiptCommand : IRequest<ReceiptDto>
{
    public Guid PurchaseId { get; set; }
    public string ReceiptNumber { get; set; } = string.Empty;
    public ReceiptType ReceiptType { get; set; }
    public ReceiptFormat Format { get; set; }
    public string? StorageLocation { get; set; }
    public DateTime ReceiptDate { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? Notes { get; set; }
}

public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, ReceiptDto>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public CreateReceiptCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReceiptDto> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
    {
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = request.PurchaseId,
            ReceiptNumber = request.ReceiptNumber,
            ReceiptType = request.ReceiptType,
            Format = request.Format,
            StorageLocation = request.StorageLocation,
            ReceiptDate = request.ReceiptDate,
            StoreName = request.StoreName,
            TotalAmount = request.TotalAmount,
            PaymentMethod = request.PaymentMethod,
            Status = ReceiptStatus.Active,
            IsVerified = false,
            Notes = request.Notes,
            UploadedAt = DateTime.UtcNow
        };

        _context.Receipts.Add(receipt);
        await _context.SaveChangesAsync(cancellationToken);

        return receipt.ToDto();
    }
}
