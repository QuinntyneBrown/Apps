import { ScreeningType } from './screening-type';

export interface Screening {
  screeningId: string;
  userId: string;
  screeningType: ScreeningType;
  name: string;
  recommendedFrequencyMonths: number;
  lastScreeningDate?: string;
  nextDueDate?: string;
  provider?: string;
  notes?: string;
  createdAt: string;
  isDueSoon: boolean;
}
