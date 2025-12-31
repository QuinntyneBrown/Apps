export enum SessionType {
  DeepWork = 0,
  Pomodoro = 1,
  Study = 2,
  Creative = 3,
  Meeting = 4,
  Learning = 5,
  Planning = 6,
  Other = 7
}

export const SessionTypeLabels: Record<SessionType, string> = {
  [SessionType.DeepWork]: 'Deep Work',
  [SessionType.Pomodoro]: 'Pomodoro',
  [SessionType.Study]: 'Study',
  [SessionType.Creative]: 'Creative',
  [SessionType.Meeting]: 'Meeting',
  [SessionType.Learning]: 'Learning',
  [SessionType.Planning]: 'Planning',
  [SessionType.Other]: 'Other'
};
