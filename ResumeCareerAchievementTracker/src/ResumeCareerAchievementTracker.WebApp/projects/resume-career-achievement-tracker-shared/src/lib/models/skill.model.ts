export interface Skill {
  skillId: string;
  userId: string;
  name: string;
  category: string;
  proficiencyLevel: string;
  yearsOfExperience?: number;
  lastUsedDate?: string;
  notes?: string;
  isFeatured: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateSkill {
  userId: string;
  name: string;
  category: string;
  proficiencyLevel: string;
  yearsOfExperience?: number;
  lastUsedDate?: string;
  notes?: string;
}

export interface UpdateSkill {
  skillId: string;
  name: string;
  category: string;
  proficiencyLevel: string;
  yearsOfExperience?: number;
  lastUsedDate?: string;
  notes?: string;
}
