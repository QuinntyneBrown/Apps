export enum EntryType {
  General = 0,
  Gratitude = 1,
  Reflection = 2,
  Prayer = 3,
  Goal = 4,
  Challenge = 5,
  Celebration = 6
}

export const EntryTypeLabels: Record<EntryType, string> = {
  [EntryType.General]: 'General',
  [EntryType.Gratitude]: 'Gratitude',
  [EntryType.Reflection]: 'Reflection',
  [EntryType.Prayer]: 'Prayer',
  [EntryType.Goal]: 'Goal',
  [EntryType.Challenge]: 'Challenge',
  [EntryType.Celebration]: 'Celebration'
};
