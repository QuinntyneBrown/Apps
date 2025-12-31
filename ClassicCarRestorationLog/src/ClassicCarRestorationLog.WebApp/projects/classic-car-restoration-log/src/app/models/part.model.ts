export interface Part {
  partId: string;
  userId: string;
  projectId: string;
  name: string;
  partNumber?: string;
  supplier?: string;
  cost?: number;
  orderedDate?: string;
  receivedDate?: string;
  isInstalled: boolean;
  notes?: string;
  createdAt: string;
}

export interface CreatePartCommand {
  userId: string;
  projectId: string;
  name: string;
  partNumber?: string;
  supplier?: string;
  cost?: number;
  orderedDate?: string;
  receivedDate?: string;
  isInstalled: boolean;
  notes?: string;
}

export interface UpdatePartCommand {
  partId: string;
  userId: string;
  projectId: string;
  name: string;
  partNumber?: string;
  supplier?: string;
  cost?: number;
  orderedDate?: string;
  receivedDate?: string;
  isInstalled: boolean;
  notes?: string;
}
