export enum ApplicationStatus {
  Draft = 0,
  Applied = 1,
  UnderReview = 2,
  PhoneScreen = 3,
  Interviewing = 4,
  OfferReceived = 5,
  Accepted = 6,
  Rejected = 7,
  Withdrawn = 8
}

export const ApplicationStatusLabels: Record<ApplicationStatus, string> = {
  [ApplicationStatus.Draft]: 'Draft',
  [ApplicationStatus.Applied]: 'Applied',
  [ApplicationStatus.UnderReview]: 'Under Review',
  [ApplicationStatus.PhoneScreen]: 'Phone Screen',
  [ApplicationStatus.Interviewing]: 'Interviewing',
  [ApplicationStatus.OfferReceived]: 'Offer Received',
  [ApplicationStatus.Accepted]: 'Accepted',
  [ApplicationStatus.Rejected]: 'Rejected',
  [ApplicationStatus.Withdrawn]: 'Withdrawn'
};
