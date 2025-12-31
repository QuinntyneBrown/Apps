export interface FamilyPhoto {
  familyPhotoId: string;
  personId: string;
  photoUrl?: string;
  caption?: string;
  dateTaken?: string;
  createdAt: string;
}

export interface CreateFamilyPhotoRequest {
  personId: string;
  photoUrl?: string;
  caption?: string;
  dateTaken?: string;
}

export interface UpdateFamilyPhotoRequest {
  familyPhotoId?: string;
  photoUrl?: string;
  caption?: string;
  dateTaken?: string;
}
