import { DateType } from './date-type';
import { RecurrencePattern } from './recurrence-pattern';

export interface ImportantDate {
  dateId: string;
  userId: string;
  personName: string;
  dateType: DateType;
  dateValue: Date;
  recurrencePattern: RecurrencePattern;
  relationship: string;
  notes: string;
  isActive: boolean;
  createdAt: Date;
}
