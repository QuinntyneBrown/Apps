export enum TaskType {
  Exercise = 0,
  Meditation = 1,
  Journaling = 2,
  Reading = 3,
  Breakfast = 4,
  Hygiene = 5,
  Planning = 6,
  Gratitude = 7,
  Learning = 8,
  Other = 9
}

export const TaskTypeLabels: Record<TaskType, string> = {
  [TaskType.Exercise]: 'Exercise',
  [TaskType.Meditation]: 'Meditation',
  [TaskType.Journaling]: 'Journaling',
  [TaskType.Reading]: 'Reading',
  [TaskType.Breakfast]: 'Breakfast',
  [TaskType.Hygiene]: 'Hygiene',
  [TaskType.Planning]: 'Planning',
  [TaskType.Gratitude]: 'Gratitude',
  [TaskType.Learning]: 'Learning',
  [TaskType.Other]: 'Other'
};
