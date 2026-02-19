export interface Favorite {
  favoriteId: string;
  contentId: string;
  contentType: string;
  addedDate: Date;
  favoriteCategory?: string;
  rewatchCount: number;
  emotionalSignificance?: string;
}

export interface CreateFavoriteRequest {
  contentType: string;
  favoriteCategory?: string;
  emotionalSignificance?: string;
}
