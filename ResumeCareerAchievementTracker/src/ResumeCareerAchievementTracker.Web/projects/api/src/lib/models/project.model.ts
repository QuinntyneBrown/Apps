export interface Project {
  projectId: string;
  userId: string;
  name: string;
  description: string;
  organization?: string;
  role?: string;
  startDate: string;
  endDate?: string;
  technologies: string[];
  outcomes: string[];
  projectUrl?: string;
  isFeatured: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateProject {
  userId: string;
  name: string;
  description: string;
  organization?: string;
  role?: string;
  startDate: string;
  endDate?: string;
  technologies?: string[];
  outcomes?: string[];
  projectUrl?: string;
}

export interface UpdateProject {
  projectId: string;
  name: string;
  description: string;
  organization?: string;
  role?: string;
  startDate: string;
  endDate?: string;
  technologies?: string[];
  outcomes?: string[];
  projectUrl?: string;
}
