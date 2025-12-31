export interface TastingNote {
  tastingNoteId: string;
  userId: string;
  wineId: string;
  tastingDate: string;
  rating: number;
  appearance?: string;
  aroma?: string;
  taste?: string;
  finish?: string;
  overallImpression?: string;
  createdAt: string;
}
