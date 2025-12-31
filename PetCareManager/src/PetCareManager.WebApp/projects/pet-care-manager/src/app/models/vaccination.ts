export interface Vaccination {
  vaccinationId: string;
  petId: string;
  name: string;
  dateAdministered: string;
  nextDueDate?: string;
  vetName?: string;
  createdAt: string;
}

export interface CreateVaccinationDto {
  petId: string;
  name: string;
  dateAdministered: string;
  nextDueDate?: string;
  vetName?: string;
}

export interface UpdateVaccinationDto {
  vaccinationId: string;
  petId: string;
  name: string;
  dateAdministered: string;
  nextDueDate?: string;
  vetName?: string;
}
