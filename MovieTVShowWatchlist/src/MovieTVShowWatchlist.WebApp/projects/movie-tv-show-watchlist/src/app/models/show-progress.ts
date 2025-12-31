export interface ShowProgress {
  tvShowId: string;
  lastWatchedSeason?: number;
  lastWatchedEpisode?: number;
  totalEpisodesWatched: number;
  completedSeasons: number;
  isCompleted: boolean;
  completionDate?: Date;
}
