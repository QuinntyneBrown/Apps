export enum ProjectStatus {
  Idea = 0,
  Planned = 1,
  InProgress = 2,
  OnHold = 3,
  Completed = 4,
  Cancelled = 5
}

export const ProjectStatusLabels: Record<ProjectStatus, string> = {
  [ProjectStatus.Idea]: 'Idea',
  [ProjectStatus.Planned]: 'Planned',
  [ProjectStatus.InProgress]: 'In Progress',
  [ProjectStatus.OnHold]: 'On Hold',
  [ProjectStatus.Completed]: 'Completed',
  [ProjectStatus.Cancelled]: 'Cancelled'
};
