export interface Installation {
  installationId: string;
  modificationId: string;
  vehicleInfo: string;
  installationDate: Date;
  installedBy?: string;
  installationCost?: number;
  partsCost?: number;
  laborHours?: number;
  partsUsed: string[];
  notes?: string;
  difficultyRating?: number;
  satisfactionRating?: number;
  photos: string[];
  isCompleted: boolean;
  totalCost: number;
}

export interface CreateInstallationCommand {
  modificationId: string;
  vehicleInfo: string;
  installationDate: Date;
  installedBy?: string;
  installationCost?: number;
  partsCost?: number;
  laborHours?: number;
  partsUsed?: string[];
  notes?: string;
  difficultyRating?: number;
  satisfactionRating?: number;
  photos?: string[];
  isCompleted: boolean;
}

export interface UpdateInstallationCommand extends CreateInstallationCommand {
  installationId: string;
}
