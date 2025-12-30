// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Api.Features.Wallets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoPortfolioManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WalletsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<WalletDto>>> GetWallets()
    {
        var wallets = await _mediator.Send(new GetWalletsQuery());
        return Ok(wallets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WalletDto>> GetWallet(Guid id)
    {
        var wallet = await _mediator.Send(new GetWalletByIdQuery { WalletId = id });

        if (wallet == null)
            return NotFound();

        return Ok(wallet);
    }

    [HttpPost]
    public async Task<ActionResult<WalletDto>> CreateWallet([FromBody] CreateWalletCommand command)
    {
        var wallet = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetWallet), new { id = wallet.WalletId }, wallet);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WalletDto>> UpdateWallet(Guid id, [FromBody] UpdateWalletCommand command)
    {
        command.WalletId = id;
        var wallet = await _mediator.Send(command);

        if (wallet == null)
            return NotFound();

        return Ok(wallet);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWallet(Guid id)
    {
        var result = await _mediator.Send(new DeleteWalletCommand { WalletId = id });

        if (!result)
            return NotFound();

        return NoContent();
    }
}
