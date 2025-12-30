using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Purchases;

public class UpdatePurchaseCommand : IRequest<PurchaseDto>
{
    public Guid PurchaseId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public PurchaseStatus Status { get; set; }
    public string? ModelNumber { get; set; }
    public string? Notes { get; set; }
}

public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand, PurchaseDto>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public UpdatePurchaseCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<PurchaseDto> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _context.Purchases
            .FirstOrDefaultAsync(p => p.PurchaseId == request.PurchaseId, cancellationToken);

        if (purchase == null)
        {
            throw new InvalidOperationException($"Purchase with ID {request.PurchaseId} not found.");
        }

        purchase.ProductName = request.ProductName;
        purchase.Category = request.Category;
        purchase.StoreName = request.StoreName;
        purchase.PurchaseDate = request.PurchaseDate;
        purchase.Price = request.Price;
        purchase.Status = request.Status;
        purchase.ModelNumber = request.ModelNumber;
        purchase.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return purchase.ToDto();
    }
}
