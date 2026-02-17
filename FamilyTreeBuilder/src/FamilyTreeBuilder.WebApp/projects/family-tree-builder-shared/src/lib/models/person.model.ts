import { Gender } from './gender.enum';

export interface Person {
  personId: string;
  userId: string;
  firstName: string;
  lastName?: string;
  gender?: Gender;
  dateOfBirth?: string;
  dateOfDeath?: string;
  birthPlace?: string;
  createdAt: string;
}

export interface CreatePersonRequest {
  userId: string;
  firstName: string;
  lastName?: string;
  gender?: Gender;
  dateOfBirth?: string;
  dateOfDeath?: string;
  birthPlace?: string;
}

export interface UpdatePersonRequest {
  personId?: string;
  firstName: string;
  lastName?: string;
  gender?: Gender;
  dateOfBirth?: string;
  dateOfDeath?: string;
  birthPlace?: string;
}
