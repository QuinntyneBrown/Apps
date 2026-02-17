import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Interaction, CreateInteractionRequest, UpdateInteractionRequest } from '../models';

@Injectable({
  providedIn: 'root'
})
export class InteractionsService {
  private http = inject(HttpClient);
  private baseUrl = environment.baseUrl + '/api';

  private interactionsSubject = new BehaviorSubject<Interaction[]>([]);
  public interactions$ = this.interactionsSubject.asObservable();

  private selectedInteractionSubject = new BehaviorSubject<Interaction | null>(null);
  public selectedInteraction$ = this.selectedInteractionSubject.asObservable();

  loadInteractions(): Observable<Interaction[]> {
    return this.http.get<Interaction[]>(`${this.baseUrl}/interactions`).pipe(
      tap(interactions => this.interactionsSubject.next(interactions))
    );
  }

  getInteractionById(id: string): Observable<Interaction> {
    return this.http.get<Interaction>(`${this.baseUrl}/interactions/${id}`).pipe(
      tap(interaction => this.selectedInteractionSubject.next(interaction))
    );
  }

  createInteraction(request: CreateInteractionRequest): Observable<Interaction> {
    return this.http.post<Interaction>(`${this.baseUrl}/interactions`, request).pipe(
      tap(interaction => {
        const currentInteractions = this.interactionsSubject.value;
        this.interactionsSubject.next([...currentInteractions, interaction]);
      })
    );
  }

  updateInteraction(request: UpdateInteractionRequest): Observable<Interaction> {
    return this.http.put<Interaction>(`${this.baseUrl}/interactions/${request.interactionId}`, request).pipe(
      tap(updatedInteraction => {
        const currentInteractions = this.interactionsSubject.value;
        const index = currentInteractions.findIndex(i => i.interactionId === updatedInteraction.interactionId);
        if (index !== -1) {
          currentInteractions[index] = updatedInteraction;
          this.interactionsSubject.next([...currentInteractions]);
        }
      })
    );
  }

  deleteInteraction(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/interactions/${id}`).pipe(
      tap(() => {
        const currentInteractions = this.interactionsSubject.value;
        this.interactionsSubject.next(currentInteractions.filter(i => i.interactionId !== id));
      })
    );
  }

  clearSelectedInteraction(): void {
    this.selectedInteractionSubject.next(null);
  }
}
