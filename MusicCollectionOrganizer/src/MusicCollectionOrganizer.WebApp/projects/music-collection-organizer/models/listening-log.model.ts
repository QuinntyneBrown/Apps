export interface ListeningLog {
  listeningLogId: string;
  userId: string;
  albumId: string;
  albumTitle?: string | null;
  listeningDate: string;
  rating?: number | null;
  notes?: string | null;
  createdAt: string;
}

export interface CreateListeningLog {
  userId: string;
  albumId: string;
  listeningDate: string;
  rating?: number | null;
  notes?: string | null;
}

export interface UpdateListeningLog {
  listeningLogId: string;
  listeningDate: string;
  rating?: number | null;
  notes?: string | null;
}
