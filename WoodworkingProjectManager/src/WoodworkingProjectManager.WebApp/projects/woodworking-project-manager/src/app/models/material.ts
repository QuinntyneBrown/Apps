export interface Material {
  materialId: string;
  userId: string;
  projectId?: string;
  name: string;
  description?: string;
  quantity: number;
  unit: string;
  cost?: number;
  supplier?: string;
  createdAt: string;
}
