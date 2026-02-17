export enum InjuryType {
  Strain = 0,
  Sprain = 1,
  Fracture = 2,
  Tendonitis = 3,
  CartilageDamage = 4,
  Overuse = 5,
  Other = 6
}

export const INJURY_TYPE_LABELS: Record<InjuryType, string> = {
  [InjuryType.Strain]: 'Strain',
  [InjuryType.Sprain]: 'Sprain',
  [InjuryType.Fracture]: 'Fracture',
  [InjuryType.Tendonitis]: 'Tendonitis',
  [InjuryType.CartilageDamage]: 'Cartilage Damage',
  [InjuryType.Overuse]: 'Overuse',
  [InjuryType.Other]: 'Other'
};
