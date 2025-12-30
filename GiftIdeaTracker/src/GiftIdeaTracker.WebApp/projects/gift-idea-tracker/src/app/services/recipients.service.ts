import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Recipient, CreateRecipientRequest, UpdateRecipientRequest } from '../models';

@Injectable({ providedIn: 'root' })
export class RecipientsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getRecipients(): Observable<Recipient[]> {
    return this.http.get<Recipient[]>(`${this.baseUrl}/api/recipients`);
  }

  getRecipient(recipientId: string): Observable<Recipient> {
    return this.http.get<Recipient>(`${this.baseUrl}/api/recipients/${recipientId}`);
  }

  createRecipient(request: CreateRecipientRequest): Observable<Recipient> {
    return this.http.post<Recipient>(`${this.baseUrl}/api/recipients`, request);
  }

  updateRecipient(request: UpdateRecipientRequest): Observable<Recipient> {
    return this.http.put<Recipient>(`${this.baseUrl}/api/recipients/${request.recipientId}`, request);
  }

  deleteRecipient(recipientId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/recipients/${recipientId}`);
  }
}
