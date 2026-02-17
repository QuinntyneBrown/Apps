export interface MarkEpisodeWatchedRequest {
  watchDate: Date;
  platform?: string;
  bingeSessionId?: string;
  viewingDurationMinutes?: number;
}
