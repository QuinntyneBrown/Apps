export interface Statistic {
  statisticId: string;
  userId: string;
  teamId: string;
  statName: string;
  value: number;
  recordedDate: Date;
  createdAt: Date;
}

export interface CreateStatisticRequest {
  teamId: string;
  statName: string;
  value: number;
  recordedDate: Date;
}

export interface UpdateStatisticRequest {
  statisticId: string;
  teamId: string;
  statName: string;
  value: number;
  recordedDate: Date;
}
