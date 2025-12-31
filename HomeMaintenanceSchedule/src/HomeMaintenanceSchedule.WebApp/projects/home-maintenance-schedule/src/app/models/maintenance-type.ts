export enum MaintenanceType {
  Preventive = 0,
  Corrective = 1,
  Seasonal = 2,
  Emergency = 3,
  Inspection = 4
}

export const MAINTENANCE_TYPE_LABELS: Record<MaintenanceType, string> = {
  [MaintenanceType.Preventive]: 'Preventive',
  [MaintenanceType.Corrective]: 'Corrective',
  [MaintenanceType.Seasonal]: 'Seasonal',
  [MaintenanceType.Emergency]: 'Emergency',
  [MaintenanceType.Inspection]: 'Inspection'
};
