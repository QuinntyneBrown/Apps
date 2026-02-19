import { BloodPressureCategory } from './blood-pressure-category.enum';

export interface Reading {
  readingId: string;
  userId: string;
  systolic: number;
  diastolic: number;
  pulse?: number;
  category: BloodPressureCategory;
  measuredAt: string;
  position?: string;
  arm?: string;
  notes?: string;
  createdAt: string;
  isCritical: boolean;
}

export interface CreateReadingRequest {
  userId: string;
  systolic: number;
  diastolic: number;
  pulse?: number;
  measuredAt?: string;
  position?: string;
  arm?: string;
  notes?: string;
}

export interface UpdateReadingRequest {
  readingId: string;
  systolic: number;
  diastolic: number;
  pulse?: number;
  measuredAt: string;
  position?: string;
  arm?: string;
  notes?: string;
}
