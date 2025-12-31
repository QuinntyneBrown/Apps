export enum SessionType {
  Portrait = 0,
  Landscape = 1,
  Wedding = 2,
  Event = 3,
  Product = 4,
  Wildlife = 5,
  Sports = 6,
  Macro = 7,
  Other = 8
}

export const SessionTypeLabels: Record<SessionType, string> = {
  [SessionType.Portrait]: 'Portrait',
  [SessionType.Landscape]: 'Landscape',
  [SessionType.Wedding]: 'Wedding',
  [SessionType.Event]: 'Event',
  [SessionType.Product]: 'Product',
  [SessionType.Wildlife]: 'Wildlife',
  [SessionType.Sports]: 'Sports',
  [SessionType.Macro]: 'Macro',
  [SessionType.Other]: 'Other'
};
