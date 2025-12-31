import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HoleScore, CreateHoleScoreCommand, UpdateHoleScoreCommand } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HoleScoresService {
  private readonly apiUrl = `${environment.baseUrl}/api/HoleScores`;
  private holeScoresSubject = new BehaviorSubject<HoleScore[]>([]);
  public holeScores$ = this.holeScoresSubject.asObservable();

  private currentHoleScoreSubject = new BehaviorSubject<HoleScore | null>(null);
  public currentHoleScore$ = this.currentHoleScoreSubject.asObservable();

  constructor(private http: HttpClient) {}

  getHoleScores(roundId?: string): Observable<HoleScore[]> {
    let params = new HttpParams();
    if (roundId) {
      params = params.set('roundId', roundId);
    }

    return this.http.get<HoleScore[]>(this.apiUrl, { params }).pipe(
      tap(holeScores => this.holeScoresSubject.next(holeScores))
    );
  }

  getHoleScoreById(id: string): Observable<HoleScore> {
    return this.http.get<HoleScore>(`${this.apiUrl}/${id}`).pipe(
      tap(holeScore => this.currentHoleScoreSubject.next(holeScore))
    );
  }

  createHoleScore(command: CreateHoleScoreCommand): Observable<HoleScore> {
    return this.http.post<HoleScore>(this.apiUrl, command).pipe(
      tap(holeScore => {
        const currentHoleScores = this.holeScoresSubject.value;
        this.holeScoresSubject.next([...currentHoleScores, holeScore]);
      })
    );
  }

  updateHoleScore(id: string, command: UpdateHoleScoreCommand): Observable<HoleScore> {
    return this.http.put<HoleScore>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedHoleScore => {
        const currentHoleScores = this.holeScoresSubject.value;
        const index = currentHoleScores.findIndex(h => h.holeScoreId === id);
        if (index !== -1) {
          currentHoleScores[index] = updatedHoleScore;
          this.holeScoresSubject.next([...currentHoleScores]);
        }
        this.currentHoleScoreSubject.next(updatedHoleScore);
      })
    );
  }

  deleteHoleScore(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentHoleScores = this.holeScoresSubject.value;
        this.holeScoresSubject.next(currentHoleScores.filter(h => h.holeScoreId !== id));
      })
    );
  }

  clearCurrentHoleScore(): void {
    this.currentHoleScoreSubject.next(null);
  }
}
