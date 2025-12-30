import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { GiftIdea, CreateGiftIdeaRequest, UpdateGiftIdeaRequest } from '../models';

@Injectable({ providedIn: 'root' })
export class GiftIdeasService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getGiftIdeas(recipientId?: string): Observable<GiftIdea[]> {
    let params = new HttpParams();
    if (recipientId) {
      params = params.set('recipientId', recipientId);
    }
    return this.http.get<GiftIdea[]>(`${this.baseUrl}/api/giftideas`, { params });
  }

  getGiftIdea(giftIdeaId: string): Observable<GiftIdea> {
    return this.http.get<GiftIdea>(`${this.baseUrl}/api/giftideas/${giftIdeaId}`);
  }

  createGiftIdea(request: CreateGiftIdeaRequest): Observable<GiftIdea> {
    return this.http.post<GiftIdea>(`${this.baseUrl}/api/giftideas`, request);
  }

  updateGiftIdea(request: UpdateGiftIdeaRequest): Observable<GiftIdea> {
    return this.http.put<GiftIdea>(`${this.baseUrl}/api/giftideas/${request.giftIdeaId}`, request);
  }

  deleteGiftIdea(giftIdeaId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/giftideas/${giftIdeaId}`);
  }

  markAsPurchased(giftIdeaId: string): Observable<GiftIdea> {
    return this.http.post<GiftIdea>(`${this.baseUrl}/api/giftideas/${giftIdeaId}/purchase`, {});
  }
}
