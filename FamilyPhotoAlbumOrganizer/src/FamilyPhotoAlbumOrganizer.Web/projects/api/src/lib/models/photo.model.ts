export interface Photo {
  photoId: string;
  userId: string;
  albumId?: string;
  fileName: string;
  fileUrl: string;
  thumbnailUrl?: string;
  caption?: string;
  dateTaken?: Date;
  location?: string;
  isFavorite: boolean;
  createdAt: Date;
}

export interface CreatePhotoCommand {
  userId: string;
  albumId?: string;
  fileName: string;
  fileUrl: string;
  thumbnailUrl?: string;
  caption?: string;
  dateTaken?: Date;
  location?: string;
  isFavorite: boolean;
}

export interface UpdatePhotoCommand {
  photoId: string;
  albumId?: string;
  caption?: string;
  dateTaken?: Date;
  location?: string;
  isFavorite: boolean;
}
