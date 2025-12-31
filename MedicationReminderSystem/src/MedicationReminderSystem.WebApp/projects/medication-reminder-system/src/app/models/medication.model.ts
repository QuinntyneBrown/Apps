import { MedicationType } from './medication-type.enum';

export interface Medication {
  medicationId: string;
  userId: string;
  name: string;
  medicationType: MedicationType;
  dosage: string;
  prescribingDoctor?: string | null;
  prescriptionDate?: string | null;
  purpose?: string | null;
  instructions?: string | null;
  sideEffects?: string | null;
  isActive: boolean;
  createdAt: string;
}

export interface CreateMedicationCommand {
  userId: string;
  name: string;
  medicationType: MedicationType;
  dosage: string;
  prescribingDoctor?: string | null;
  prescriptionDate?: string | null;
  purpose?: string | null;
  instructions?: string | null;
  sideEffects?: string | null;
}

export interface UpdateMedicationCommand {
  medicationId: string;
  name: string;
  medicationType: MedicationType;
  dosage: string;
  prescribingDoctor?: string | null;
  prescriptionDate?: string | null;
  purpose?: string | null;
  instructions?: string | null;
  sideEffects?: string | null;
  isActive: boolean;
}
