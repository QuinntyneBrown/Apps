export enum ActivityType {
  TeamSports = 0,
  IndividualSports = 1,
  Music = 2,
  Art = 3,
  Academic = 4,
  Dance = 5,
  Other = 6
}

export const ActivityTypeLabels: Record<ActivityType, string> = {
  [ActivityType.TeamSports]: 'Team Sports',
  [ActivityType.IndividualSports]: 'Individual Sports',
  [ActivityType.Music]: 'Music',
  [ActivityType.Art]: 'Art',
  [ActivityType.Academic]: 'Academic',
  [ActivityType.Dance]: 'Dance',
  [ActivityType.Other]: 'Other'
};
