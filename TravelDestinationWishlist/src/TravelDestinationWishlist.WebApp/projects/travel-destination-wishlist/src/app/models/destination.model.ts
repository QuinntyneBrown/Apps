import { DestinationType } from './destination-type.enum';

export interface Destination {
  destinationId: string;
  userId: string;
  name: string;
  country: string;
  destinationType: DestinationType;
  description?: string;
  priority: number;
  isVisited: boolean;
  visitedDate?: string;
  notes?: string;
  createdAt: string;
}
