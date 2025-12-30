// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Invoices;

/// <summary>
/// Command to update an invoice.
/// </summary>
public class UpdateInvoiceCommand : IRequest<InvoiceDto?>
{
    /// <summary>
    /// Gets or sets the invoice ID.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the invoice number.
    /// </summary>
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the invoice date.
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets the total amount.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public string Status { get; set; } = "Draft";

    /// <summary>
    /// Gets or sets notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for updating an invoice.
/// </summary>
public class UpdateInvoiceHandler : IRequestHandler<UpdateInvoiceCommand, InvoiceDto?>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateInvoiceHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateInvoiceHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<InvoiceDto?> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _context.Invoices
            .Where(i => i.InvoiceId == request.InvoiceId && i.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (invoice == null)
        {
            return null;
        }

        invoice.ClientId = request.ClientId;
        invoice.ProjectId = request.ProjectId;
        invoice.InvoiceNumber = request.InvoiceNumber;
        invoice.InvoiceDate = request.InvoiceDate;
        invoice.DueDate = request.DueDate;
        invoice.TotalAmount = request.TotalAmount;
        invoice.Currency = request.Currency;
        invoice.Status = request.Status;
        invoice.Notes = request.Notes;
        invoice.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new InvoiceDto
        {
            InvoiceId = invoice.InvoiceId,
            UserId = invoice.UserId,
            ClientId = invoice.ClientId,
            ProjectId = invoice.ProjectId,
            InvoiceNumber = invoice.InvoiceNumber,
            InvoiceDate = invoice.InvoiceDate,
            DueDate = invoice.DueDate,
            TotalAmount = invoice.TotalAmount,
            Currency = invoice.Currency,
            Status = invoice.Status,
            PaidDate = invoice.PaidDate,
            Notes = invoice.Notes,
            CreatedAt = invoice.CreatedAt,
            UpdatedAt = invoice.UpdatedAt
        };
    }
}
