import { VitalType } from './vital-type.enum';

export interface Vital {
  vitalId: string;
  userId: string;
  vitalType: VitalType;
  value: number;
  unit: string;
  measuredAt: string;
  notes: string | null;
  source: string | null;
  createdAt: string;
}

export interface CreateVital {
  userId: string;
  vitalType: VitalType;
  value: number;
  unit: string;
  measuredAt: string;
  notes: string | null;
  source: string | null;
}

export interface UpdateVital {
  vitalId: string;
  vitalType: VitalType;
  value: number;
  unit: string;
  measuredAt: string;
  notes: string | null;
  source: string | null;
}
