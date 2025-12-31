import { PetType } from './pet-type';

export interface Pet {
  petId: string;
  userId: string;
  name: string;
  petType: PetType;
  breed?: string;
  dateOfBirth?: string;
  color?: string;
  weight?: number;
  microchipNumber?: string;
  createdAt: string;
}

export interface CreatePetDto {
  userId: string;
  name: string;
  petType: PetType;
  breed?: string;
  dateOfBirth?: string;
  color?: string;
  weight?: number;
  microchipNumber?: string;
}

export interface UpdatePetDto {
  petId: string;
  userId: string;
  name: string;
  petType: PetType;
  breed?: string;
  dateOfBirth?: string;
  color?: string;
  weight?: number;
  microchipNumber?: string;
}
