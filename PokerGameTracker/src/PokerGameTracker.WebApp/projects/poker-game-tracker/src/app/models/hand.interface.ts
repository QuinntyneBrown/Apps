export interface Hand {
  handId: string;
  userId: string;
  sessionId: string;
  startingCards?: string;
  potSize?: number;
  wasWon: boolean;
  notes?: string;
  createdAt: string;
}

export interface CreateHand {
  userId: string;
  sessionId: string;
  startingCards?: string;
  potSize?: number;
  wasWon: boolean;
  notes?: string;
}

export interface UpdateHand {
  handId: string;
  userId: string;
  sessionId: string;
  startingCards?: string;
  potSize?: number;
  wasWon: boolean;
  notes?: string;
}
