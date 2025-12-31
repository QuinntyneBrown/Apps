export interface Run {
  runId: string;
  userId: string;
  distance: number;
  durationMinutes: number;
  completedAt: string;
  averagePace: number | null;
  averageHeartRate: number | null;
  elevationGain: number | null;
  caloriesBurned: number | null;
  route: string | null;
  weather: string | null;
  notes: string | null;
  effortRating: number | null;
  createdAt: string;
}

export interface CreateRunRequest {
  userId: string;
  distance: number;
  durationMinutes: number;
  completedAt: string;
  averagePace?: number;
  averageHeartRate?: number;
  elevationGain?: number;
  caloriesBurned?: number;
  route?: string;
  weather?: string;
  notes?: string;
  effortRating?: number;
}

export interface UpdateRunRequest {
  distance: number;
  durationMinutes: number;
  completedAt: string;
  averagePace?: number;
  averageHeartRate?: number;
  elevationGain?: number;
  caloriesBurned?: number;
  route?: string;
  weather?: string;
  notes?: string;
  effortRating?: number;
}
