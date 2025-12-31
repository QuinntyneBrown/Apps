import { Format } from './format.enum';
import { Genre } from './genre.enum';

export interface Album {
  albumId: string;
  userId: string;
  title: string;
  artistId?: string | null;
  artistName?: string | null;
  format: Format;
  genre: Genre;
  releaseYear?: number | null;
  label?: string | null;
  purchasePrice?: number | null;
  purchaseDate?: string | null;
  notes?: string | null;
  createdAt: string;
}

export interface CreateAlbum {
  userId: string;
  title: string;
  artistId?: string | null;
  format: Format;
  genre: Genre;
  releaseYear?: number | null;
  label?: string | null;
  purchasePrice?: number | null;
  purchaseDate?: string | null;
  notes?: string | null;
}

export interface UpdateAlbum {
  albumId: string;
  title: string;
  artistId?: string | null;
  format: Format;
  genre: Genre;
  releaseYear?: number | null;
  label?: string | null;
  purchasePrice?: number | null;
  purchaseDate?: string | null;
  notes?: string | null;
}
