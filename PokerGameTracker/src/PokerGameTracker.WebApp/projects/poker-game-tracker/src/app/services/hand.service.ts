import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments';
import { Hand, CreateHand, UpdateHand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class HandService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.baseUrl}/api/hands`;

  private handsSubject = new BehaviorSubject<Hand[]>([]);
  public hands$ = this.handsSubject.asObservable();

  getHands(): Observable<Hand[]> {
    return this.http.get<Hand[]>(this.baseUrl).pipe(
      tap(hands => this.handsSubject.next(hands))
    );
  }

  getHandById(id: string): Observable<Hand> {
    return this.http.get<Hand>(`${this.baseUrl}/${id}`);
  }

  createHand(hand: CreateHand): Observable<Hand> {
    return this.http.post<Hand>(this.baseUrl, hand).pipe(
      tap(() => this.getHands().subscribe())
    );
  }

  updateHand(hand: UpdateHand): Observable<Hand> {
    return this.http.put<Hand>(`${this.baseUrl}/${hand.handId}`, hand).pipe(
      tap(() => this.getHands().subscribe())
    );
  }

  deleteHand(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => this.getHands().subscribe())
    );
  }
}
