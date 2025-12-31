export enum MotorcycleType {
  Sport = 0,
  Cruiser = 1,
  Touring = 2,
  Naked = 3,
  Adventure = 4,
  DualSport = 5,
  OffRoad = 6,
  Scooter = 7,
  Standard = 8,
}

export const MotorcycleTypeLabels: Record<MotorcycleType, string> = {
  [MotorcycleType.Sport]: 'Sport',
  [MotorcycleType.Cruiser]: 'Cruiser',
  [MotorcycleType.Touring]: 'Touring',
  [MotorcycleType.Naked]: 'Naked',
  [MotorcycleType.Adventure]: 'Adventure',
  [MotorcycleType.DualSport]: 'Dual Sport',
  [MotorcycleType.OffRoad]: 'Off-Road',
  [MotorcycleType.Scooter]: 'Scooter',
  [MotorcycleType.Standard]: 'Standard',
};
