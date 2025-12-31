import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments';
import { Distraction, CreateDistractionCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DistractionsService {
  private readonly apiUrl = `${environment.baseUrl}/api/Distractions`;
  private distractionsSubject = new BehaviorSubject<Distraction[]>([]);
  public distractions$ = this.distractionsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getDistractions(focusSessionId?: string): Observable<Distraction[]> {
    let params = new HttpParams();
    if (focusSessionId) params = params.set('focusSessionId', focusSessionId);

    return this.http.get<Distraction[]>(this.apiUrl, { params }).pipe(
      tap(distractions => this.distractionsSubject.next(distractions))
    );
  }

  createDistraction(command: CreateDistractionCommand): Observable<Distraction> {
    return this.http.post<Distraction>(this.apiUrl, command).pipe(
      tap(distraction => {
        const distractions = this.distractionsSubject.value;
        this.distractionsSubject.next([...distractions, distraction]);
      })
    );
  }

  deleteDistraction(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const distractions = this.distractionsSubject.value.filter(d => d.distractionId !== id);
        this.distractionsSubject.next(distractions);
      })
    );
  }
}
