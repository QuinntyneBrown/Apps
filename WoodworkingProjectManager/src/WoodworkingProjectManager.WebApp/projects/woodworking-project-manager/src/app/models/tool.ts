export interface Tool {
  toolId: string;
  userId: string;
  name: string;
  brand?: string;
  model?: string;
  description?: string;
  purchasePrice?: number;
  purchaseDate?: string;
  location?: string;
  notes?: string;
  createdAt: string;
}
