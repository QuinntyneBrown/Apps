import { ProjectPhase } from './project-phase.enum';

export interface PhotoLog {
  photoLogId: string;
  userId: string;
  projectId: string;
  photoDate: string;
  description?: string;
  photoUrl?: string;
  phase?: ProjectPhase;
  createdAt: string;
}

export interface CreatePhotoLogCommand {
  userId: string;
  projectId: string;
  photoDate: string;
  description?: string;
  photoUrl?: string;
  phase?: ProjectPhase;
}

export interface UpdatePhotoLogCommand {
  photoLogId: string;
  userId: string;
  projectId: string;
  photoDate: string;
  description?: string;
  photoUrl?: string;
  phase?: ProjectPhase;
}
