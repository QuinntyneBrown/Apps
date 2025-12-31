export interface TastingNote {
  tastingNoteId: string;
  userId: string;
  batchId: string;
  tastingDate: Date;
  rating: number;
  appearance?: string;
  aroma?: string;
  flavor?: string;
  mouthfeel?: string;
  overallImpression?: string;
  createdAt: Date;
}

export interface CreateTastingNoteRequest {
  userId: string;
  batchId: string;
  tastingDate: Date;
  rating: number;
  appearance?: string;
  aroma?: string;
  flavor?: string;
  mouthfeel?: string;
  overallImpression?: string;
}

export interface UpdateTastingNoteRequest {
  tastingNoteId: string;
  tastingDate: Date;
  rating: number;
  appearance?: string;
  aroma?: string;
  flavor?: string;
  mouthfeel?: string;
  overallImpression?: string;
}
