export enum BatchStatus {
  Planned = 0,
  Fermenting = 1,
  Bottled = 2,
  Conditioning = 3,
  Completed = 4,
  Failed = 5
}

export const BatchStatusLabels: Record<BatchStatus, string> = {
  [BatchStatus.Planned]: 'Planned',
  [BatchStatus.Fermenting]: 'Fermenting',
  [BatchStatus.Bottled]: 'Bottled',
  [BatchStatus.Conditioning]: 'Conditioning',
  [BatchStatus.Completed]: 'Completed',
  [BatchStatus.Failed]: 'Failed'
};
