export interface NetWorthSnapshot {
  netWorthSnapshotId: string;
  snapshotDate: string;
  totalAssets: number;
  totalLiabilities: number;
  netWorth: number;
  notes?: string;
  createdAt: string;
}

export interface CreateNetWorthSnapshot {
  snapshotDate: string;
  totalAssets: number;
  totalLiabilities: number;
  notes?: string;
}

export interface UpdateNetWorthSnapshot {
  netWorthSnapshotId: string;
  snapshotDate: string;
  totalAssets: number;
  totalLiabilities: number;
  notes?: string;
}
