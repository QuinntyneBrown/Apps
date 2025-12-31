export interface Technique {
  techniqueId: string;
  userId: string;
  name: string;
  description: string;
  category: string;
  difficultyLevel: number;
  instructions: string;
  tips: string | null;
  isFavorite: boolean;
  createdAt: string;
}

export interface CreateTechnique {
  userId: string;
  name: string;
  description: string;
  category: string;
  difficultyLevel: number;
  instructions: string;
  tips: string | null;
}

export interface UpdateTechnique {
  techniqueId: string;
  name: string;
  description: string;
  category: string;
  difficultyLevel: number;
  instructions: string;
  tips: string | null;
}
