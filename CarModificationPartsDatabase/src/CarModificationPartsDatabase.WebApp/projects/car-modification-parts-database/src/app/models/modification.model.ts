import { ModCategory } from './mod-category.enum';

export interface Modification {
  modificationId: string;
  name: string;
  category: ModCategory;
  description: string;
  manufacturer?: string;
  estimatedCost?: number;
  difficultyLevel?: number;
  estimatedInstallationTime?: number;
  performanceGain?: string;
  compatibleVehicles: string[];
  requiredTools: string[];
  notes?: string;
}

export interface CreateModificationCommand {
  name: string;
  category: ModCategory;
  description: string;
  manufacturer?: string;
  estimatedCost?: number;
  difficultyLevel?: number;
  estimatedInstallationTime?: number;
  performanceGain?: string;
  compatibleVehicles?: string[];
  requiredTools?: string[];
  notes?: string;
}

export interface UpdateModificationCommand extends CreateModificationCommand {
  modificationId: string;
}
