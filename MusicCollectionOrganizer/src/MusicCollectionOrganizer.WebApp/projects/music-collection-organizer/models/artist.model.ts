export interface Artist {
  artistId: string;
  userId: string;
  name: string;
  biography?: string | null;
  country?: string | null;
  formedYear?: number | null;
  website?: string | null;
  createdAt: string;
}

export interface CreateArtist {
  userId: string;
  name: string;
  biography?: string | null;
  country?: string | null;
  formedYear?: number | null;
  website?: string | null;
}

export interface UpdateArtist {
  artistId: string;
  name: string;
  biography?: string | null;
  country?: string | null;
  formedYear?: number | null;
  website?: string | null;
}
