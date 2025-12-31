export interface LegacyInstruction {
  legacyInstructionId: string;
  userId: string;
  title: string;
  content: string;
  category?: string;
  priority: number;
  assignedTo?: string;
  executionTiming?: string;
  lastUpdatedAt: Date;
  createdAt: Date;
}

export interface CreateLegacyInstructionCommand {
  userId: string;
  title: string;
  content: string;
  category?: string;
  priority: number;
  assignedTo?: string;
  executionTiming?: string;
}

export interface UpdateLegacyInstructionCommand {
  legacyInstructionId: string;
  title: string;
  content: string;
  category?: string;
  priority: number;
  assignedTo?: string;
  executionTiming?: string;
}
