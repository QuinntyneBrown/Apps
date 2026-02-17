export interface MarkMovieWatchedRequest {
  watchDate: Date;
  viewingLocation?: string;
  viewingPlatform?: string;
  watchedWith?: string[];
  viewingContext?: string;
  isRewatch: boolean;
}
