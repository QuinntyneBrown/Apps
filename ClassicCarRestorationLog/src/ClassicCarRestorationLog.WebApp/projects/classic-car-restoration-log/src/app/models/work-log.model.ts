export interface WorkLog {
  workLogId: string;
  userId: string;
  projectId: string;
  workDate: string;
  hoursWorked: number;
  description: string;
  workPerformed?: string;
  createdAt: string;
}

export interface CreateWorkLogCommand {
  userId: string;
  projectId: string;
  workDate: string;
  hoursWorked: number;
  description: string;
  workPerformed?: string;
}

export interface UpdateWorkLogCommand {
  workLogId: string;
  userId: string;
  projectId: string;
  workDate: string;
  hoursWorked: number;
  description: string;
  workPerformed?: string;
}
