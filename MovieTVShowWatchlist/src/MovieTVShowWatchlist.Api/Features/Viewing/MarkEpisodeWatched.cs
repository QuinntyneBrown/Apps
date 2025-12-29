using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public class MarkEpisodeWatched
{
    public record Command(
        Guid UserId,
        Guid EpisodeId,
        DateTime WatchDate,
        string? Platform,
        Guid? BingeSessionId,
        int? ViewingDurationMinutes
    ) : IRequest<EpisodeViewingRecordDto>;

    public class Handler : IRequestHandler<Command, EpisodeViewingRecordDto>
    {
        private readonly IMovieTVShowWatchlistContext _context;
        private readonly ILogger<Handler> _logger;

        public Handler(IMovieTVShowWatchlistContext context, ILogger<Handler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<EpisodeViewingRecordDto> Handle(Command request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Marking episode {EpisodeId} as watched for user {UserId}", request.EpisodeId, request.UserId);

            var episode = await _context.Episodes
                .FirstOrDefaultAsync(e => e.EpisodeId == request.EpisodeId, cancellationToken);

            if (episode == null)
            {
                throw new InvalidOperationException($"Episode {request.EpisodeId} not found");
            }

            var record = new EpisodeViewingRecord
            {
                EpisodeViewingRecordId = Guid.NewGuid(),
                UserId = request.UserId,
                EpisodeId = request.EpisodeId,
                TVShowId = episode.TVShowId,
                SeasonNumber = episode.SeasonNumber,
                EpisodeNumber = episode.EpisodeNumber,
                WatchDate = request.WatchDate,
                Platform = request.Platform,
                BingeSessionId = request.BingeSessionId,
                ViewingDurationMinutes = request.ViewingDurationMinutes,
                CreatedAt = DateTime.UtcNow
            };

            _context.EpisodeViewingRecords.Add(record);

            var progress = await _context.ShowProgresses
                .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.TVShowId == episode.TVShowId, cancellationToken);

            if (progress == null)
            {
                progress = new ShowProgress
                {
                    ShowProgressId = Guid.NewGuid(),
                    UserId = request.UserId,
                    TVShowId = episode.TVShowId,
                    TotalEpisodesWatched = 0,
                    CompletedSeasons = 0,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.ShowProgresses.Add(progress);
            }

            progress.LastWatchedSeason = episode.SeasonNumber;
            progress.LastWatchedEpisode = episode.EpisodeNumber;
            progress.TotalEpisodesWatched++;
            progress.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Episode {EpisodeId} marked as watched with record ID {RecordId}", request.EpisodeId, record.EpisodeViewingRecordId);

            return new EpisodeViewingRecordDto(
                record.EpisodeViewingRecordId,
                record.EpisodeId,
                record.TVShowId,
                record.SeasonNumber,
                record.EpisodeNumber,
                record.WatchDate,
                record.Platform
            );
        }
    }
}

public record EpisodeViewingRecordDto(
    Guid EpisodeViewingRecordId,
    Guid EpisodeId,
    Guid TVShowId,
    int SeasonNumber,
    int EpisodeNumber,
    DateTime WatchDate,
    string? Platform
);
