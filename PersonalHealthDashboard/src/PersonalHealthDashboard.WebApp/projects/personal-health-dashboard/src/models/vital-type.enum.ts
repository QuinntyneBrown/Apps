export enum VitalType {
  BloodPressure = 0,
  HeartRate = 1,
  Temperature = 2,
  BloodGlucose = 3,
  OxygenSaturation = 4,
  Weight = 5,
  RespiratoryRate = 6
}

export const VitalTypeLabels: Record<VitalType, string> = {
  [VitalType.BloodPressure]: 'Blood Pressure',
  [VitalType.HeartRate]: 'Heart Rate',
  [VitalType.Temperature]: 'Temperature',
  [VitalType.BloodGlucose]: 'Blood Glucose',
  [VitalType.OxygenSaturation]: 'Oxygen Saturation',
  [VitalType.Weight]: 'Weight',
  [VitalType.RespiratoryRate]: 'Respiratory Rate'
};
