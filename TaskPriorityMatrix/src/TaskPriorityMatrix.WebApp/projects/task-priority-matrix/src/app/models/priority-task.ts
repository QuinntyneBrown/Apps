import { Urgency } from './urgency';
import { Importance } from './importance';
import { TaskStatus } from './task-status';

export interface PriorityTask {
  priorityTaskId: string;
  title: string;
  description: string;
  urgency: Urgency;
  importance: Importance;
  status: TaskStatus;
  dueDate: Date | null;
  categoryId: string | null;
  createdAt: Date;
  completedAt: Date | null;
}

export interface CreatePriorityTaskRequest {
  title: string;
  description: string;
  urgency: Urgency;
  importance: Importance;
  dueDate: Date | null;
  categoryId: string | null;
}

export interface UpdatePriorityTaskRequest {
  priorityTaskId: string;
  title: string;
  description: string;
  urgency: Urgency;
  importance: Importance;
  status: TaskStatus;
  dueDate: Date | null;
  categoryId: string | null;
}
