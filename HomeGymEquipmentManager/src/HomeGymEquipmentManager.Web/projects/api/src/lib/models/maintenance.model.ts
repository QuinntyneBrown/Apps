export interface Maintenance {
  maintenanceId?: string;
  userId: string;
  equipmentId: string;
  maintenanceDate: string;
  description: string;
  cost?: number;
  nextMaintenanceDate?: string;
  notes?: string;
  createdAt?: string;
}
