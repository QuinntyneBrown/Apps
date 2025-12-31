import { MoodLevel } from './mood-level';
import { StressLevel } from './stress-level';

export interface MoodEntry {
  moodEntryId: string;
  userId: string;
  moodLevel: MoodLevel;
  stressLevel: StressLevel;
  entryTime: Date;
  notes?: string;
  activities?: string;
  createdAt: Date;
}
