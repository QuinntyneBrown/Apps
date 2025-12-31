import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Itinerary, CreateItineraryCommand, UpdateItineraryCommand } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ItineraryService {
  private readonly apiUrl = `${environment.baseUrl}/api/itineraries`;
  private itinerariesSubject = new BehaviorSubject<Itinerary[]>([]);
  public itineraries$ = this.itinerariesSubject.asObservable();

  constructor(private http: HttpClient) {}

  getItineraries(tripId?: string): Observable<Itinerary[]> {
    const params = tripId ? { tripId } : {};
    return this.http.get<Itinerary[]>(this.apiUrl, { params }).pipe(
      tap(itineraries => this.itinerariesSubject.next(itineraries))
    );
  }

  getItineraryById(itineraryId: string): Observable<Itinerary> {
    return this.http.get<Itinerary>(`${this.apiUrl}/${itineraryId}`);
  }

  createItinerary(command: CreateItineraryCommand): Observable<Itinerary> {
    return this.http.post<Itinerary>(this.apiUrl, command).pipe(
      tap(itinerary => {
        const currentItineraries = this.itinerariesSubject.value;
        this.itinerariesSubject.next([...currentItineraries, itinerary]);
      })
    );
  }

  updateItinerary(itineraryId: string, command: UpdateItineraryCommand): Observable<Itinerary> {
    return this.http.put<Itinerary>(`${this.apiUrl}/${itineraryId}`, command).pipe(
      tap(updatedItinerary => {
        const currentItineraries = this.itinerariesSubject.value;
        const index = currentItineraries.findIndex(i => i.itineraryId === itineraryId);
        if (index !== -1) {
          currentItineraries[index] = updatedItinerary;
          this.itinerariesSubject.next([...currentItineraries]);
        }
      })
    );
  }

  deleteItinerary(itineraryId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${itineraryId}`).pipe(
      tap(() => {
        const currentItineraries = this.itinerariesSubject.value;
        this.itinerariesSubject.next(currentItineraries.filter(i => i.itineraryId !== itineraryId));
      })
    );
  }
}
