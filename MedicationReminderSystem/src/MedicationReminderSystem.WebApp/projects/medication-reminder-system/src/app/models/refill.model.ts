export interface Refill {
  refillId: string;
  userId: string;
  medicationId: string;
  refillDate: string;
  quantity: number;
  pharmacyName?: string | null;
  cost?: number | null;
  nextRefillDate?: string | null;
  refillsRemaining?: number | null;
  notes?: string | null;
  createdAt: string;
}

export interface CreateRefillCommand {
  userId: string;
  medicationId: string;
  refillDate: string;
  quantity: number;
  pharmacyName?: string | null;
  cost?: number | null;
  nextRefillDate?: string | null;
  refillsRemaining?: number | null;
  notes?: string | null;
}

export interface UpdateRefillCommand {
  refillId: string;
  refillDate: string;
  quantity: number;
  pharmacyName?: string | null;
  cost?: number | null;
  nextRefillDate?: string | null;
  refillsRemaining?: number | null;
  notes?: string | null;
}
