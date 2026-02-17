export interface AddTVShowToWatchlistRequest {
  showId: string;
  title: string;
  premiereYear: number;
  genres?: string[];
  numberOfSeasons?: number;
  status?: string;
  priority?: string;
  streamingPlatform?: string;
}
