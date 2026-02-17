export interface PersonTag {
  personTagId: string;
  photoId: string;
  personName: string;
  coordinateX?: number;
  coordinateY?: number;
  createdAt: Date;
}

export interface CreatePersonTagCommand {
  photoId: string;
  personName: string;
  coordinateX?: number;
  coordinateY?: number;
}
