export interface ValueEstimate {
  valueEstimateId: string;
  itemId: string;
  estimatedValue: number;
  estimationDate: string;
  source?: string | null;
  notes?: string | null;
  createdAt: string;
}

export interface CreateValueEstimateCommand {
  itemId: string;
  estimatedValue: number;
  estimationDate: string;
  source?: string | null;
  notes?: string | null;
}

export interface UpdateValueEstimateCommand {
  valueEstimateId: string;
  estimatedValue: number;
  estimationDate: string;
  source?: string | null;
  notes?: string | null;
}
