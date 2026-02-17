import { BeverageType } from './beverage-type.enum';

export interface Intake {
  intakeId: string;
  userId: string;
  beverageType: BeverageType;
  amountMl: number;
  intakeTime: Date;
  notes?: string;
  createdAt: Date;
}

export interface CreateIntakeCommand {
  userId: string;
  beverageType: BeverageType;
  amountMl: number;
  intakeTime: Date;
  notes?: string;
}

export interface UpdateIntakeCommand {
  intakeId: string;
  userId: string;
  beverageType: BeverageType;
  amountMl: number;
  intakeTime: Date;
  notes?: string;
}
