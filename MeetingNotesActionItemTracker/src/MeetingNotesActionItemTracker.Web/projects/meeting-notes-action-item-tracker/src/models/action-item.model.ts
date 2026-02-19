import { Priority } from './priority.enum';
import { ActionItemStatus } from './action-item-status.enum';

export interface ActionItem {
  actionItemId: string;
  userId: string;
  meetingId: string;
  description: string;
  responsiblePerson?: string;
  dueDate?: string;
  priority: Priority;
  status: ActionItemStatus;
  completedDate?: string;
  notes?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateActionItemDto {
  userId: string;
  meetingId: string;
  description: string;
  responsiblePerson?: string;
  dueDate?: string;
  priority: Priority;
  status: ActionItemStatus;
  notes?: string;
}

export interface UpdateActionItemDto {
  actionItemId: string;
  description: string;
  responsiblePerson?: string;
  dueDate?: string;
  priority: Priority;
  status: ActionItemStatus;
  notes?: string;
}
