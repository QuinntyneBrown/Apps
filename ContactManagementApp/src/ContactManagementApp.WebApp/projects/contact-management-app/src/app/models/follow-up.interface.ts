export interface FollowUp {
  followUpId: string;
  userId: string;
  contactId: string;
  description: string;
  dueDate: string;
  isCompleted: boolean;
  completedDate?: string;
  priority: string;
  notes?: string;
  isOverdue: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateFollowUpRequest {
  userId: string;
  contactId: string;
  description: string;
  dueDate: string;
  priority: string;
  notes?: string;
}
