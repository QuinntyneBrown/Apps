export enum Room {
  LivingRoom = 0,
  Bedroom = 1,
  Kitchen = 2,
  DiningRoom = 3,
  Bathroom = 4,
  Garage = 5,
  Basement = 6,
  Attic = 7,
  Office = 8,
  Storage = 9,
  Other = 10
}

export const RoomLabels: Record<Room, string> = {
  [Room.LivingRoom]: 'Living Room',
  [Room.Bedroom]: 'Bedroom',
  [Room.Kitchen]: 'Kitchen',
  [Room.DiningRoom]: 'Dining Room',
  [Room.Bathroom]: 'Bathroom',
  [Room.Garage]: 'Garage',
  [Room.Basement]: 'Basement',
  [Room.Attic]: 'Attic',
  [Room.Office]: 'Office',
  [Room.Storage]: 'Storage',
  [Room.Other]: 'Other'
};
