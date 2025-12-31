export enum MaintenanceType {
  OilChange = 0,
  TireReplacement = 1,
  ChainAdjustment = 2,
  BrakeService = 3,
  AirFilterChange = 4,
  SparkPlugReplacement = 5,
  BatteryReplacement = 6,
  CoolantChange = 7,
  Inspection = 8,
  Repair = 9,
  Other = 10,
}

export const MaintenanceTypeLabels: Record<MaintenanceType, string> = {
  [MaintenanceType.OilChange]: 'Oil Change',
  [MaintenanceType.TireReplacement]: 'Tire Replacement',
  [MaintenanceType.ChainAdjustment]: 'Chain Adjustment',
  [MaintenanceType.BrakeService]: 'Brake Service',
  [MaintenanceType.AirFilterChange]: 'Air Filter Change',
  [MaintenanceType.SparkPlugReplacement]: 'Spark Plug Replacement',
  [MaintenanceType.BatteryReplacement]: 'Battery Replacement',
  [MaintenanceType.CoolantChange]: 'Coolant Change',
  [MaintenanceType.Inspection]: 'Inspection',
  [MaintenanceType.Repair]: 'Repair',
  [MaintenanceType.Other]: 'Other',
};
