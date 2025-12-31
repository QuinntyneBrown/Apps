export enum SecurityLevel {
  Unknown = 0,
  Low = 1,
  Medium = 2,
  High = 3
}

export const SecurityLevelLabels: Record<SecurityLevel, string> = {
  [SecurityLevel.Unknown]: 'Unknown',
  [SecurityLevel.Low]: 'Low',
  [SecurityLevel.Medium]: 'Medium',
  [SecurityLevel.High]: 'High'
};
