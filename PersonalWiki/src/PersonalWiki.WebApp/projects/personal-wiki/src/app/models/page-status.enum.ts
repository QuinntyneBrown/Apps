export enum PageStatus {
  Draft = 0,
  Published = 1,
  Review = 2,
  Archived = 3
}

export const PageStatusLabels: Record<PageStatus, string> = {
  [PageStatus.Draft]: 'Draft',
  [PageStatus.Published]: 'Published',
  [PageStatus.Review]: 'Review',
  [PageStatus.Archived]: 'Archived'
};
