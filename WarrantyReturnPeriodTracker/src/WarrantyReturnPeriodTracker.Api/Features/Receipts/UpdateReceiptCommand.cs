using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Receipts;

public class UpdateReceiptCommand : IRequest<ReceiptDto>
{
    public Guid ReceiptId { get; set; }
    public string ReceiptNumber { get; set; } = string.Empty;
    public ReceiptType ReceiptType { get; set; }
    public ReceiptFormat Format { get; set; }
    public string? StorageLocation { get; set; }
    public DateTime ReceiptDate { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public ReceiptStatus Status { get; set; }
    public bool IsVerified { get; set; }
    public string? Notes { get; set; }
}

public class UpdateReceiptCommandHandler : IRequestHandler<UpdateReceiptCommand, ReceiptDto>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public UpdateReceiptCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReceiptDto> Handle(UpdateReceiptCommand request, CancellationToken cancellationToken)
    {
        var receipt = await _context.Receipts
            .FirstOrDefaultAsync(r => r.ReceiptId == request.ReceiptId, cancellationToken);

        if (receipt == null)
        {
            throw new InvalidOperationException($"Receipt with ID {request.ReceiptId} not found.");
        }

        receipt.ReceiptNumber = request.ReceiptNumber;
        receipt.ReceiptType = request.ReceiptType;
        receipt.Format = request.Format;
        receipt.StorageLocation = request.StorageLocation;
        receipt.ReceiptDate = request.ReceiptDate;
        receipt.StoreName = request.StoreName;
        receipt.TotalAmount = request.TotalAmount;
        receipt.PaymentMethod = request.PaymentMethod;
        receipt.Status = request.Status;
        receipt.IsVerified = request.IsVerified;
        receipt.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return receipt.ToDto();
    }
}
