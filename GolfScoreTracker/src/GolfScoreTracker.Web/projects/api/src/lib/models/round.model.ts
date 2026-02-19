export interface Round {
  roundId: string;
  userId: string;
  courseId: string;
  playedDate: Date;
  totalScore: number;
  totalPar: number;
  weather?: string;
  notes?: string;
  createdAt: Date;
  courseName?: string;
}

export interface CreateRoundCommand {
  userId: string;
  courseId: string;
  playedDate: Date;
  totalScore: number;
  totalPar: number;
  weather?: string;
  notes?: string;
}

export interface UpdateRoundCommand {
  roundId: string;
  userId: string;
  courseId: string;
  playedDate: Date;
  totalScore: number;
  totalPar: number;
  weather?: string;
  notes?: string;
}
