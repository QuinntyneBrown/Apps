export interface MissionStatement {
  missionStatementId: string;
  userId: string;
  title: string;
  text: string;
  version: number;
  isCurrentVersion: boolean;
  statementDate: Date;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateMissionStatement {
  userId: string;
  title: string;
  text: string;
  statementDate?: Date;
}

export interface UpdateMissionStatement {
  missionStatementId: string;
  title?: string;
  text?: string;
  isCurrentVersion?: boolean;
}
