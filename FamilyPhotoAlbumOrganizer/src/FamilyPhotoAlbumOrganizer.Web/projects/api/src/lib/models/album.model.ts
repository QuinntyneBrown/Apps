export interface Album {
  albumId: string;
  userId: string;
  name: string;
  description?: string;
  coverPhotoUrl?: string;
  createdDate: Date;
  createdAt: Date;
  photoCount: number;
}

export interface CreateAlbumCommand {
  userId: string;
  name: string;
  description?: string;
  coverPhotoUrl?: string;
  createdDate: Date;
}

export interface UpdateAlbumCommand {
  albumId: string;
  name: string;
  description?: string;
  coverPhotoUrl?: string;
  createdDate: Date;
}
