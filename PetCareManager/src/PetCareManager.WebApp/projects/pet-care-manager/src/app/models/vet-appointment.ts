export interface VetAppointment {
  vetAppointmentId: string;
  petId: string;
  appointmentDate: string;
  vetName?: string;
  reason?: string;
  notes?: string;
  cost?: number;
  createdAt: string;
}

export interface CreateVetAppointmentDto {
  petId: string;
  appointmentDate: string;
  vetName?: string;
  reason?: string;
  notes?: string;
  cost?: number;
}

export interface UpdateVetAppointmentDto {
  vetAppointmentId: string;
  petId: string;
  appointmentDate: string;
  vetName?: string;
  reason?: string;
  notes?: string;
  cost?: number;
}
