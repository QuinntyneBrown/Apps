export interface Trigger {
  triggerId: string;
  userId: string;
  name: string;
  description?: string;
  triggerType: string;
  impactLevel: number;
  createdAt: Date;
}
