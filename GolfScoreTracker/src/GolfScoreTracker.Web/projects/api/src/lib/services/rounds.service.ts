import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Round, CreateRoundCommand, UpdateRoundCommand } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoundsService {
  private readonly apiUrl = `${environment.baseUrl}/api/Rounds`;
  private roundsSubject = new BehaviorSubject<Round[]>([]);
  public rounds$ = this.roundsSubject.asObservable();

  private currentRoundSubject = new BehaviorSubject<Round | null>(null);
  public currentRound$ = this.currentRoundSubject.asObservable();

  constructor(private http: HttpClient) {}

  getRounds(userId?: string, courseId?: string): Observable<Round[]> {
    let params = new HttpParams();
    if (userId) {
      params = params.set('userId', userId);
    }
    if (courseId) {
      params = params.set('courseId', courseId);
    }

    return this.http.get<Round[]>(this.apiUrl, { params }).pipe(
      tap(rounds => this.roundsSubject.next(rounds))
    );
  }

  getRoundById(id: string): Observable<Round> {
    return this.http.get<Round>(`${this.apiUrl}/${id}`).pipe(
      tap(round => this.currentRoundSubject.next(round))
    );
  }

  createRound(command: CreateRoundCommand): Observable<Round> {
    return this.http.post<Round>(this.apiUrl, command).pipe(
      tap(round => {
        const currentRounds = this.roundsSubject.value;
        this.roundsSubject.next([...currentRounds, round]);
      })
    );
  }

  updateRound(id: string, command: UpdateRoundCommand): Observable<Round> {
    return this.http.put<Round>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedRound => {
        const currentRounds = this.roundsSubject.value;
        const index = currentRounds.findIndex(r => r.roundId === id);
        if (index !== -1) {
          currentRounds[index] = updatedRound;
          this.roundsSubject.next([...currentRounds]);
        }
        this.currentRoundSubject.next(updatedRound);
      })
    );
  }

  deleteRound(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentRounds = this.roundsSubject.value;
        this.roundsSubject.next(currentRounds.filter(r => r.roundId !== id));
      })
    );
  }

  clearCurrentRound(): void {
    this.currentRoundSubject.next(null);
  }
}
