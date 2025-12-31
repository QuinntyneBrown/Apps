export interface Material {
  materialId: string;
  projectId: string;
  name: string;
  quantity: number;
  unit?: string;
  unitCost?: number;
  totalCost?: number;
  supplier?: string;
  createdAt: string;
}
