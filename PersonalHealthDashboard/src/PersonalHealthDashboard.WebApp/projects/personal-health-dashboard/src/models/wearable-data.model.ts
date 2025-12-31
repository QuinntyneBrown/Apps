export interface WearableData {
  wearableDataId: string;
  userId: string;
  deviceName: string;
  dataType: string;
  value: number;
  unit: string;
  recordedAt: string;
  syncedAt: string;
  metadata: string | null;
  createdAt: string;
  dataAgeInHours: number;
}

export interface CreateWearableData {
  userId: string;
  deviceName: string;
  dataType: string;
  value: number;
  unit: string;
  recordedAt: string;
  syncedAt: string;
  metadata: string | null;
}

export interface UpdateWearableData {
  wearableDataId: string;
  deviceName: string;
  dataType: string;
  value: number;
  unit: string;
  recordedAt: string;
  syncedAt: string;
  metadata: string | null;
}
