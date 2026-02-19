export interface HoleScore {
  holeScoreId: string;
  roundId: string;
  holeNumber: number;
  par: number;
  score: number;
  putts?: number;
  fairwayHit: boolean;
  greenInRegulation: boolean;
  notes?: string;
  createdAt: Date;
}

export interface CreateHoleScoreCommand {
  roundId: string;
  holeNumber: number;
  par: number;
  score: number;
  putts?: number;
  fairwayHit: boolean;
  greenInRegulation: boolean;
  notes?: string;
}

export interface UpdateHoleScoreCommand {
  holeScoreId: string;
  roundId: string;
  holeNumber: number;
  par: number;
  score: number;
  putts?: number;
  fairwayHit: boolean;
  greenInRegulation: boolean;
  notes?: string;
}
