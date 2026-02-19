export interface Value {
  valueId: string;
  missionStatementId?: string;
  userId: string;
  name: string;
  description?: string;
  priority: number;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateValue {
  userId: string;
  missionStatementId?: string;
  name: string;
  description?: string;
  priority: number;
}

export interface UpdateValue {
  valueId: string;
  name?: string;
  description?: string;
  priority?: number;
  missionStatementId?: string;
}
