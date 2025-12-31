export enum MedicationType {
  Tablet = 0,
  Capsule = 1,
  Liquid = 2,
  Injection = 3,
  Topical = 4,
  Inhaler = 5,
  Patch = 6,
  Other = 7
}

export const MedicationTypeLabels: Record<MedicationType, string> = {
  [MedicationType.Tablet]: 'Tablet',
  [MedicationType.Capsule]: 'Capsule',
  [MedicationType.Liquid]: 'Liquid',
  [MedicationType.Injection]: 'Injection',
  [MedicationType.Topical]: 'Topical',
  [MedicationType.Inhaler]: 'Inhaler',
  [MedicationType.Patch]: 'Patch',
  [MedicationType.Other]: 'Other'
};
