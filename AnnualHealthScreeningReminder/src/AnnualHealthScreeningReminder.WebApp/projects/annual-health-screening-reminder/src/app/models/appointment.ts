export interface Appointment {
  appointmentId: string;
  userId: string;
  screeningId: string;
  appointmentDate: string;
  location: string;
  provider?: string;
  isCompleted: boolean;
  notes?: string;
  createdAt: string;
  isUpcoming: boolean;
}
