// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Invoices;

/// <summary>
/// Command to delete an invoice.
/// </summary>
public class DeleteInvoiceCommand : IRequest<bool>
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
/// Handler for deleting an invoice.
/// </summary>
public class DeleteInvoiceHandler : IRequestHandler<DeleteInvoiceCommand, bool>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteInvoiceHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteInvoiceHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _context.Invoices
            .Where(i => i.InvoiceId == request.InvoiceId && i.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (invoice == null)
        {
            return false;
        }

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
