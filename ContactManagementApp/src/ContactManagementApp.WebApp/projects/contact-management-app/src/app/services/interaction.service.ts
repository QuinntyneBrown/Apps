import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Interaction, CreateInteractionRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class InteractionService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/interactions`;

  private interactionsSubject = new BehaviorSubject<Interaction[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  interactions$ = this.interactionsSubject.asObservable();
  loading$ = this.loadingSubject.asObservable();

  getInteractions(userId?: string, contactId?: string): Observable<Interaction[]> {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (contactId) params = params.set('contactId', contactId);

    return this.http.get<Interaction[]>(this.baseUrl, { params }).pipe(
      tap(interactions => {
        this.interactionsSubject.next(interactions);
        this.loadingSubject.next(false);
      })
    );
  }

  getInteractionById(interactionId: string): Observable<Interaction> {
    this.loadingSubject.next(true);

    return this.http.get<Interaction>(`${this.baseUrl}/${interactionId}`).pipe(
      tap(() => this.loadingSubject.next(false))
    );
  }

  createInteraction(request: CreateInteractionRequest): Observable<Interaction> {
    this.loadingSubject.next(true);

    return this.http.post<Interaction>(this.baseUrl, request).pipe(
      tap(interaction => {
        const currentInteractions = this.interactionsSubject.value;
        this.interactionsSubject.next([interaction, ...currentInteractions]);
        this.loadingSubject.next(false);
      })
    );
  }

  deleteInteraction(interactionId: string): Observable<void> {
    this.loadingSubject.next(true);

    return this.http.delete<void>(`${this.baseUrl}/${interactionId}`).pipe(
      tap(() => {
        const currentInteractions = this.interactionsSubject.value;
        this.interactionsSubject.next(currentInteractions.filter(i => i.interactionId !== interactionId));
        this.loadingSubject.next(false);
      })
    );
  }
}
