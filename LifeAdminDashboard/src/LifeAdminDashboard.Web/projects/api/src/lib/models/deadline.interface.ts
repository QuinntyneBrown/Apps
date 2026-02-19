export interface Deadline {
  deadlineId: string;
  userId: string;
  title: string;
  description?: string;
  deadlineDateTime: string;
  category?: string;
  isCompleted: boolean;
  completionDate?: string;
  remindersEnabled: boolean;
  reminderDaysAdvance: number;
  notes?: string;
  createdAt: string;
}

export interface CreateDeadline {
  userId: string;
  title: string;
  description?: string;
  deadlineDateTime: string;
  category?: string;
  remindersEnabled: boolean;
  reminderDaysAdvance: number;
  notes?: string;
}

export interface UpdateDeadline {
  deadlineId: string;
  title: string;
  description?: string;
  deadlineDateTime: string;
  category?: string;
  remindersEnabled: boolean;
  reminderDaysAdvance: number;
  notes?: string;
}
