export interface Photo {
  photoId: string;
  userId: string;
  sessionId: string;
  fileName: string;
  filePath?: string;
  cameraSettings?: string;
  rating?: number;
  tags?: string;
  createdAt: string;
}

export interface CreatePhoto {
  userId: string;
  sessionId: string;
  fileName: string;
  filePath?: string;
  cameraSettings?: string;
  rating?: number;
  tags?: string;
}

export interface UpdatePhoto {
  photoId: string;
  fileName: string;
  filePath?: string;
  cameraSettings?: string;
  rating?: number;
  tags?: string;
}
