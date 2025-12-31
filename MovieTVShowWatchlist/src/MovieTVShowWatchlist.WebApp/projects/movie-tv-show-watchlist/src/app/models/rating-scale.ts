export enum RatingScale {
  FiveStar = 'FiveStar',
  TenPoint = 'TenPoint',
  HundredPoint = 'HundredPoint'
}

export const ratingScaleLabels: Record<RatingScale, string> = {
  [RatingScale.FiveStar]: '5 Star',
  [RatingScale.TenPoint]: '10 Point',
  [RatingScale.HundredPoint]: '100 Point'
};
