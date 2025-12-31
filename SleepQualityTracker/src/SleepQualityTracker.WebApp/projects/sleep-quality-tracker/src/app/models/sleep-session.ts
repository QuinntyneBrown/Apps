import { SleepQuality } from './sleep-quality';

export interface SleepSession {
  sleepSessionId: string;
  userId: string;
  bedtime: string;
  wakeTime: string;
  totalSleepMinutes: number;
  sleepQuality: SleepQuality;
  timesAwakened?: number;
  deepSleepMinutes?: number;
  remSleepMinutes?: number;
  sleepEfficiency?: number;
  notes?: string;
  createdAt: string;
  timeInBedMinutes: number;
  meetsRecommendedDuration: boolean;
  isGoodQuality: boolean;
}

export interface CreateSleepSessionRequest {
  userId: string;
  bedtime: string;
  wakeTime: string;
  totalSleepMinutes: number;
  sleepQuality: SleepQuality;
  timesAwakened?: number;
  deepSleepMinutes?: number;
  remSleepMinutes?: number;
  sleepEfficiency?: number;
  notes?: string;
}

export interface UpdateSleepSessionRequest {
  sleepSessionId: string;
  bedtime: string;
  wakeTime: string;
  totalSleepMinutes: number;
  sleepQuality: SleepQuality;
  timesAwakened?: number;
  deepSleepMinutes?: number;
  remSleepMinutes?: number;
  sleepEfficiency?: number;
  notes?: string;
}
