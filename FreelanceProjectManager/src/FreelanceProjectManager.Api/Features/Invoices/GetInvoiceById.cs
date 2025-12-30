// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Invoices;

/// <summary>
/// Query to get an invoice by ID.
/// </summary>
public class GetInvoiceByIdQuery : IRequest<InvoiceDto?>
{
    /// <summary>
    /// Gets or sets the invoice ID.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for getting an invoice by ID.
/// </summary>
public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto?>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInvoiceByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetInvoiceByIdHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<InvoiceDto?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _context.Invoices
            .Where(i => i.InvoiceId == request.InvoiceId && i.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (invoice == null)
        {
            return null;
        }

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
