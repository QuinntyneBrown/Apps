export interface Interaction {
  interactionId: string;
  userId: string;
  contactId: string;
  interactionType: string;
  interactionDate: string;
  subject?: string;
  notes?: string;
  outcome?: string;
  durationMinutes?: number;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateInteractionRequest {
  userId: string;
  contactId: string;
  interactionType: string;
  interactionDate: string;
  subject?: string;
  notes?: string;
  outcome?: string;
  durationMinutes?: number;
}
