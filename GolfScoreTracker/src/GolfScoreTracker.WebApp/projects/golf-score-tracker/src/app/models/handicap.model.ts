export interface Handicap {
  handicapId: string;
  userId: string;
  handicapIndex: number;
  calculatedDate: Date;
  roundsUsed: number;
  notes?: string;
  createdAt: Date;
}

export interface CreateHandicapCommand {
  userId: string;
  handicapIndex: number;
  calculatedDate: Date;
  roundsUsed: number;
  notes?: string;
}

export interface UpdateHandicapCommand {
  handicapId: string;
  userId: string;
  handicapIndex: number;
  calculatedDate: Date;
  roundsUsed: number;
  notes?: string;
}
