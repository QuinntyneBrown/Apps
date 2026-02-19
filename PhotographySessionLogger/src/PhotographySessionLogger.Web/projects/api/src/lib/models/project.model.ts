export interface Project {
  projectId: string;
  userId: string;
  name: string;
  description?: string;
  dueDate?: string;
  isCompleted: boolean;
  notes?: string;
  createdAt: string;
}

export interface CreateProject {
  userId: string;
  name: string;
  description?: string;
  dueDate?: string;
  notes?: string;
}

export interface UpdateProject {
  projectId: string;
  name: string;
  description?: string;
  dueDate?: string;
  isCompleted: boolean;
  notes?: string;
}
