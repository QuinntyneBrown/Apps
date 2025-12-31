export interface Manual {
  manualId: string;
  applianceId: string;
  title?: string;
  fileUrl?: string;
  fileType?: string;
  createdAt: string;
}

export interface CreateManualRequest {
  applianceId: string;
  title?: string;
  fileUrl?: string;
  fileType?: string;
}

export interface UpdateManualRequest {
  manualId: string;
  applianceId: string;
  title?: string;
  fileUrl?: string;
  fileType?: string;
}
