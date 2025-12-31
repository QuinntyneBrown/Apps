export interface LessonReminder {
  lessonReminderId: string;
  lessonId: string;
  userId: string;
  reminderDateTime: string;
  message?: string;
  isSent: boolean;
  isDismissed: boolean;
  createdAt: string;
}

export interface CreateLessonReminder {
  lessonId: string;
  userId: string;
  reminderDateTime: string;
  message?: string;
}

export interface UpdateLessonReminder {
  lessonReminderId: string;
  reminderDateTime: string;
  message?: string;
  isSent: boolean;
  isDismissed: boolean;
}
