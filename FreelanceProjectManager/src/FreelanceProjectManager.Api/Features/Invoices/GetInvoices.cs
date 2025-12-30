// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Invoices;

/// <summary>
/// Query to get all invoices for a user.
/// </summary>
public class GetInvoicesQuery : IRequest<List<InvoiceDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for getting invoices.
/// </summary>
public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, List<InvoiceDto>>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInvoicesHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetInvoicesHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Invoices
            .Where(i => i.UserId == request.UserId)
            .Select(i => new InvoiceDto
            {
                InvoiceId = i.InvoiceId,
                UserId = i.UserId,
                ClientId = i.ClientId,
                ProjectId = i.ProjectId,
                InvoiceNumber = i.InvoiceNumber,
                InvoiceDate = i.InvoiceDate,
                DueDate = i.DueDate,
                TotalAmount = i.TotalAmount,
                Currency = i.Currency,
                Status = i.Status,
                PaidDate = i.PaidDate,
                Notes = i.Notes,
                CreatedAt = i.CreatedAt,
                UpdatedAt = i.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
