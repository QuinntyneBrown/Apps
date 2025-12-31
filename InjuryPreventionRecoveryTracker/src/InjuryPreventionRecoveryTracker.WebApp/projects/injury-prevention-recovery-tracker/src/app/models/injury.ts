import { InjuryType } from './injury-type';
import { InjurySeverity } from './injury-severity';

export interface Injury {
  injuryId: string;
  userId: string;
  injuryType: InjuryType;
  severity: InjurySeverity;
  bodyPart: string;
  injuryDate: string;
  description?: string;
  diagnosis?: string;
  expectedRecoveryDays?: number;
  status: string;
  progressPercentage: number;
  notes?: string;
  createdAt: string;
}

export interface CreateInjury {
  userId: string;
  injuryType: InjuryType;
  severity: InjurySeverity;
  bodyPart: string;
  injuryDate: string;
  description?: string;
  diagnosis?: string;
  expectedRecoveryDays?: number;
  notes?: string;
}

export interface UpdateInjury {
  injuryId: string;
  injuryType: InjuryType;
  severity: InjurySeverity;
  bodyPart: string;
  injuryDate: string;
  description?: string;
  diagnosis?: string;
  expectedRecoveryDays?: number;
  status: string;
  progressPercentage: number;
  notes?: string;
}
