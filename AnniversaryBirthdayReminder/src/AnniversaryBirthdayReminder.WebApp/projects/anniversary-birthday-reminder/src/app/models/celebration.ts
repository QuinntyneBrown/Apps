import { CelebrationStatus } from './celebration-status';

export interface Celebration {
  celebrationId: string;
  dateId: string;
  celebrationDate: Date;
  notes: string;
  photos: string[];
  attendees: string[];
  rating: number;
  status: CelebrationStatus;
}
