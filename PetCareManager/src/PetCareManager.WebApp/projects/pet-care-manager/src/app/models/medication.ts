export interface Medication {
  medicationId: string;
  petId: string;
  name: string;
  dosage?: string;
  frequency?: string;
  startDate?: string;
  endDate?: string;
  createdAt: string;
}

export interface CreateMedicationDto {
  petId: string;
  name: string;
  dosage?: string;
  frequency?: string;
  startDate?: string;
  endDate?: string;
}

export interface UpdateMedicationDto {
  medicationId: string;
  petId: string;
  name: string;
  dosage?: string;
  frequency?: string;
  startDate?: string;
  endDate?: string;
}
