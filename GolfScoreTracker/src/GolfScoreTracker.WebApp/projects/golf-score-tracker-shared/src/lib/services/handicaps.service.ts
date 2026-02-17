import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Handicap, CreateHandicapCommand, UpdateHandicapCommand } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HandicapsService {
  private readonly apiUrl = `${environment.baseUrl}/api/Handicaps`;
  private handicapsSubject = new BehaviorSubject<Handicap[]>([]);
  public handicaps$ = this.handicapsSubject.asObservable();

  private currentHandicapSubject = new BehaviorSubject<Handicap | null>(null);
  public currentHandicap$ = this.currentHandicapSubject.asObservable();

  constructor(private http: HttpClient) {}

  getHandicaps(userId?: string): Observable<Handicap[]> {
    let params = new HttpParams();
    if (userId) {
      params = params.set('userId', userId);
    }

    return this.http.get<Handicap[]>(this.apiUrl, { params }).pipe(
      tap(handicaps => this.handicapsSubject.next(handicaps))
    );
  }

  getHandicapById(id: string): Observable<Handicap> {
    return this.http.get<Handicap>(`${this.apiUrl}/${id}`).pipe(
      tap(handicap => this.currentHandicapSubject.next(handicap))
    );
  }

  createHandicap(command: CreateHandicapCommand): Observable<Handicap> {
    return this.http.post<Handicap>(this.apiUrl, command).pipe(
      tap(handicap => {
        const currentHandicaps = this.handicapsSubject.value;
        this.handicapsSubject.next([...currentHandicaps, handicap]);
      })
    );
  }

  updateHandicap(id: string, command: UpdateHandicapCommand): Observable<Handicap> {
    return this.http.put<Handicap>(`${this.apiUrl}/${id}`, command).pipe(
      tap(updatedHandicap => {
        const currentHandicaps = this.handicapsSubject.value;
        const index = currentHandicaps.findIndex(h => h.handicapId === id);
        if (index !== -1) {
          currentHandicaps[index] = updatedHandicap;
          this.handicapsSubject.next([...currentHandicaps]);
        }
        this.currentHandicapSubject.next(updatedHandicap);
      })
    );
  }

  deleteHandicap(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const currentHandicaps = this.handicapsSubject.value;
        this.handicapsSubject.next(currentHandicaps.filter(h => h.handicapId !== id));
      })
    );
  }

  clearCurrentHandicap(): void {
    this.currentHandicapSubject.next(null);
  }
}
