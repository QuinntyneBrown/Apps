import { TaskCategory } from './task-category.enum';
import { TaskPriority } from './task-priority.enum';

export interface AdminTask {
  adminTaskId: string;
  userId: string;
  title: string;
  description?: string;
  category: TaskCategory;
  priority: TaskPriority;
  dueDate?: string;
  isCompleted: boolean;
  completionDate?: string;
  isRecurring: boolean;
  recurrencePattern?: string;
  notes?: string;
  createdAt: string;
}

export interface CreateAdminTask {
  userId: string;
  title: string;
  description?: string;
  category: TaskCategory;
  priority: TaskPriority;
  dueDate?: string;
  isRecurring: boolean;
  recurrencePattern?: string;
  notes?: string;
}

export interface UpdateAdminTask {
  adminTaskId: string;
  title: string;
  description?: string;
  category: TaskCategory;
  priority: TaskPriority;
  dueDate?: string;
  isRecurring: boolean;
  recurrencePattern?: string;
  notes?: string;
}
