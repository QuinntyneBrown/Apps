import { BatchStatus } from './batch-status.enum';

export interface Batch {
  batchId: string;
  userId: string;
  recipeId: string;
  batchNumber: string;
  status: BatchStatus;
  brewDate: Date;
  bottlingDate?: Date;
  actualOriginalGravity?: number;
  actualFinalGravity?: number;
  actualABV?: number;
  notes?: string;
  createdAt: Date;
}

export interface CreateBatchRequest {
  userId: string;
  recipeId: string;
  batchNumber: string;
  brewDate: Date;
  notes?: string;
}

export interface UpdateBatchRequest {
  batchId: string;
  batchNumber: string;
  status: BatchStatus;
  brewDate: Date;
  bottlingDate?: Date;
  actualOriginalGravity?: number;
  actualFinalGravity?: number;
  actualABV?: number;
  notes?: string;
}
