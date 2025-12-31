export interface Experience {
  experienceId: string;
  dateIdeaId: string;
  userId: string;
  experienceDate: Date;
  notes: string;
  actualCost?: number;
  photos?: string;
  wasSuccessful: boolean;
  wouldRepeat: boolean;
  createdAt: Date;
}

export interface CreateExperience {
  dateIdeaId: string;
  userId: string;
  experienceDate: Date;
  notes: string;
  actualCost?: number;
  photos?: string;
  wasSuccessful: boolean;
  wouldRepeat: boolean;
}

export interface UpdateExperience {
  experienceId: string;
  experienceDate: Date;
  notes: string;
  actualCost?: number;
  photos?: string;
  wasSuccessful: boolean;
  wouldRepeat: boolean;
}
