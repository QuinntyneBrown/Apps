export interface Assignment {
  assignmentId: string;
  choreId: string;
  choreName?: string;
  familyMemberId: string;
  familyMemberName?: string;
  assignedDate: Date;
  dueDate: Date;
  completedDate?: Date;
  isCompleted: boolean;
  isVerified: boolean;
  notes?: string;
  pointsEarned: number;
  isOverdue: boolean;
  createdAt: Date;
}

export interface CreateAssignment {
  choreId: string;
  familyMemberId: string;
  assignedDate: Date;
  dueDate: Date;
  notes?: string;
}

export interface UpdateAssignment {
  dueDate: Date;
  notes?: string;
}

export interface CompleteAssignment {
  notes?: string;
}

export interface VerifyAssignment {
  points: number;
}
