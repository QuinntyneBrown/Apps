export enum ModCategory {
  Engine = 0,
  Exhaust = 1,
  Suspension = 2,
  Brakes = 3,
  WheelsAndTires = 4,
  Exterior = 5,
  Interior = 6,
  AudioAndElectronics = 7,
  Lighting = 8,
  Aerodynamics = 9,
  ForcedInduction = 10,
  Other = 11
}

export const MOD_CATEGORY_LABELS: Record<ModCategory, string> = {
  [ModCategory.Engine]: 'Engine',
  [ModCategory.Exhaust]: 'Exhaust',
  [ModCategory.Suspension]: 'Suspension',
  [ModCategory.Brakes]: 'Brakes',
  [ModCategory.WheelsAndTires]: 'Wheels & Tires',
  [ModCategory.Exterior]: 'Exterior',
  [ModCategory.Interior]: 'Interior',
  [ModCategory.AudioAndElectronics]: 'Audio & Electronics',
  [ModCategory.Lighting]: 'Lighting',
  [ModCategory.Aerodynamics]: 'Aerodynamics',
  [ModCategory.ForcedInduction]: 'Forced Induction',
  [ModCategory.Other]: 'Other'
};
