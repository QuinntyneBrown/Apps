// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;

namespace FreelanceProjectManager.Api.Features.Invoices;

/// <summary>
/// Command to create a new invoice.
/// </summary>
public class CreateInvoiceCommand : IRequest<InvoiceDto>
{
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
    /// Gets or sets notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for creating an invoice.
/// </summary>
public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInvoiceHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateInvoiceHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = new Invoice
        {
            InvoiceId = Guid.NewGuid(),
            UserId = request.UserId,
            ClientId = request.ClientId,
            ProjectId = request.ProjectId,
            InvoiceNumber = request.InvoiceNumber,
            InvoiceDate = request.InvoiceDate,
            DueDate = request.DueDate,
            TotalAmount = request.TotalAmount,
            Currency = request.Currency,
            Status = "Draft",
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Invoices.Add(invoice);
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
