export interface DoseSchedule {
  doseScheduleId: string;
  userId: string;
  medicationId: string;
  scheduledTime: string;
  daysOfWeek: string;
  frequency: string;
  reminderEnabled: boolean;
  reminderOffsetMinutes: number;
  lastTaken?: string | null;
  isActive: boolean;
  createdAt: string;
}

export interface CreateDoseScheduleCommand {
  userId: string;
  medicationId: string;
  scheduledTime: string;
  daysOfWeek: string;
  frequency: string;
  reminderEnabled: boolean;
  reminderOffsetMinutes: number;
}

export interface UpdateDoseScheduleCommand {
  doseScheduleId: string;
  scheduledTime: string;
  daysOfWeek: string;
  frequency: string;
  reminderEnabled: boolean;
  reminderOffsetMinutes: number;
  isActive: boolean;
}
