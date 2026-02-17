export interface AddMovieToWatchlistRequest {
  movieId: string;
  title: string;
  releaseYear: number;
  genres?: string[];
  director?: string;
  runtime?: number;
  priorityLevel?: string;
  recommendationSource?: string;
  availability?: string;
}
