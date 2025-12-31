export enum ProjectPriority {
  Low = 0,
  Medium = 1,
  High = 2,
  Critical = 3
}

export const ProjectPriorityLabels: Record<ProjectPriority, string> = {
  [ProjectPriority.Low]: 'Low',
  [ProjectPriority.Medium]: 'Medium',
  [ProjectPriority.High]: 'High',
  [ProjectPriority.Critical]: 'Critical'
};
