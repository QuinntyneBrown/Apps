import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { RSVP, CreateRSVP, UpdateRSVP } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RSVPsService {
  private readonly apiUrl = `${environment.baseUrl}/api/rsvps`;
  private rsvpsSubject = new BehaviorSubject<RSVP[]>([]);
  public rsvps$ = this.rsvpsSubject.asObservable();

  constructor(private http: HttpClient) {}

  getRSVP(id: string): Observable<RSVP> {
    return this.http.get<RSVP>(`${this.apiUrl}/${id}`);
  }

  getRSVPsByEvent(eventId: string): Observable<RSVP[]> {
    return this.http.get<RSVP[]>(`${this.apiUrl}/event/${eventId}`).pipe(
      tap(rsvps => this.rsvpsSubject.next(rsvps))
    );
  }

  createRSVP(rsvp: CreateRSVP): Observable<RSVP> {
    return this.http.post<RSVP>(this.apiUrl, rsvp).pipe(
      tap(() => {
        if (rsvp.eventId) {
          this.getRSVPsByEvent(rsvp.eventId).subscribe();
        }
      })
    );
  }

  updateRSVP(id: string, rsvp: UpdateRSVP): Observable<RSVP> {
    return this.http.put<RSVP>(`${this.apiUrl}/${id}`, rsvp);
  }

  deleteRSVP(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
