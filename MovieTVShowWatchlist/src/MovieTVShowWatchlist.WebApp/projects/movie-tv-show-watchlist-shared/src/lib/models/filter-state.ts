import { ContentType } from './content-type';
import { Genre } from './genre';
import { Mood } from './mood';

export interface FilterState {
  contentTypes: ContentType[];
  genres: Genre[];
  moods: Mood[];
  availableNow: boolean;
  comingSoon: boolean;
  unavailable: boolean;
}
