import { GenreBreakdown } from './genre-breakdown';
import { MonthlyData } from './monthly-data';

export interface ViewingStats {
  moviesWatched: number;
  showsWatched: number;
  hoursWatched: number;
  averageRating: number;
  currentStreak: number;
  longestStreak: number;
  milestones: number;
  nextMilestone: string;
  genreBreakdown: GenreBreakdown[];
  monthlyData: MonthlyData[];
  moviesChange: number;
  showsChange: number;
  hoursChange: number;
}
