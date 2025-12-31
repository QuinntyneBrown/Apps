export enum RideType {
  Commute = 0,
  Leisure = 1,
  Touring = 2,
  Track = 3,
  OffRoad = 4,
  GroupRide = 5,
  Other = 6,
}

export const RideTypeLabels: Record<RideType, string> = {
  [RideType.Commute]: 'Commute',
  [RideType.Leisure]: 'Leisure',
  [RideType.Touring]: 'Touring',
  [RideType.Track]: 'Track',
  [RideType.OffRoad]: 'Off-Road',
  [RideType.GroupRide]: 'Group Ride',
  [RideType.Other]: 'Other',
};
