export interface Rating {
  ratingId: string;
  dateIdeaId?: string;
  experienceId?: string;
  userId: string;
  score: number;
  review?: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateRating {
  dateIdeaId?: string;
  experienceId?: string;
  userId: string;
  score: number;
  review?: string;
}

export interface UpdateRating {
  ratingId: string;
  score: number;
  review?: string;
}
