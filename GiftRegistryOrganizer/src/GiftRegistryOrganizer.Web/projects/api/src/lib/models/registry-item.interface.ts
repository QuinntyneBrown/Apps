import { Priority } from './priority.enum';

export interface RegistryItem {
  registryItemId: string;
  registryId: string;
  name: string;
  description?: string;
  price?: number;
  url?: string;
  quantityDesired: number;
  quantityReceived: number;
  priority: Priority;
  isFulfilled: boolean;
  createdAt: Date;
}
