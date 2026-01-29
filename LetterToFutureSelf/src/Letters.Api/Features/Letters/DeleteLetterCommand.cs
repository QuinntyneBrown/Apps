using Letters.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Letters.Api.Features.Letters;

public record DeleteLetterCommand : IRequest<bool>
{
    public Guid LetterId { get; init; }
}

public class DeleteLetterCommandHandler : IRequestHandler<DeleteLetterCommand, bool>
{
    private readonly ILettersDbContext _context;
    private readonly ILogger<DeleteLetterCommandHandler> _logger;

    public DeleteLetterCommandHandler(ILettersDbContext context, ILogger<DeleteLetterCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteLetterCommand request, CancellationToken cancellationToken)
    {
        var letter = await _context.Letters
            .FirstOrDefaultAsync(l => l.LetterId == request.LetterId, cancellationToken);

        if (letter == null) return false;

        _context.Letters.Remove(letter);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Letter deleted: {LetterId}", request.LetterId);

        return true;
    }
}
