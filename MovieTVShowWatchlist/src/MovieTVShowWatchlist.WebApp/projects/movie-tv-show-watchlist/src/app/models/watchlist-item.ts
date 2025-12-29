import { ContentType } from './content-type';
import { Priority } from './priority';
import { Genre } from './genre';
import { Mood } from './mood';

export interface WatchlistItem {
  watchlistItemId: string;
  title: string;
  contentType: ContentType;
  releaseYear: number;
  genres: Genre[];
  priority: Priority;
  platform: string;
  runtime?: number;
  seasons?: number;
  addedDate: Date;
  mood?: Mood;
  posterUrl?: string;
}
