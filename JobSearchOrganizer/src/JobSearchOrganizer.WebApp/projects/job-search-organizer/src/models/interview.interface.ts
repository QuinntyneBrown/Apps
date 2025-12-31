export interface Interview {
  interviewId: string;
  userId: string;
  applicationId: string;
  interviewType: string;
  scheduledDateTime: string;
  durationMinutes?: number;
  interviewers: string[];
  location?: string;
  preparationNotes?: string;
  feedback?: string;
  isCompleted: boolean;
  completedDate?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateInterview {
  userId: string;
  applicationId: string;
  interviewType: string;
  scheduledDateTime: string;
  durationMinutes?: number;
  interviewers: string[];
  location?: string;
  preparationNotes?: string;
}

export interface UpdateInterview {
  interviewId: string;
  interviewType: string;
  scheduledDateTime: string;
  durationMinutes?: number;
  interviewers: string[];
  location?: string;
  preparationNotes?: string;
  feedback?: string;
  isCompleted: boolean;
}
