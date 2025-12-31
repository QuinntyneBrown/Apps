import { RegistryType } from './registry-type.enum';

export interface Registry {
  registryId: string;
  userId: string;
  name: string;
  description?: string;
  type: RegistryType;
  eventDate: Date;
  isActive: boolean;
  createdAt: Date;
}
