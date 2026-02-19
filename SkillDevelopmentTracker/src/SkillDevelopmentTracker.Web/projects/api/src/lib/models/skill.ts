import { ProficiencyLevel } from './proficiency-level';

export interface Skill {
  skillId: string;
  userId: string;
  name: string;
  category: string;
  proficiencyLevel: ProficiencyLevel;
  targetLevel?: ProficiencyLevel;
  startDate: string;
  targetDate?: string;
  hoursSpent: number;
  notes?: string;
  courseIds: string[];
  createdAt: string;
  updatedAt?: string;
}
